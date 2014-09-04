(function() {
    var urls = {
        searchList: "/admin/user/list",
        editor: "/admin/user/editor",
        save: "/admin/user/save"
    };

    page.onLoaded = function () {
        var me = this;
        $("#btnSearch").click(function () {
            me.search();
        });
        $("#btnCreate").click(function (e) {
            e.preventDefault();
            me.showEditor();
        });
        $(".edit-link").live("click", function(e) {
            e.preventDefault();
            me.showEditor($(this).closest("tr").attr("data-id"));
        });
        me.search();
    };

    page.search = function() {
        var data = $("#formSearch").serialize();
        admin.ajax.busyPost(urls.searchList, data, function(html) {
            $("#result").html(html);
        }, "加载中...");
    };

    page.showEditor = function(id) {
        var me = this;
        var isNew = id == undefined;
        var dialogId = "dialogEditor";
        admin.notification.dialog(dialogId, isNew ? "新增用户" : "编辑用户", {
            width: 800,
            height: 600,
            buttons: {
                "取消":function() {
                    $(this).dialog("close");
                },
                "保存":function() {
                    save();
                }
            },
            open:function() {
                admin.ajax.load("#" + dialogId, urls.editor + "?id=" + id, function() {
                    admin.utils.parseUnobtrusive("#" + dialogId + " form");
                });
            }
        });

        function save() {
            var $form = $("#" + dialogId + " form");
            if ($form.valid() == false) {
                return;
            }
            var data = $form.serialize();
            data += "&dbUser.password=" + $("#txtPassword").val();
            $("#roles :checked").each(function(i) {
                data += "&roles[" + i + "]=" + $(this).val();
            });

            admin.ajax.busyPost(urls.save, data, function(result) {
                if (result.Success) {
                    admin.notification.toastSuccess("保存成功！");
                    $("#" + dialogId).dialog("close");
                    me.search();
                } else {
                    admin.notification.alertError(result.Message);
                }
            }, "保存中...");
        }
    };

})();


