(function() {
    var urls = {
        register: "/main/account/register"
    };

    $(document).ready(function () {
        $("#navRegister").addClass("selected");

        $("#btnRegister").click(function () {
            if ($(this).hasClass("disabled")) {
                return;
            }
            register();
        });
        $("#cbAgree").click(function() {
            setBtnRegisterStatus();
        });
    });

    function register() {
        if (!$("#cbAgree").is(":checked")) {
            MvcSolution.notification.toast("You have to agree to the Agreement");
            return;
        }

        $("#btnRegister").cssDisable().html("Processing...");
        $("#cbAgree").attr("disabled", "disabled");

        MvcSolution.ajax.post(urls.register, $("form").serialize(), function (result) {
            $("#cbAgree").removeAttr("disabled");
            if (result.Success) {
                window.location.replace("/main/account/registerCompleted");
            } else {
                MvcSolution.notification.toastError(result.Message);
                $("#btnRegister").cssEnable().html("Submit");
            }
        });
    };

    function setBtnRegisterStatus()
    {
        if ($("#cbAgree").is(":checked")) {
            $("#btnRegister").cssEnable();
        } else {
            $("#btnRegister").cssDisable();
        }
    };

})();