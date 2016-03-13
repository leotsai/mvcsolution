(function() {
    var dialogId = "dialogTagManager";
    var hasChanges = false;
    var urls = {
        saveTag: "/admin/tag/save",
        tagList: "/admin/tag/list",
        deleteTag: "/admin/tag/delete"
    };

    page.showTagManager = function () {
        hasChanges = false;
        MvcSolution.notification.dialog(dialogId, "Manage Tags", {
            width: 600,
            height: 400,
            open: function () {
                var html = '<div class="group">\
                                <input type="text" class="txt" id="txtNewTagName" placeholder="Tag Name"/>\
                                <a href="javascript:void(0);" id="btnAddNewTag" class="btn">Add a Tag</a>\
                            </div>\
                            <div>\
                                <table class="grid">\
                                    <colgroup>\
                                        <col width="auto"/>\
                                        <col width="100px"/>\
                                    </colgroup>\
                                    <thead>\
                                        <tr>\
                                            <th>Name</th>\
                                            <th class="center">Options</th>\
                                        </tr>\
                                    </thead>\
                                    <tbody></tbody>\
                                </table>\
                            </div>';
                $("#" + dialogId).html(html);
                loadTagList();
                bindAddTagButton();
            },
            close: function () {
                if (hasChanges) {
                    page.refreshTagsAndGrid();
                }
            }
        });
    };

    function bindRowClicks() {
        $("#" + dialogId + " a.btn-edit").click(function () {
            var $tr = $(this).closest("tr");
            $tr.find("input").val($tr.find("label").html()).show();
            $tr.find("label").hide();
            showSaveBtn($tr);
        });
        $("#" + dialogId + " a.btn-cancel").click(function () {
            var $tr = $(this).closest("tr");
            $tr.find("input").hide();
            $tr.find("label").show();
            hideSaveBtn($tr);
        });
        $("#" + dialogId + " a.btn-delete").click(function () {
            if (!confirm("Are you sure you want to delete this tag?")) {
                return;
            }
            var $tr = $(this).closest("tr");
            var id = $tr.attr("data-id");
            MvcSolution.ajax.busyPost(urls.deleteTag, "tagid=" + id, function (result) {
                if (result.Success) {
                    MvcSolution.notification.toastSuccess("The tag was deleted!");
                    $tr.remove();
                    hasChanges = true;
                } else {
                    MvcSolution.notification.toastError(result.Message);
                }
            });
        });
        $("#" + dialogId + " a.btn-save").click(function () {
            var $tr = $(this).closest("tr");
            var id = $tr.attr("data-id");
            var name = $tr.find("input").val();
            MvcSolution.ajax.busyPost(urls.saveTag, "id=" + id + "&name=" + name, function (result) {
                if (result.Success) {
                    loadTagList();
                    hasChanges = true;
                } else {
                    MvcSolution.notification.toastError(result.Message);
                }
            }, "Saving...");
        });
    };

    function bindAddTagButton() {
        $("#" + dialogId + " a#btnAddNewTag").click(function () {
            var name = $("#txtNewTagName").val();
            if (name == '') {
                MvcSolution.notification.alert("please enter a name");
                return;
            }
            MvcSolution.ajax.busyPost(urls.saveTag, "id=newid&name=" + name, function (result) {
                if (result.Success) {
                    $("#txtNewTagName").val('');
                    loadTagList();
                    hasChanges = true;
                } else {
                    MvcSolution.notification.toastError(result.Message);
                }
            }, "Saving...");
        });
    };

    function showSaveBtn($tr) {
        $tr.find("a.btn-edit,a.btn-delete").hide();
        $tr.find("a.btn-cancel,a.btn-save").show();
    };

    function hideSaveBtn($tr) {
        $tr.find("a.btn-edit,a.btn-delete").show();
        $tr.find("a.btn-cancel,a.btn-save").hide();
    };

    function loadTagList() {
        $("#" + dialogId + " tbody").html('<tr><td colspan="2" class="loading">Loading...</td></tr>');
        MvcSolution.ajax.post(urls.tagList, null, function (html) {
            $("#" + dialogId + " tbody").html(html);
            bindRowClicks();
        });
    };

})();
