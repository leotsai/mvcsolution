(function() {
    if (window.mvcsolution) {
        return;
    }
    window.mvcsolution = {
        ajax: {

        },
        notification: {

        },
        utils: {
            validator: {

            }
        },

        debug: true
    };

    window.page = {

    };

    $(document).ready(function() {
        if (window.page && page.onLoaded) {
            page.onLoaded();
        }
    });
})();
(function() {
    mvcsolution.ajax.busyPost = function(url, data, successCallBack, busyContent) {
        data = "ajax=true&" + data;
        $.ajax({
            type: "post",
            url: url,
            contentType: "application/x-www-form-urlencoded",
            data: data,
            beforeSend: function(request) {
                var op = {
                    isBusy: true,
                    busyContent: busyContent == undefined ? 'Loading...' : busyContent,
                    request: request,
                    showCancel: true,
                    delay: 500,
                    modal: true
                };
                $.busy(op);
            },
            success: function(result) {
                $.busy(false);
                successCallBack(result);
            },
            error: function(err) {
                $.busy(false);
                if (err.status != 0) {
                    mvcsolution.notification.toastError("Error occurred!");
                }
            }
        });
    };

    mvcsolution.ajax.post = function(url, data, successCallBack) {
        data = "ajax=true&" + data;
        $.ajax({
            type: "post",
            url: url,
            contentType: "application/x-www-form-urlencoded",
            data: data,
            success: function(result) {
                successCallBack(result);
            },
            error: function(err) {
                if (err.status != 0) {
                    mvcsolution.notification.toastError("Error occurred!");
                }
            }
        });
    };

    mvcsolution.ajax.load = function(wrapper, url, successCallback, message) {
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
            success: function(result) {
                $wrapper.html(result);
                if (successCallback != undefined && successCallback != null) {
                    successCallback();
                }
            },
            error: function() {
                $wrapper.html("<div class='load-error'><span class='error'>Error occuurred.</span><a href='javascript:void(0);'>Re-try</a></div>");
                $wrapper.find("a").click(function() {
                    mvcsolution.ajax.load(wrapper, url, successCallback);
                });
            }
        });
    };
})();
(function($) {
    $.fn.busy = function(options) {

        var busyOptions = function(isbusy) {
            this.isBusy = isbusy;
            this.busyContent = "Loading...";
            this.request = null;
            this.showCancel = true;
            this.modal = true;
            this.delay = 0;
            this.css = "";
        };

        busyOptions.Fix = function(obj) {
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
                cancelLink.click(function() {
                    if (opts.request != undefined && opts.request != null) {
                        opts.request.abort();
                    }
                    container.remove();
                });
            }
            $("<div class='busy-text'></div>").appendTo(contentWrapper);
            return container;
        }

        return $(this).each(function(i, elem) {
            var item = $(elem);
            options = busyOptions.Fix(options);
            var timer = null;
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
                    } else {
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

                timer = setTimeout(function() {
                    if (options.modal == false) {
                        indicator.css("display", "table");
                    } else {
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

$.busy = function(options) {
    $("body").busy(options);
};

$.toast = function(options) {
    var toastOptions = function(message) {
        this.message = message;
        this.showDuration = 500;
        this.displayDuration = 2000;
        this.hideDuration = 500;
        this.css = "";
        this.showClose = true;
    };

    toastOptions.Fix = function(obj) {
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
        $close.click(function() {
            $toast.animate({
                top: "-=35",
                opacity: 0
            }, {
                duration: options.hideDuration,
                complete: function() {
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
        complete: function() {
            setTimeout(function() {
                $toast.animate({
                    top: "-=35",
                    opacity: 0
                }, {
                    duration: options.hideDuration,
                    complete: function() {
                        $toast.remove();
                    }
                });
            }, options.displayDuration);
        }
    });
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

    jQuery.prototype.disableTextSelect = function() {
        return this.each(function () {
            jQuery(this).bind('selectstart', function() {
                 return false;
            });
        });
    };

    jQuery.prototype.tabs = function() {
        return this.each(function() {
            if (!$(this).hasClass("tabs")) {
                $(this).addClass("tabs");
            }
            $(this).find("ul.tab-headers > li").last().addClass("last");
            $(this).find("ul.tab-headers > li").click(function() {
                var $li = $(this);
                if ($li.hasClass("selected")) {
                    return;
                }
                var $tabs = $li.closest(".tabs");
                $tabs.find("ul.tab-headers > li.selected").removeClass("selected");
                $li.addClass("selected");

                var index = $li.index();
                var $content = $tabs.find("ul.tab-contents > li").eq(index);
                $tabs.find("ul.tab-contents > li").removeClass("selected");
                $content.addClass("selected");
            });
        });
    };
})();
(function() {
    mvcsolution.notification.toastSuccess = function(message) {
        $.toast({
            message: message,
            css: "success"
        });
    };

    mvcsolution.notification.toastError = function(message) {
        if (message == undefined || message == "") {
            message = "Error occurred!";
        }
        $.toast({
            message: message,
            css: "error",
            displayDuration: 3000
        });
    };

    mvcsolution.notification.toastInfo = function(message) {
        $.toast({
            message: message,
            css: "info"
        });
    };


    mvcsolution.notification.messageBox = function(title, content, buttons, onClose, width, height) {
        var id = "messagebox" + new Date().getTime();
        width = width == undefined ? 400 : width;
        height = height == undefined ? 250 : height;
        var $dialog = $("<div id='" + id + "' class='message-box'>" + content + "</div>");
        $dialog.appendTo("body");
        $dialog.dialog({
            modal: true,
            title: title,
            width: width,
            height: height,
            buttons: buttons,
            close: function() {
                $(this).dialog("destroy");
                if (onClose != undefined && onClose != null) {
                    onClose();
                }
            }
        });
    };

    mvcsolution.notification.alert = function(title, content, onClose, width, height) {
        mvcsolution.notification.messageBox(title, content, {
            "OK": function() {
                $(this).dialog("close");
            }
        }, onClose, width, height);
    };

    mvcsolution.notification.alertError = function(message, title) {
        if (title == undefined) {
            title = "Error Ocurred!";
        }
        if (message == undefined || message == '') {
            message = "We are sorry. An unknown error occurred.";
        }
        message = "<p class='error'>" + message + "</p>";
        mvcsolution.notification.alert(title, message);
    };

    mvcsolution.notification.dialog = function(id, title, options, destoryOnClose) {
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
                options.close = function() {
                    $dialog.dialog("destroy");
                    $dialog.remove();
                };
            } else {
                var close = options.close;
                options.close = function () {
                    close();
                    $dialog.dialog("destroy");
                    $dialog.remove();
                };
            }
        }
        var createFunc = options.create;
        options.create = function() {
            createFunc && createFunc();
            //(function (idd) {
            //    setTimeout(function () {
            //        $('#' + idd).css("height", "auto");
            //    });
            //})(id);
        };
        $dialog.dialog(options);
        $dialog.dialog("open");
    };
})();
(function() {
    mvcsolution.utils.bindDatePicker = function (input) {
        $(input).datepicker({
            dateFormat: "yy-mm-dd",
            monthNames: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"]
        });
    };

    mvcsolution.utils.onEnterKeydown = function(inputSelector, callback) {
        $(inputSelector).keydown(function(e) {
            if (e.keyCode == 13) {
                callback();
            }
        });
    };

    mvcsolution.utils.getCookieValue = function(name) {
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

    mvcsolution.utils.removeCookie = function (name) {
        var value = this.getCookieValue(name);
        if (value != null) {
            var exp = new Date();
            exp.setFullYear(2000);
            document.cookie = name + "=" + value + ";path=/;expires=" + exp.toGMTString();
        }
    };

    mvcsolution.utils.centerDialog = function(selector) {
        var $dialogs = selector ? $(selector).closest(".ui-dialog") : $(".ui-dialog:visible");
        $dialogs.each(function() {
            var $dialog = $(this);
            var height = $dialog.height();
            var winHeight = $(window).height();
            var top = (winHeight - height) / 2;
            top = Math.max(top, 0);
            $dialog.animate({ top: top }, 200);
        });
    };

    mvcsolution.utils.parseUnobtrusive = function(formSelector) {
        $.validator.unobtrusive.parse($(formSelector));
    };

    mvcsolution.utils.validator.checkEmail = function(val) {
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        return reg.test(val);
    };

    mvcsolution.utils.validator.checkPassword = function(val) {
        var reg = /^[\@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{1,}$/;
        return reg.test(val);
    };

    mvcsolution.utils.validator.checkDomain = function (domain) {
        var reg = /^(http\:\/\/|https\:\/\/)?[0-9a-zA-Z]+[0-9a-zA-Z\.-]*\.[a-zA-Z]{2,4}\/?$/;
        return reg.test(domain);
    };

    mvcsolution.utils.validator.checkUrl = function (url) {
        var reg = /(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?/;
        return reg.test(url);
    };
})();