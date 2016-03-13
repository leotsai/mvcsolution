MvcSolution.ajax.busyPost = function (url, data, successCallBack, busyContent) {
    data = "ajax=true&ts=" + new Date().getTime() + "&" + data;
    $.ajax({
        type: "post",
        url: url,
        contentType: "application/x-www-form-urlencoded",
        data: data,
        beforeSend: function (request) {
            var op = {
                isBusy: true,
                busyContent: busyContent == undefined ? '处理中...' : busyContent,
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
                successCallBack({
                    Success: false,
                    Message: '网络出错了'
                });
            }
        }
    });
};

MvcSolution.ajax.post = function (url, data, successCallBack) {
    data = "ajax=true&ts=" + new Date().getTime() + "&" + data;
    $.ajax({
        type: "post",
        url: url,
        contentType: "application/x-www-form-urlencoded",
        data: data,
        success: function (result) {
            successCallBack(result);
        },
        error: function (err) {
            if (err.status != 0) {
                successCallBack({
                    Success: false,
                    Message: '网络出错了'
                });
            }
        }
    });
};

MvcSolution.ajax.load = function (wrapper, url, successCallback, message) {
    var $wrapper = $(wrapper);
    $wrapper.empty();
    message = message == undefined ? "加载中..." : message;
    $wrapper.html("<div class=='loading'>" + message + "</div>");
    var loadUrl;
    if (url.indexOf("?") < 0) {
        loadUrl = url + "?ajax=true&ts=" + new Date().getTime();
    } else {
        loadUrl = url + "&ajax=true&ts=" + new Date().getTime();
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
                MvcSolution.ajax.load(wrapper, url, successCallback);
            });
        }
    });
};
