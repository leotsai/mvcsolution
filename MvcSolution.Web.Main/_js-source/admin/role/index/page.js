(function () {
    var urls = {
        list: "/admin/role/SearchUsers",
        add: "/admin/role/add",
        save: "/admin/role/save",
        deleteUser: "/admin/role/delete"
    };
    var grid = null;
    var criteria = '';

    $(document).ready(function() {
        $("#navUser").addClass("selected");
        grid = new Grid("tbody", ".pager");
        grid.pageSize = 20;
        grid.init();
        grid.url = urls.list;
        grid.getDataFunc = function() {
            return criteria;
        };
        grid.complete(function() {
            $("a.btn-delete").click(function() {
                deleteUser($(this).closest("tr").find(".cell-username").html());
            });
            $(".cell-avartar img").click(function () {
                new ImageViewer().show([$(this).attr("src")], 0);
            });
        });
        $("#btnSearch").click(function() {
            search();
        });
        $("#btnAdd").click(function() {
            showEditor();
        });
        search();
    });

    function search() {
        criteria = $("#formSearch").serialize();
        grid.pageTo(0);
    };

    function showEditor() {
        MvcSolution.notification.dialog("dialogEditor", "Add", {
            width: 400,
            height: 'auto',
            minHeight: 200,
            buttons: {
                "Cancel": function() {
                    $(this).dialog("close");
                },
                "Save": function() {
                    save();
                }
            },
            open: function() {
                MvcSolution.ajax.load("#dialogEditor", urls.add);
            }
        });
    };

    function save() {
        var $form = $("#dialogEditor form");
        MvcSolution.ajax.busyPost(urls.save, $form.serialize(), function (result) {
            if (result.Success) {
                $("#dialogEditor").dialog("close");
                MvcSolution.notification.alert("Success", "Saved. The new role will take effect when the user logout and re-login.", function () {
                    grid.refresh();
                });
            } else {
                MvcSolution.notification.toastError(result.Message);
            }
        });
    };

    function deleteUser(username) {
        var msg = "Are you sure you wan to remove all roles of the user? If removed, the user will be an normal registered user.";
        MvcSolution.notification.messageBox("Remove Role", msg, {
            "Cancel": function() {
                $(this).dialog("close");
            },
            "Confirm to Remove": function() {
                $(this).dialog("close");
                doDelete(username);
            }
        });
        
    };

    function doDelete(username) {
        MvcSolution.ajax.busyPost(urls.deleteUser, "username=" + username, function (result) {
            if (result.Success) {
                MvcSolution.notification.alert("Success", "Roles removed. It will take effect when the user logout and re-login.", function () {
                    grid.refresh();
                });
            } else {
                MvcSolution.notification.toastError(result.Message);
            }
        });
    };

})();