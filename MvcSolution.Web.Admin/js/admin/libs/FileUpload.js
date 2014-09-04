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