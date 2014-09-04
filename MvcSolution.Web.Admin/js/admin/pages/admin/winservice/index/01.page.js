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