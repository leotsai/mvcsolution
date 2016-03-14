var MvcSolution = {
    ajax: {
        
    },
    notification: {        
        
    },
    shell: {        
        
    },
    documentReady:{
        
    },
    utils: {
        
    },
    pages: {
        
    },
    ckey: null
};
(function ($) {
    $.fn.busy = function (options) {
        var timer = null;
        var busyOptions = function (isbusy) {
            this.isBusy = isbusy;
            this.busyContent = "Loading...";
            this.request = null;
            this.showCancel = true;
            this.modal = true;
            this.delay = 0;
            this.css = "";
        };

        busyOptions.Fix = function (obj) {
            if (obj.constructor == Boolean) {
                return new busyOptions(obj);
            }
            var result = new busyOptions(true);
            for (property in obj) {
                result[property] = obj[property];
            }
            return result;
        };

        function createIndicator(opts) {
            var container = $("<div class='busy-indicator'></div>");
            if (opts.css != null && opts.css != "") {
                container.addClass(opts.css);
            }
            var contentWrapper = $("<div class='busy-content'></div>");
            contentWrapper.appendTo(container);
            if (opts.showCancel) {
                var cancelLink = $("<a href='javascript:void(0)' title='cancel' class='busy-cancel'></a>");
                cancelLink.appendTo(contentWrapper);
                cancelLink.click(function () {
                    if (opts.request != undefined && opts.request != null) {
                        opts.request.abort();
                    }
                    container.remove();
                });
            }
            $("<div class='busy-text'></div>").appendTo(contentWrapper);
            return container;
        }

        return $(this).each(function (i, elem) {
            var item = $(elem);
            options = busyOptions.Fix(options);

            if (options.isBusy) {
                var indicator = item.find(".busy-indicator");
                if (indicator.length == 0) {
                    indicator = createIndicator(options);
                    item.prepend(indicator);
                    var $content = item.find(".busy-content");
                    var contentHeight = $content.height();
                    var contentWidth = $content.width();
                    var windowHeight = $(window).height();
                    var windowWidth = $(window).width();
                    if (options.modal) {
                        indicator.addClass("busy-modal");
                        var itemHeight = item.height();
                        var itemWidth = item.width();
                        if (item.is("body")) {
                            indicator.css("position", "absolute");
                            var itemContentsHeight = 0;
                            var itemContentsWidth = 0;
                            if (item.find("#wrapper").length > 0) {
                                itemContentsHeight = item.find("#wrapper").height();
                                itemContentsWidth = item.find("#wrapper").width();
                            }
                            indicator.height(Math.max(itemHeight, windowHeight, itemContentsHeight) + "px");
                            indicator.width(Math.max(itemWidth, windowWidth, itemContentsWidth) + "px");
                            $content.css("position", "fixed");
                            contentWidth = $content.width();
                            $content.css("top", (windowHeight - contentHeight) / 2 + "px");
                            $content.css("left", (windowWidth - contentWidth) / 2 + "px");
                        } else {
                            indicator.height(itemHeight + "px");
                            indicator.width(itemWidth + "px");
                            $content.css("top", (indicator.height() - contentHeight) / 2 + "px");
                        }
                    }
                    else {
                        var wrapperHeight = 0;
                        var wrapperWidth = 0;
                        if (item.is("body")) {
                            indicator.css("position", "fixed");
                            wrapperHeight = windowHeight;
                            wrapperWidth = windowWidth;
                        } else {
                            wrapperHeight = item.height();
                            wrapperWidth = item.width();
                        }

                        var top = (wrapperHeight - contentHeight) / 2;
                        var left = (wrapperWidth - contentWidth) / 2;
                        indicator.css("left", left + "px");
                        indicator.css("top", top + "px");
                    }
                }
                indicator.find(".busy-text").html(options.busyContent);
                indicator.hide();

                timer = setTimeout(function () {
                    if (options.modal == false) {
                        indicator.css("display", "table");
                    }
                    else {
                        indicator.show();
                    }
                }, options.delay);
            } else {
                item.find(".busy-indicator").remove();
                clearTimeout(timer);
            }
        });
    };
})(jQuery);

$.busy = function (options) {
    $("body").busy(options);
};

$.toast = function (options) {
    var toastOptions = function (message) {
        this.message = message;
        this.showDuration = 500;
        this.displayDuration = 2000;
        this.hideDuration = 500;
        this.css = "";
        this.showClose = true;
    };

    toastOptions.Fix = function (obj) {
        if (obj.constructor == String) {
            return new toastOptions(obj);
        }
        var result = new toastOptions("");
        for (property in obj) {
            result[property] = obj[property];
        }
        return result;
    };


    options = toastOptions.Fix(options);
    var $toast = $("<div class='toast'>" + options.message + "</div>");
    if (options.css != "") {
        $toast.addClass(options.css);
    }
    $toast.appendTo("body");
    if (options.showClose) {
        var $close = $("<a class='close' href='javascript:void(0)'></a>");
        $close.appendTo($toast);
        $close.click(function () {
            $toast.animate({
                top: "-=35",
                opacity: 0
            }, {
                duration: options.hideDuration,
                complete: function () {
                    $toast.remove();
                }
            });
        });
    }
    var centerX = $(window).width() / 2;
    var centerY = $(window).height() / 2;
    var left = centerX - $toast.width() / 2;
    var top = centerY - $($toast).height() / 2 + 50;
    $toast.css("top", top + "px");
    $toast.css("left", left + "px");
    $toast.animate({
        top: "-=50",
        opacity: 1
    }, {
        duration: options.showDuration,
        complete: function () {
            setTimeout(function () {
                $toast.animate({
                    top: "-=35",
                    opacity: 0
                }, {
                    duration: options.hideDuration,
                    complete: function () {
                        $toast.remove();
                    }
                });
            }, options.displayDuration);
        }
    });
};

MvcSolution.guid = {
    empty: function() {
        return '00000000-0000-0000-0000-000000000000';
    }
};
(function() {
    jQuery.prototype.cssDisable = function() {
        return this.each(function() {
            $(this).addClass("disabled");
        });
    };

    jQuery.prototype.cssEnable = function () {
        return this.each(function () {
            $(this).removeClass("disabled");
        });
    };
})();
if (!window.Number) {
    window.Number = function() {

    };
}

Number.get2Digits = function(number) {
    return number < 10 ? "0" + number : number;
};
var page = {
    widgets: {        
        
    },
    init: function() {
        this.onLoaded();
        for (var p in this.widgets) {
            var widget = this.widgets[p];
            widget.init && widget.init();
        }
    },
    onLoaded: function() {

    }
};

$(document).ready(function () {
    page.init();
});

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

MvcSolution.notification.toastSuccess = function (message) {
    $.toast({
        message: message,
        css: "success"
    });
};

MvcSolution.notification.toastError = function (message) {
    if (message == undefined || message == "") {
        message = "Error";
    }
    $.toast({
        message: message,
        css: "error",
        displayDuration: 3000
    });
};

MvcSolution.notification.toastInfo = function (message) {
    $.toast({
        message: message,
        css: "info"
    });
};


MvcSolution.notification.messageBox = function (title, content, buttons, onClose, width, height) {
    var id = "messagebox" + new Date().getTime();
    width = width == undefined ? 400 : width;
    height = height == undefined ? 250 : height;
    var $dialog = $("<div id='" + id + "' class='message-box'>" + content + "</div>");
    $dialog.appendTo("body");
    $dialog.dialog({
        modal: true,
        title: title,
        width: width,
        minHeight: height,
        height: 'auto',
        buttons: buttons,
        close: function () {
            $(this).dialog("destroy").remove();
            if (onClose != undefined && onClose != null) {
                onClose();
            }
        }
    });
};

MvcSolution.notification.alert = function (title, content, onClose, width, height) {
    MvcSolution.notification.messageBox(title, content, {
        "OK": function () {
            $(this).dialog("close");
        }
    }, onClose, width, height);
};

MvcSolution.notification.alertError = function (message, title) {
    if (title == undefined) {
        title = "Error";
    }
    if (message == undefined || message == '') {
        message = "The server returned an unknown error";
    }
    message = "<p class='error'>" + message + "</p>";
    MvcSolution.notification.alert(title, message);
};

MvcSolution.notification.dialog = function (id, title, options, destoryOnClose) {
    destoryOnClose = destoryOnClose == undefined ? true : destoryOnClose;
    var $dialog = $("#" + id);
    if ($dialog.length == 0) {
        $dialog = $("<div id='" + id + "' class='dialog' title='" + title + "'></div>").appendTo("body");
    }
    if (options.autoOpen == undefined) {
        options.autoOpen = false;
    }
    if (options.modal == undefined) {
        options.modal = true;
    }
    if (destoryOnClose) {
        if (options.close == undefined) {
            options.close = function () {
                $dialog.dialog("destroy");
                $dialog.remove();
            };
        } else {
            (function(closeFunc) {
                options.close = function () {
                    closeFunc();
                    $dialog.dialog("destroy");
                    $dialog.remove();
                };
            })(options.close);
        }
    }
    var createFunc = options.create;
    options.create = function() {
        createFunc && createFunc();
        //(function(idd) {
        //    setTimeout(function() {
        //        $('#' + idd).css("height", "auto");
        //    });
        //})(id);
    };
    $dialog.dialog(options);
    $dialog.dialog("open");
};
(function() {
    var urls = {
        myNotifications: '/me/me/notifications'
    };
    
    MvcSolution.utils.bindDatePicker = function (input) {
        $(input).datepicker({
            dateFormat: "yy-m-d",
            monthNames: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            monthNamesShort: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
            changeYear: true,
            changeMonth: true
        });
    };

    MvcSolution.utils.onEnterKeydown = function (inputSelector, callback) {
        $(inputSelector).keydown(function (e) {
            if (e.keyCode == 13) {
                callback();
            }
        });
    };

    MvcSolution.utils.getCookieValue = function (name) {
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
    };

    MvcSolution.utils.removeCookie = function (name) {
        var value = this.getCookieValue(name);
        if (value != null) {
            var exp = new Date();
            exp.setFullYear(2000);
            document.cookie = name + "=" + value + ";path=/;expires=" + exp.toGMTString();
        }
    };

    MvcSolution.utils.setCookie = function (name, value, expire) {
        document.cookie = name + "=" + value + ";path=/;expires=" + expire.toGMTString();
    };

})();
(function() {
    
    MvcSolution.documentReady.setCkey = function() {
        
    };


})();

$(document).ready(function () {
    for (var handler in MvcSolution.documentReady) {
        MvcSolution.documentReady[handler]();
    }
});