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