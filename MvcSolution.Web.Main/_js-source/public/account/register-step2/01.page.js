(function() {
    var urls = {
        completeRegistration: "/main/account/CompleteRegistration"
    };
    var uploader = null;

    $(document).ready(function () {
        $("#btnRegister").click(function () {
            if ($(this).hasClass("disabled")) {
                return;
            }
            register();
        });

        uploader = new WebImageUploader("#imgUploader", []);
        uploader.maxImages = 1;
        uploader.imageType = 189;
        uploader.showTip = false;
        uploader.cropCenter = true;
        uploader.init();
    });

    function register() {
        var imgs = uploader.getImageKeys();
        if (imgs == null) {
            return;
        }
        if (imgs.length == 0) {
            MvcSolution.notification.toastError("Please upload avartar");
            return;
        }
        $("#hfImageKey").val(imgs[0]);

        $("#btnRegister").cssDisable().html("Processing...");

        MvcSolution.ajax.post(urls.completeRegistration, $("form").serialize(), function (result) {
            if (result.Success) {
                window.location.replace("/admin");
            } else {
                MvcSolution.notification.toastError(result.Message);
                $("#btnRegister").cssEnable().html("Complete Registration");
            }
        });
    };
})();