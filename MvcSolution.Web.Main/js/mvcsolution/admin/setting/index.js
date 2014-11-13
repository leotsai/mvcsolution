(function() {
    var urls = {
        update: "/admin/setting/update"
    };

    page.onLoaded = function () {
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
            mvcsolution.ajax.busyPost(urls.update, data, function(result) {
                if (result.Success) {
                    hideEditor($tr);
                    window.location = window.location;
                } else {
                    mvcsolution.notification.alertError(result.Message);
                }
            }, "保存中...");
        });
        $(".btn-cancel").click(function () {
            var $tr = $(this).closest("tr");
            $tr.find(".db-value span").show();
            $tr.find(".db-value input").hide().val($tr.find(".db-value span").html());
            hideEditor($tr);
        });
    };

    function showEditor(tr) {
        $(tr).find(".btn-edit").hide();
        $(tr).find(".save").show();
    };

    function hideEditor(tr) {
        $(tr).find(".btn-edit").show();
        $(tr).find(".save").hide();
    };
})();