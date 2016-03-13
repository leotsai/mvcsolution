(function () {
    if (window.WeixinJsSdk) {
        return;
    }

    var isInitialized = false;
    var urlConfigOptions = "/app/weixin/GetConfigOptions";
    var jsApiList = ['chooseImage', 'openLocation', 'previewImage', 'uploadImage', 'onMenuShareTimeline'];

    window.WeixinJsSdk = function () {

    };

    WeixinJsSdk.isInWeixin = function () {
        return $("#body").hasClass("weixin");
    };

    WeixinJsSdk.registerJsApiList = function (array) {
        jsApiList = array;
    };

    WeixinJsSdk.bindLocationBtn = function (selector) {
        $(selector).click(function (e) {
            if (WeixinJsSdk.isInWeixin() == false) {
                return;
            }
            e.preventDefault();
            var lat = $(this).attr("data-lat") * 1;
            var lng = $(this).attr("data-lng") * 1;
            var address = $.trim($(this).html());

            var txPosition = LatLngConverter.convertBaidu2Tengxin(lng, lat);

            WeixinJsSdk.ready(function () {
                wx.openLocation({
                    latitude: txPosition.lat,
                    longitude: txPosition.lng,
                    name: address,
                    address: address,
                    scale: 15,
                    infoUrl: ''
                });
            });
        });
    };

    WeixinJsSdk.onShare = function (title, link, imgUrl, description, successCallback, cancelCallback) {
        if (description == null || description === '') {
            description = title;
        }
        if (description.length > 100) {
            description = description.substring(0, 100) + "...";
        }

        WeixinJsSdk.ready(function () {
            wx.onMenuShareTimeline({
                title: title,
                link: link,
                imgUrl: imgUrl,
                success: function () {
                    successCallback && successCallback();
                },
                cancel: function () {
                    cancelCallback && cancelCallback();
                }
            });

            wx.onMenuShareAppMessage({
                title: title, 
                desc: description, 
                link: link, 
                imgUrl: imgUrl, 
                type: 'link', 
                dataUrl: '', 
                success: function () {
                    successCallback && successCallback();
                },
                cancel: function () {
                    cancelCallback && cancelCallback();
                }
            });

            wx.onMenuShareQQ({
                title: title, 
                desc: description, 
                link: link, 
                imgUrl: imgUrl, 
                success: function () {
                    successCallback && successCallback();
                },
                cancel: function () {
                    cancelCallback && cancelCallback();
                }
            });

            wx.onMenuShareWeibo({
                title: title, 
                desc: description, 
                link: link, 
                imgUrl: imgUrl, 
                success: function () {
                    successCallback && successCallback();
                },
                cancel: function () {
                    cancelCallback && cancelCallback();
                }
            });

            wx.onMenuShareQZone({
                title: title, 
                desc: description, 
                link: link, 
                imgUrl: imgUrl, 
                success: function () {
                    successCallback && successCallback();
                },
                cancel: function () {
                    cancelCallback && cancelCallback();
                }
            });

        });
    };

    WeixinJsSdk.ready = function (func) {
        if (isInitialized) {
            func && func();
        } else {
            initializeWeixin(func);
        }
    };

    var initializeWeixin = function (callback) {
        getConfigOptions(function (options) {
            wx.config({
                debug: false,
                appId: options.appId,
                timestamp: options.timestamp,
                nonceStr: options.nonceStr,
                signature: options.signature,
                jsApiList: jsApiList
            });

            wx.ready(function () {
                isInitialized = true;
                callback && callback();
            });

            wx.error(function (res) {
                var msg = "微信出错啦：";
                for (var p in res) {
                    msg += '\r\n' + p + ": " + res[p];
                }
                alert(msg);
            });
        });
    };

    function getConfigOptions(callback) {
        MvcSolution.ajax.post(urlConfigOptions, null, function (result) {
            if (result.Success) {
                var value = result.Value;
                callback({
                    appId: value.AppId,
                    timestamp: value.Timestamp,
                    nonceStr: value.NonceStr,
                    signature: value.Signature
                });
            } else {
                MvcSolution.notification.toast(result.Message);
            }
        });
    };

})();