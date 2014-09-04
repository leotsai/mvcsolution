(function () {
    if (window.FileUpload) {
        return;
    }
    window.FileUpload = function (id, url) {
        this.id = id;
        this.autoUpload = true;
        this.url = url;
        this.maxSize = null;
        this.extensions = null;
        this.dropId = null;
    };

    window.FileUpload.prototype.init = function () {
        var obj = this;
        $('#' + this.id).change(function () {
            if (obj.autoUpload) {
                obj.upload();
            }
        });
        if (this.supportsFormData()) {
            if (this.dropId != null) {
                var drop = $('#' + this.dropId)[0];
                drop.addEventListener("dragover", function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    $('#' + obj.dropId).addClass("dragover");
                }, false);
                drop.addEventListener("dragout", function (e) {
                    $('#' + obj.dropId).removeClass("dragover");
                }, false);
                drop.addEventListener("drop", function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    $('#' + obj.dropId).removeClass("dragover");
                    obj._uploadUsingFormData(e.dataTransfer.files[0]);
                }, false);
            }
        } else {
            if (this.dropId != null) {
                $('#' + this.dropId).hide();
            }
        }
    };

    FileUpload.prototype.supportsFormData = function () {
        return window.FormData != undefined;
    };

    FileUpload.prototype.upload = function() {
        var evt = $.Event("onBeforeUploading");
        var args = {
            isCancelled: false
        };
        $(this).trigger(evt, args);
        if (args.isCancelled) {
            return;
        }

        if (this.supportsFormData()) {
            this._uploadUsingFormData($("#" + this.id)[0].files[0]);
        } else {
            this._uploadUsingFrame();
        }
    };

    FileUpload.prototype._uploadUsingFrame = function () {
        var obj = this;
        var $frame = $('#uploadFrame');
        if ($frame.length == 0) {
            $frame = $('<iframe id="uploadFrame" width="0" height="0" name="uploadFrame" src=""></iframe>');
            $frame.appendTo("body");
            $frame.load(function () {
                var response = $frame.contents().text();
                try {
                    var json = $.parseJSON(response);
                    $(obj).trigger("onLoad", json);
                } catch (ex) {
                    $(obj).trigger("onError", response);
                }
            });
        }
        var $form = $("#" + this.id).closest("form");
        var action = $form.attr("action");
        $form.attr("action", this.url);
        var form = $form[0];
        form.target = 'uploadFrame';
        form.submit();
        $form.attr("action", action);
    };

    FileUpload.prototype._uploadUsingFormData = function (file) {
        var obj = this;
        var xhr = new XMLHttpRequest();
        xhr.addEventListener("load", function (e) {
            var json = $.parseJSON(xhr.response);
            $(obj).trigger("onLoad", json);
        }, false);
        xhr.addEventListener("error", function (e) {
            $(obj).trigger("onError", e);
        }, false);
        xhr.upload.addEventListener("progress", function (e) {
            if (e.lengthComputable) {
                $(obj).trigger("onProgress", e.loaded, e.total);
            }
        }, false);
        xhr.open("POST", obj.url);

        if (obj.maxSize != null && file.size > obj.maxSize) {
            $(obj).trigger("onMaxSizeError");
            return;
        }
        if (obj.extensions != null) {
            var name = file.name;
            var ext = name.substring(name.lastIndexOf("."), name.length).toLowerCase();
            if (obj.extensions.indexOf(ext) < 0) {
                $(obj).trigger("onExtensionError");
                return;
            }
        }
        var formData = new FormData();
        formData.append("files", file);
        xhr.send(formData);
    };

    FileUpload.prototype.onLoad = function (handler) {
        $(this).bind("onLoad", function (sender, args) {
            handler && handler(args);
        });
    };

    FileUpload.prototype.onBeforeUploading = function (handler) {
        $(this).bind("onBeforeUploading", function (sender, args) {
            handler && handler(args);
        });
    };

    FileUpload.prototype.onProgress = function (handler) {
        $(this).bind("onProgress", function (sender, loaded, total) {
            handler && handler(loaded, total);
        });
    };

    FileUpload.prototype.onError = function (handler) {
        $(this).bind("onError", function (sender, error) {
            handler && handler(error);
        });
    };

    FileUpload.prototype.onMaxSizeError = function (handler) {
        $(this).bind("onMaxSizeError", handler);
    };

    FileUpload.prototype.onExtensionError = function (handler) {
        $(this).bind("onExtensionError", handler);
    };
})();
(function () {
    var urls = {
        uploader: "/admin/winservice/uploader",
        upload: "/admin/winservice/upload",
        search: "/admin/winservice/search"
    };

    var searchCriteria = null;
    page.currentTaskId = null;

    page.onLoaded = function () {
        $("#btnSearch").click(function () {
            page.search();
        });
        $("#btnUpload").click(function () {
            page.showUploader();
        });
        page.search();
    };

    page.search = function () {
        var data = $("#formSearch").serialize();
        searchCriteria = data;
        this.refresh();
    };

    page.showDetails = function (sender) {
        var id = $(sender).closest("tr").attr("data-id");
        this.currentTaskId = id;
        var name = $(sender).closest("tr").find("h4").html();
        admin.notification.dialog("dialogDetails", name, {
            width: 800,
            height: 500,
            open: function () {
                page.refreshDetails();
            }
        });
    };

    page.showUploader = function (id) {
        var upload;
        admin.notification.dialog("dialogUpload", "上传DLL", {
            width: 500,
            height: 300,
            buttons: {
                "取消": function () {
                    $(this).dialog("close");
                },
                "上传": function () {
                    upload.upload();
                }
            },
            open: function () {
                admin.ajax.load("#dialogUpload", urls.uploader, function () {
                    upload = new FileUpload("fileDll", urls.upload + "?id=" + id);
                    upload.autoUpload = false;
                    upload.extensions = ".dll";
                    upload.onLoad(function (result) {
                        if (result.Success) {
                            admin.notification.toastSuccess("成功！");
                            window.location = window.location;
                        } else {
                            admin.notification.alertError(result.Message);
                        }
                    });
                    upload.init();
                });
            }
        });
    };

    page.refresh = function() {
        admin.ajax.busyPost(urls.search, searchCriteria, function (html) {
            $("#result").html(html);
            $(".task-details").click(function () {
                page.showDetails(this);
            });
        }, "搜索中...");
    };

    page.closeDetailsDialog = function() {
        $("#dialogDetails").dialog("close");
    };

})();
(function () {
    var urls = {
        stop: "/admin/WinService/Stop",
        start: "/admin/WinService/Start",
        deleteTask: "/admin/WinService/Delete",
        run: "/admin/WinService/RunImmediately",
        details: "/admin/winservice/taskDetails"
    };

    page.bindButtons = function () {
        $(".task-btn-stop").click(function () {
            if (!confirm("确定要停止吗？")) {
                return;
            }
            admin.ajax.busyPost(urls.stop, "id=" + page.getId(this), function (result) {
                if (result.Success) {
                    admin.notification.toastSuccess("成功！");
                    page.refresh();
                    page.refreshDetails();
                } else {
                    admin.notification.alertError(result.Message);
                }
            }, "处理中...");
        });

        $(".task-btn-start").click(function () {
            if (!confirm("确定要启动吗？")) {
                return;
            }
            admin.ajax.busyPost(urls.start, "id=" + page.getId(this), function (result) {
                if (result.Success) {
                    admin.notification.toastSuccess("成功！");
                    page.refresh();
                    page.refreshDetails();
                } else {
                    admin.notification.alertError(result.Message);
                }
            }, "处理中...");
        });

        $(".task-btn-updatedll").click(function () {
            page.showUploader(page.getId(this));
        });
        $("#btnUploadDll").click(function () {
            page.showUploader(null);
        });
        $(".task-btn-delete").click(function () {
            if (!confirm("确定要删除任务吗？")) {
                return;
            }
            admin.ajax.busyPost(urls.deleteTask, "id=" + page.getId(this), function (result) {
                if (result.Success) {
                    admin.notification.toastSuccess("成功！");
                    page.refresh();
                    page.closeDetailsDialog();
                } else {
                    admin.notification.alertError(result.Message);
                }
            }, "删除中...");
        });
        $(".btn-run-immediately").click(function () {
            page.runImmediately(page.getId(this));
        });
        $(".btn-refresh-details").click(function() {
            page.refreshDetails();
        });
    };

    page.runImmediately = function (id) {
        if (!confirm("确定要立即执行任务吗？")) {
            return;
        }
        admin.ajax.busyPost(urls.run, "id=" + id, function (result) {
            if (result.Success) {
                admin.notification.toastSuccess("成功！");
                page.refresh();
                page.refreshDetails();
            } else {
                admin.notification.alertError(result.Message);
            }
        }, "更新中...");
    };

    page.getId = function (sender) {
        return $(sender).closest(".group").attr("data-task-id");
    };

    page.refreshDetails = function() {
        admin.ajax.load("#dialogDetails", urls.details + "?taskid=" + page.currentTaskId, function () {
            page.bindButtons();
        }, "加载中...");
    };
})();