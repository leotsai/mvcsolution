(function () {
    var urls = {
        list: "/admin/user/list",
        tags: "/admin/tag/entityList",
        saveUser: "/admin/user/save",
        userEditor: "/admin/user/editor",
        sendWeixin: "/admin/user/sendWeixin"
    };
    var criteria = "";
    var grid = null;

    $(document).ready(function() {
        $("#navUser").addClass("selected");
        grid = new Grid("tbody", ".pager");
        grid.pageSize = 20;
        grid.init();
        grid.url = urls.list;
        grid.getDataFunc = function () {
            return criteria;
        };
        grid.complete(function () {
            $("a.btn-edit").click(function() {
                showEditor($(this).closest("tr").attr("data-id"));
            });
            $(".cell-avartar img").click(function() {
                new ImageViewer().show([$(this).attr("src")], 0);
            });
        });
        $("#btnSearch").click(function () {
            search();
        });
        $("#btnTags").click(function () {
            page.showTagManager();
        });
        $("#btnMessage").click(function () {
            sendMessage();
        });
        search();
    });

    function search() {
        criteria = $("#formSearch").serialize();
        grid.pageTo(0);
    };

    page.refreshTagsAndGrid = function () {
        MvcSolution.ajax.post(urls.tags, null, function (result) {
            var id = $("#ddlTags").val();
            var html = '<option value="">Any/All</option>';
            for (var i = 0; i < result.Value.length; i++) {
                var item = result.Value[i];
                html += '<option value="' + item.Id + '">' + item.Name + '</option>';
            }
            $("#ddlTags").html(html).val(id);
            grid.refresh();
        });
    };

    function sendMessage() {
        var html = '<div class="group">\
                        Are you sure you want send message to the searched <b>' + grid.totalRows + '</b> users？\
                    </div>\
                    <div class="group">\
                        <label>Message Content：</label>\
                        <textarea id="txtWeixinContent" placeholder="less than 250 chars, html is allowed"></textarea>\
                    </div>\
                    <div class="group">\
                        Message Type：\
                        <input type="checkbox" id="cbWeixin" name="types" value="Weixin"/>\
                        <label for="cbWeixin">WeChat</label>\
                        <input type="checkbox" id="cbSystemMmessage" name="types" value="SystemMessage"/>\
                        <label for="cbSystemMmessage">System Message</label>\
                    </div>';
        MvcSolution.notification.dialog("dialogWeixin", "Send WeChat Message And System Message", {
            width: 500,
            height: 'auto',
            minHeight: 300,
            buttons: {
                "Cancel": function () {
                    $(this).dialog("close");
                },
                "Send": function () {
                    doSendMessage();
                }
            },
            open: function () {
                $("#dialogWeixin").html(html);
            }
        });
    };

    function doSendMessage() {
        var htmlContent = $("#txtWeixinContent").val();
        var text = $("<div>").html(htmlContent).text();
        var data = criteria + "&htmlContent=" + htmlContent + "&text=" + text;
        if ($("#cbWeixin").is(":checked")) {
            data += "&types=Weixin";
        }
        if ($("#cbSystemMmessage").is(":checked")) {
            data += "&types=SystemMessage";
        }

        MvcSolution.ajax.busyPost(urls.sendWeixin, data, function (result) {
            if (result.Success) {
                $("#dialogWeixin").dialog("close");
                MvcSolution.notification.alert("Success", "Message sent successfully！");
            } else {
                MvcSolution.notification.alertError(result.Message);
            }
        }, "sending...");
    };

    function showEditor(userId) {
        var dialogId = "dialogUserEditor";
        MvcSolution.notification.dialog(dialogId, "Edit User", {
            width: 600,
            height: 'auto',
            minHeight: 300,
            buttons: {
                "Cancel": function () {
                    $(this).dialog("close");
                },
                "Save": function () {
                    var dialog = this;
                    saveUser(function () {
                        $(dialog).dialog("close");
                        grid.refresh();
                    });
                }
            },
            open: function () {
                MvcSolution.ajax.load("#" + dialogId, urls.userEditor + "?userId=" + userId);
            }
        });

        function saveUser(callback) {
            var $form = $("#formEditor");
            $("#cellTags input").removeAttr("name");
            $("#cellTags input:checked").each(function (i) {
                $(this).attr("name", "tagIds[" + i + "]");
            });
            var data = $form.serialize();
            MvcSolution.ajax.busyPost(urls.saveUser, data, function (result) {
                if (result.Success) {
                    callback && callback();
                } else {
                    MvcSolution.notification.alertError(result.Message);
                }
            }, "Saving...");
        }
    };

})();