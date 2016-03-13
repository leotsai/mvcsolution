(function() {
    var urls = {
        login: "/main/account/login"
    };
    var returnUrl = '';

    $(document).ready(function () {
        $("#navLogin").addClass("selected");
        var $hfReturnUrl = $("#hfReturnUrl");
        if ($hfReturnUrl.length > 0) {
            returnUrl = $hfReturnUrl.val();
        }

        $("#btnLogin").click(function () {
            if ($(this).hasClass("disabled")) {
                return;
            }
            doLogin();
        });
        MvcSolution.utils.onEnterKeydown("#txtPassword", function () {
            if ($("#btnLogin").hasClass("disabled")) {
                return;
            }
            doLogin();
        });
    });

    function doLogin() {
        var $form = $("#btnLogin").closest("form");
        var $btn = $("#btnLogin");
        $btn.cssDisable().html("Login...");
        MvcSolution.ajax.post(urls.login, $form.serialize(), function (result) {
            if (result.Success) {
                $btn.html("Login succeeded...");
                window.location = returnUrl === "" ? "/admin" : returnUrl;
            } else {
                $btn.cssEnable().html("Login");
                MvcSolution.notification.toastError(result.Message);
            }
        });
    };

})();