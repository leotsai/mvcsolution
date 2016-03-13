(function() {
    if (window.Slider) {
        return;
    }
    window.Slider = function (itemsSelector, btnLeftSelector, btnRightSelector) {
        this.itemWidth = 80;
        this.itemsSelector = itemsSelector;
        this.btnLeftSelector = btnLeftSelector;
        this.btnRightSelector = btnRightSelector;
        this._isMoving = false;
        this.isInitialized = false;
        this.initWithAnimation = false;
    };

    Slider.prototype = {
        init: function() {
            var me = this;
            var $btnLeft = $(me.btnLeftSelector);
            var $btnRight = $(me.btnRightSelector);
            me.setItemAndWrapperWidth();
            $btnLeft.click(function() {
                if ($(this).hasClass("disabled")) {
                    return;
                }
                me.go(-1);
            });
            $btnRight.click(function () {
                if ($(this).hasClass("disabled")) {
                    return;
                }
                me.go(1);
            });
            me.slideToHiddenSelectedIfNecessary(function() {
                me.setNavButtonStatus(me.getCurrentLeft());
                me.isInitialized = true;
            });
        },
        setItemAndWrapperWidth: function() {
            var $wrapper = $(this.itemsSelector);
            var $childItems = this.getWrapperChildren();
            var itemsCount = $childItems.length;
            $childItems.width(this.itemWidth + "px");
            $wrapper.width(itemsCount * this.itemWidth + "px");
        },
        getWrapperChildren: function() {
            return $(this.itemsSelector).children();
        },
        getWrapperContainer: function() {
            return $(this.itemsSelector).parent();
        },
        go: function (step) {
            if (this._isMoving) {
                return;
            }
            var $wrapper = $(this.itemsSelector);
            var $container = this.getWrapperContainer();
            var containerWidth = $container.width();
            var movingItems = Math.floor(containerWidth / this.itemWidth);
            var movingWidth = movingItems * this.itemWidth;
            var left = this.getCurrentLeft();
            left = left - step * movingWidth;
            if (left > 0) {
                left = 0;
            }
            var min = containerWidth - $wrapper.width();
            if (left < min) {
                left = min;
            }
            this.move(left);
            this.setNavButtonStatus(left);
        },
        getCurrentLeft: function() {
            return $(this.itemsSelector).position().left;
        },
        move: function(left, callback) {
            if (this._isMoving) {
                return;
            }
            if (this.isInitialized == false && this.initWithAnimation == false) {
                $(this.itemsSelector).css("left", left + "px");
                callback && callback();
            } else {
                var me = this;
                this._isMoving = true;
                $(this.itemsSelector).animate({
                    left: left
                }, 500, function () {
                    me._isMoving = false;
                    callback && callback();
                });
            }
        },
        setNavButtonStatus: function (left) {
            var $btnLeft = $(this.btnLeftSelector);
            var $btnRight = $(this.btnRightSelector);
            var $wrapper = $(this.itemsSelector);
            var $container = this.getWrapperContainer();
            var min = $container.outerWidth() - $wrapper.width();
            if (left >= 0) {
                $btnLeft.cssDisable();
            } else {
                $btnLeft.cssEnable();
            }
            if (left <= min) {
                $btnRight.cssDisable();
            } else {
                $btnRight.cssEnable();
            }
        },
        slideToHiddenSelectedIfNecessary: function(callback) {
            if (this.isSelectedVisible()) {
                callback && callback();
                return;
            }
            this.slideToSelected(callback);
        },
        isSelectedVisible: function() {
            var $selectedItem = this.getWrapperChildren().find(".selected");
            if ($selectedItem.length === 0) {
                $selectedItem = $(this.itemsSelector).children(".selected");
            }
            if ($selectedItem.length === 0) {
                return false;
            }
            var $container = this.getWrapperContainer();
            var left = this.getCurrentLeft();
            var min = -$selectedItem.index() * this.itemWidth;
            var max = min + $container.width() - this.itemWidth;
            return left >= min && left <= max;
        },
        slideToSelected: function(callback) {
            var me = this;
            var $selectedItem = this.getWrapperChildren().find(".selected");
            if ($selectedItem.length === 0) {
                $selectedItem = $(this.itemsSelector).children(".selected");
            }
            if ($selectedItem.length === 0) {
                callback && callback();
                return;
            }
            var left = -$selectedItem.index() * this.itemWidth;
            var containerWidth = this.getWrapperContainer().width();
            var itemsWidth = $(this.itemsSelector).width();

            var min = containerWidth - itemsWidth;
            if (left < min) {
                left = min;
            }

            me.move(left, function() {
                me.setNavButtonStatus(left);
                callback && callback();
            });
        }
    };
})();