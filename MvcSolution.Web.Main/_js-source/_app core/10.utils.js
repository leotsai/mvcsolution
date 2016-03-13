(function() {
    MvcSolution.utils = {
        onEnterKeydown: function (inputSelector, callback) {
            $(inputSelector).keydown(function (e) {
                if (e.keyCode == 13) {
                    callback();
                }
            });
        },
        getCookieValue: function (name) {
            var cookieValue = null;
            if (document.cookie && document.cookie != '') {
                var cookies = document.cookie.split(';');
                for (var i = 0; i < cookies.length; i++) {
                    var cookie = jQuery.trim(cookies[i]);
                    if (cookie.substring(0, name.length + 1) == (name + '=')) {
                        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                        break;
                    }
                }
            }
            return cookieValue;
        },
        removeCookie: function (name) {
            var value = this.getCookieValue(name);
            if (value != null) {
                var exp = new Date();
                exp.setFullYear(2000);
                document.cookie = name + "=" + value + ";path=/;expires=" + exp.toGMTString();
            }
        },
        setCookie: function (name, value, expire) {
            document.cookie = name + "=" + value + ";path=/;expires=" + expire.toGMTString();
        },
        getLngLat: function(callback, errorCallback, usingBaidu) {
            var me = this;
            this._getLngLatInner(function (lng, lat) {
                callback && callback(lng, lat);
                me._updateUserLocation(lng, lat);
            }, errorCallback, usingBaidu);
        },
        _getLngLatInner: function (callback, errorCallback, usingBaidu) {
            usingBaidu = usingBaidu == undefined ? true : usingBaidu;
            var value = this.getCookieValue("LOCATION");
            if (value != null) {
                var lngLat = value.split('=');
                callback && callback(lngLat[0] * 1, lngLat[1] * 1);
                return;
            }

            function setLocationCookie(lng, lat) {
                var time = new Date();
                time.setMinutes(time.getMinutes() + 10);
                MvcSolution.utils.setCookie("LOCATION", lng + "=" + lat, time);
            };

            if (usingBaidu) {
                if (window.BMap == undefined) {
                    MvcSolution.notification.toast("找不到百度定位组件");
                    return;
                }
                var geolocation = new BMap.Geolocation();
                geolocation.getCurrentPosition(function(r) {
                    if (this.getStatus() === BMAP_STATUS_SUCCESS) {
                        setLocationCookie(r.point.lng, r.point.lat);
                        callback && callback(r.point.lng, r.point.lat);
                    } else {
                        errorCallback && errorCallback("无法获取定位");
                    }
                }, { enableHighAccuracy: true });
            } else {
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function(position) {
                        setLocationCookie(position.coords.longitude, position.coords.latitude);
                        callback && callback(position.coords.longitude, position.coords.latitude);
                    });
                } else {
                    errorCallback && errorCallback("您的系统不支持GPS定位");
                }
            }
        },
        bindWeixinImagesViewer: function($imgs) {
            var srcs = [];
            $imgs.each(function () {
                srcs.push(location.origin + $(this).attr("src").toLowerCase().replace(/s80x80/g, 'full'));
            });

            (function ($imgItems, srcArray) {
                $imgItems.load(function () {
                    var w = this.naturalWidth;
                    var h = this.naturalHeight;
                    var $img = $(this);
                    if (w > h) {
                        $img.height("80px");
                        w = 80 * w / h;
                        $img.css("margin-left", ((80 - w) / 2) + "px");
                    } else {
                        $img.width("80px");
                        h = 80 * h / w;
                        $img.css("margin-top", ((80 - h) / 2) + "px");
                    }
                    $img.css("max-width", "inherit");
                    $img.css("max-height", "inherit");
                }).click(function (e) {
                    e.stopPropagation();
                    if (!WeixinJsSdk.isInWeixin()) {
                        return;
                    }
                    var src = location.origin + $(this).attr("src").toLowerCase().replace(/s80x80/g, 'full');
                    WeixinJsSdk.ready(function () {
                        wx.previewImage({
                            current: src,
                            urls: srcArray
                        });
                    });
                });
            })($imgs, srcs);
        },
        _updateUserLocation: function(lng, lat) {
            MvcSolution.ajax.post("/app/user/cacheLocation", "lng=" + lng + "&lat=" + lat, function() {

            });
        },
        getImgUrl: function(key, size) {
            if (key == null || key === '') {
                return '/_storage/css/404.png';
            }
            if (key.indexOf('/') === -1) {
                return '/img/' + size + '/' + key;
            }
            return key;
        }
    };
})();