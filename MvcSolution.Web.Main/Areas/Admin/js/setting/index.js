(function() {
    var urls = {
        save: "/admin/setting/update"
    };

    $(document).ready(function () {
        $("#navSetting").addClass("selected");
        $(".btn-edit").click(function () {
            var $tr = $(this).closest("tr");
            $tr.find(".db-value span").hide();
            $tr.find(".db-value input").show();
            showEditor($tr);
        });
        $(".btn-save").click(function () {
            var $tr = $(this).closest("tr");
            var data = "key=" + $tr.find(".db-key").html() + "&value=" + $tr.find(".db-value input").val();
            MvcSolution.ajax.busyPost(urls.save, data, function (result) {
                if (result.Success) {
                    hideEditor($tr);
                    window.location = window.location;
                } else {
                    MvcSolution.notification.alertError(result.Message);
                }
            }, "Saving...");
        });
        $(".btn-cancel").click(function () {
            var $tr = $(this).closest("tr");
            $tr.find(".db-value span").show();
            $tr.find(".db-value input").hide().val($tr.find(".db-value span").html());
            hideEditor($tr);
        });
    });

    function showEditor(tr) {
        $(tr).find(".btn-edit").hide();
        $(tr).find(".save").show();
    };

    function hideEditor(tr) {
        $(tr).find(".btn-edit").show();
        $(tr).find(".save").hide();
    };
})();