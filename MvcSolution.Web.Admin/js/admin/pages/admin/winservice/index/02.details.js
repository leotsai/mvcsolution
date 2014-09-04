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