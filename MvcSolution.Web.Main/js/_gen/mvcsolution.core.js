var mvcsolution = {
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
        
    }
};
Array.prototype.each = function (func) {
    for (var i = 0; i < this.length; i++) {
        var item = this[i];
        var result = func.call(item, i, item);
        if (result == false) {
            return;
        }
    }
};

Array.prototype.sum = function(propertyOrFunc) {
    var total = 0;
    var isFunc = typeof(propertyOrFunc) == "function";
    this.each(function() {
        if (isFunc) {
            total += propertyOrFunc.call(this);
        } else {
            var value = this[propertyOrFunc];
            if (value != undefined) {
                total += value * 1;
            }
        }
    });
    return total;
};

Array.prototype.where = function (predicateFunction) {
    var results = new Array();
    this.each(function() {
        if (predicateFunction.call(this)) {
            results.push(this);
        }
    });
    return results;
};

Array.prototype.orderBy = function (property, compare) {
    var items = this;
    for (var i = 0; i < items.length - 1; i++) {
        for (var j = 0; j < items.length - 1 - i; j++) {
            if (isFirstGreaterThanSecond(items[j], items[j + 1])) {
                var temp = items[j + 1];
                items[j + 1] = items[j];
                items[j] = temp;
            }
        }
    }
    function isFirstGreaterThanSecond(first, second) {
        if (compare != undefined) {
            return compare(first, second);
        }
        else if (property == undefined || property == null) {
            return first > second;
        }
        else {
            return first[property] > second[property];
        }
    }

    return items;
};

Array.prototype.orderByDescending = function (property, compare) {
    var items = this;
    for (var i = 0; i < items.length - 1; i++) {
        for (var j = 0; j < items.length - 1 - i; j++) {
            if (!isFirstGreaterThanSecond(items[j], items[j + 1])) {
                var temp = items[j + 1];
                items[j + 1] = items[j];
                items[j] = temp;
            }
        }
    }
    function isFirstGreaterThanSecond(first, second) {
        if (compare != undefined) {
            return compare(first, second);
        }
        else if (property == undefined || property == null) {
            return first > second;
        }
        else {
            return first[property] > second[property];
        }
    }
    return items;
};

Array.prototype.groupBy = function (predicate) {
    var results = [];
    var items = this;

    var keys = {}, index = 0;
    for (var i = 0; i < items.length; i++) {
        var selector;
        if (typeof predicate === "string") {
            selector = items[i][predicate];
        } else {
            selector = predicate(items[i]);
        }
        if (keys[selector] === undefined) {
            keys[selector] = index++;
            results.push({ key: selector, value: [items[i]] });
        } else {
            results[keys[selector]].value.push(items[i]);
        }
    }
    return results;
};

Array.prototype.skip = function (count) {
    var items = new Array();
    for (var i = count; i < this.length; i++) {
        items.push(this[i]);
    }
    return items;
};

Array.prototype.take = function (count) {
    var items = new Array();
    for (var i = 0; i < this.length && i < count; i++) {
        items.push(this[i]);
    }
    return items;
};

Array.prototype.firstOrDefault = function (predicateFunction) {
    if (this.length == 0) {
        return null;
    }
    if (predicateFunction == undefined) {
        return this[0];
    }
    var item = null;
    this.each(function () {
        if (predicateFunction.call(this)) {
            item = this;
            return false;
        }
        return true;
    });
    return item;
};

Array.prototype.any = function(predicateFunction) {
    if (predicateFunction == undefined) {
        return this.length > 0;
    }
    var hasAny = false;
    this.each(function() {
        if (predicateFunction.call(this)) {
            hasAny = true;
            return false;
        }
        return true;
    });
    return hasAny;
};

Array.prototype.select = function (predicateFunction) {
    if (predicateFunction == undefined) {
        throw "parameter predicateFunction cannot be null or undefined";
    }
    var items = [];
    this.each(function () {
        items.push(predicateFunction.call(this));
    });
    return items;
};

(function ($) {
    $.fn.busy = function (options) {

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

$.busy = function(options) {
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
        $close.click(function() {
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


Date.prototype.addDays = function (number) {
    var date = new Date(this);
    date.setDate(date.getDate() + number);
    return date;
};

Date.prototype.addMonths = function (number) {
    var date = new Date(this);
    date.setMonth(date.getMonth() + number);
    return date;
};

Date.prototype.addYears = function (number) {
    var date = new Date(this);
    date.setFullYear(date.getFullYear() + number);
    return date;
};

Date.prototype.toWeekStart = function (startsFromSunday) {
    var dayOfWeek = this.getDay();
    if (startsFromSunday) {
        return this.addDays(-dayOfWeek);
    }
    if (dayOfWeek == 0) {
        dayOfWeek = 7;
    }
    return this.addDays(1 - dayOfWeek);
};

Date.prototype.toWeekEnd = function (startsFromSunday) {
    var dayOfWeek = this.getDay();
    if (startsFromSunday) {
        return this.addDays(6 - dayOfWeek);
    }
    if (dayOfWeek == 0) {
        dayOfWeek = 7;
    }
    return this.addDays(7 - dayOfWeek);
};

Date.prototype.toMonthStart = function () {
    var date = new Date(this);
    date.setDate(1);
    return date;
};

Date.prototype.toMonthEnd = function () {
    var date = new Date(this);
    date.setMonth(date.getMonth() + 1);
    date.setDate(0);
    return date;
};

Date.prototype.toYearStart = function () {
    var date = new Date(this);
    date.setMonth(0);
    date.setDate(1);
    return date;
};

Date.prototype.toYearEnd = function () {
    var date = new Date(this);
    date.setMonth(12);
    date.setDate(0);
    return date;
};

Date.prototype.toUSDateString = function () {
    return (this.getMonth() + 1) + "/" + this.getDate() + "/" + this.getFullYear();
};


Date.parseFromJsonFormat = function(dateStr) {
    // "/Date(1383667450669)/"
    var str = dateStr.substring(6, dateStr.length - 2);
    return new Date(str * 1);
};
mvcsolution.guid = {
    empty: function() {
        return '00000000-0000-0000-0000-000000000000';
    }
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

mvcsolution.ajax.busyPost = function (url, data, successCallBack, busyContent) {
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
                mvcsolution.notification.toastError("Error occurred!");
            }
        }
    });
};

mvcsolution.ajax.load = function (wrapper, url, successCallback, message) {
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
                mvcsolution.ajax.load(wrapper, url, successCallback);
            });
        }
    });
};

mvcsolution.ajax.getJSON = function (url, successCallback, errorCallback) {

    function returnError(error) {
        if (errorCallback == undefined) {
            mvcsolution.notification.alertError(error);
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
mvcsolution.notification.toastSuccess = function (message) {
    $.toast({
        message: message,
        css: "success"
    });
};

mvcsolution.notification.toastError = function (message) {
    if (message == undefined || message == "") {
        message = "Error occurred!";
    }
    $.toast({
        message: message,
        css: "error",
        displayDuration: 3000
    });
};

mvcsolution.notification.toastInfo = function (message) {
    $.toast({
        message: message,
        css: "info"
    });
};


mvcsolution.notification.messageBox = function (title, content, buttons, onClose, width, height) {
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
        close: function () {
            $(this).dialog("destroy");
            if (onClose != undefined && onClose != null) {
                onClose();
            }
        }
    });
};

mvcsolution.notification.alert = function (title, content, onClose, width, height) {
    mvcsolution.notification.messageBox(title, content, {
        "OK": function () {
            $(this).dialog("close");
        }
    }, onClose, width, height);
};

mvcsolution.notification.alertError = function (message, title) {
    if (title == undefined) {
        title = "Error Ocurred!";
    }
    if (message == undefined || message == '') {
        message = "We are sorry. An unknown error occurred.";
    }
    message = "<p class='error'>" + message + "</p>";
    mvcsolution.notification.alert(title, message);
};

mvcsolution.notification.dialog = function (id, title, options, destoryOnClose) {
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
            };
        } else {
            options.close = function () {
                options.close();
                $dialog.dialog("destroy");
            };
        }
    }
    var createFunc = options.create;
    options.create = function() {
        createFunc && createFunc();
        (function(idd) {
            setTimeout(function() {
                $('#' + idd).css("height", "auto");
            });
        })(id);
    };
    $dialog.dialog(options);
    $dialog.dialog("open");
};


mvcsolution.utils.bindDatePicker = function (input) {
    $(input).datepicker();
};

mvcsolution.utils.onEnterKeydown = function (inputSelector, callback) {
    $(inputSelector).keydown(function (e) {
        if (e.keyCode == 13) {
            callback();
        }
    });
};

mvcsolution.utils.getCookieValue = function (name) {
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

mvcsolution.utils.centerDialog = function (selector) {
    var $dialogs = selector ? $(selector).closest(".ui-dialog") : $(".ui-dialog:visible");
    $dialogs.each(function () {
        var $dialog = $(this);
        var height = $dialog.height();
        var winHeight = $(window).height();
        var top = (winHeight - height) / 2;
        top = Math.max(top, 0);
        $dialog.animate({ top: top }, 200);
    });
};

mvcsolution.utils.parseUnobtrusive = function (formSelector) {
    $.validator.unobtrusive.parse($(formSelector));
};


$(document).ready(function () {
    for (var handler in mvcsolution.documentReady) {
        mvcsolution.documentReady[handler]();
    }
});