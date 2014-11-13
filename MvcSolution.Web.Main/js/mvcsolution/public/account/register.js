(function() {
    var urls = {
        register: "/register",
        login: "/login"
    };

    page.onLoaded = function() {
        var obj = this;
        $("#btnRegister").click(function() {
            obj.register(function() {
                mvcsolution.notification.messageBox("Done", "Registration succeeded.", {
                    "Login Now": function() {
                        window.location = urls.login;
                    },
                    "Close": function() {
                        $(this).dialog("close");
                    }
                });
            });
        });
    };

    page.register = function(callback) {
        var $form = $('#formRegister');
        if (!$form.valid()) {
            return;
        }
        var data = $form.serialize();
        mvcsolution.ajax.busyPost(urls.register, data, function(result) {
            if (result.Success) {
                callback && callback();
            } else {
                mvcsolution.notification.alertError(result.Message);
            }
        }, "Registering...");
    };
})();