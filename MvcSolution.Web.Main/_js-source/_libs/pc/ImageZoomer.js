(function() {
    if (window.ImageZoomer) {
        return;
    }
    window.ImageZoomer = function(wrapperSelector, zoomSelector) {
        this.$wrap = $(wrapperSelector);
        this.$zoom = $(zoomSelector);

        this.$img = null;
        this.fullWidth = 0;
        this.fullHeight = 0;
        this.rate = 0;
        this.bestRate = 0;
    };

    ImageZoomer.prototype = {
        init: function() {
            var me = this;
            me.$zoom.click(function() {
                if (me.rate == me.bestRate) {
                    me.zoomToRate(1);
                } else {
                    me.zoomToRate(me.bestRate);
                }
                me.centerImage();
            });
            me.enableWheelZoom();
        },
        loadImage: function(url) {
            var me = this;
            me.$img = null;
            me.$wrap.html("<div class='loading'>加载中...</div>");
            $("<img/>").attr("src", url).load(function () {
                me.$img = $(this);
                me.fullWidth = this.width;
                me.fullHeight = this.height;
                me.$wrap.empty().append(me.$img);
                me.zoomToBest();
                me.enableDrag();
            });
        },
        zoomToBest: function() {
            var rate = Math.max(this.fullWidth / this.$wrap.width(), this.fullHeight / this.$wrap.height());
            if (rate < 1) {
                rate = 1;
            }
            rate = 1 / rate;
            this.bestRate = rate;
            this.zoomToRate(rate);
            this.centerImage();
        },
        zoomToRate: function(rate) {
            if (this.$img == null) {
                return;
            }
            this.$img.width(this.fullWidth * rate + "px");
            this.$img.height(this.fullHeight * rate + "px");
            this.rate = rate;
            if (rate == this.bestRate) {
                this.$zoom.addClass("best");
            } else {
                this.$zoom.removeClass("best");
            }
        },
        centerImage: function() {
            if (this.$img == null) {
                return;
            }
            this.$img.css("top", (this.$wrap.height() - this.$img.height()) / 2 + "px");
            this.$img.css("left", (this.$wrap.width() - this.$img.width()) / 2 + "px");
        },
        enableDrag: function() {
            var me = this;
            var isMoving = false;
            var lastX = null;
            var lastY = null;
            me.$img.removeClass("moving");
            me.$img.mousedown(function (e) {
                isMoving = true;
                lastX = e.clientX;
                lastY = e.clientY;
                me.$img.addClass("moving");
            });
            $(document).mouseup(function () {
                isMoving = false;
                me.$img.removeClass("moving");
            });
            $(document).mousemove(function (e) {
                if (!isMoving) {
                    return;
                }
                e.preventDefault();
                var position = me.$img.position();
                var newLeft = position.left + (e.clientX - lastX);
                var newTop = position.top + (e.clientY - lastY);

                var containerWidth = me.$wrap.width();
                var containerHeight = me.$wrap.height();
                var width = me.$img.width();
                var height = me.$img.height();

                var minLeft = 0;
                var maxLeft = 0;
                var minTop = 0;
                var maxTop = 0;

                if (containerWidth >= width) {
                    minLeft = 0;
                    maxLeft = containerWidth - width;
                } else {
                    minLeft = containerWidth - width;
                    maxLeft = 0;
                }
                newLeft = Math.max(newLeft, minLeft);
                newLeft = Math.min(newLeft, maxLeft);
                
                if (containerHeight >= height) {
                    minTop = 0;
                    maxTop = containerHeight - height;
                } else {
                    minTop = containerHeight - height;
                    maxTop = 0;
                }
                newTop = Math.max(newTop, minTop);
                newTop = Math.min(newTop, maxTop);

                me.$img.css("left", newLeft + "px");
                me.$img.css("top", newTop + "px");
                lastX = e.clientX;
                lastY = e.clientY;
            });
        },
        enableWheelZoom: function() {
            var me = this;
            me.$wrap.bind("mousewheel", function (e) {
                e.preventDefault();
                if (me.$img == null) {
                    return;
                }
                var step = (-e.originalEvent.deltaY || e.originalEvent.wheelDelta) / 20;
                var newRate = me.rate + me.rate * step / 100;
                if (newRate < me.bestRate) {
                    newRate = me.bestRate;
                    if (me.rate == me.bestRate) {
                        return;
                    }
                }
                me.zoomToRate(newRate);
                me.centerImage();
            });
        },
        dispose: function() {
            $(document).unbind("mousemove").unbind("mouseup");
        }
    };

})();