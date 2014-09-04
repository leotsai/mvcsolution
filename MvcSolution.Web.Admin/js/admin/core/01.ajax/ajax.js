admin.ajax.busyPost = function (url, data, successCallBack, busyContent) {
    data = "ajax=true&" + data;
    $.ajax({
        type: "post",
        url: url,
        contentType: "application/x-www-form-urlencoded",
        data: data,
        beforeSend: function (request) {
            var op = {
                isBusy:true,
                busyContent: busyContent == undefined ? 'Loading...' : busyContent,
                request: request,
                showCancel: true,
                delay: 500,
                modal: true
            };
            $.busy(op);
        },
        success: function (result) {
            $.busy(false);
            successCallBack(result);
        },
        error: function (err) {
            $.busy(false);
            if (err.status != 0) {
                admin.notification.toastError("Error occurred!");
            }
        }
    });
};

admin.ajax.load = function (wrapper, url, successCallback, message) {
    var $wrapper = $(wrapper);
    $wrapper.empty();
    message = message == undefined ? "Loading..." : message;
    $wrapper.html("<div class=='loading'>" + message + "</div>");
    var loadUrl;
    if (url.indexOf("?") < 0) {
        loadUrl = url + "?ajax=true";
    } else {
        loadUrl = url + "&ajax=true";
    }
    $.ajax({
        type: "get",
        url: loadUrl,
        success: function (result) {
            $wrapper.html(result);
            if (successCallback != undefined && successCallback != null) {
                successCallback();
            }
        },
        error: function () {
            $wrapper.html("<div class='load-error'><span class='error'>Error occuurred.</span><a href='javascript:void(0);'>Re-try</a></div>");
            $wrapper.find("a").click(function () {
                admin.ajax.load(wrapper, url, successCallback);
            });
        }
    });
};

admin.ajax.getJSON = function (url, successCallback, errorCallback) {

    function returnError(error) {
        if (errorCallback == undefined) {
            admin.notification.alertError(error);
        } else if (errorCallback != null) {
            errorCallback();
        }
    }

    $.ajax({
        type: "get",
        url: url,
        success: function (result) {
            if (result.Success) {
                if (successCallback != undefined && successCallback != null) {
                    successCallback(result.Value);
                }
            } else {
                returnError(result.Message);
            }
        },
        error: function (err) {
            returnError(err);
        }
    });
};