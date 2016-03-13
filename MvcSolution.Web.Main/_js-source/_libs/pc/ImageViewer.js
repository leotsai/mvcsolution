(function() {
    if (window.ImageViewer) {
        return;
    }
    
    window.ImageViewer = function() {
        this.margins = {
            left: 20,
            right: 40,
            top: 20,
            bottom: 20
        };
        this.thumbnails = [];

        this.imageZoomer = null;
        this.slider = null;
    };

    ImageViewer.prototype = {
        show: function (thumbnails, index) {
            var me = this;
            me.thumbnails = thumbnails;

            me._renderFrame();
            this._resize();
            this.renderThumbnails(index);
            this.showImage();

            $("#imageViewer a.close").click(function() {
                me.close();
            });
            $("#imageViewer a.btn-left").click(function () {
                var $li = $("#imageViewer .thumbnails li.selected").prev();
                if ($li.length == 0) {
                    $li = $("#imageViewer .thumbnails li:last");
                }
                $("#imageViewer .thumbnails li").removeClass("selected");
                $li.addClass("selected");
                me.showImage();
            });
            $("#imageViewer a.btn-right").click(function () {
                var $li = $("#imageViewer .thumbnails li.selected").next();
                if ($li.length == 0) {
                    $li = $("#imageViewer .thumbnails li:first");
                }
                $("#imageViewer .thumbnails li").removeClass("selected");
                $li.addClass("selected");
                me.showImage();
            });
            $(document).keyup(function (e) {
                if (e.keyCode == 27) {
                    me.close();
                }
            });
        },
        close: function() {
            $("#imageViewer").remove();
            if (this.imageZoomer != null) {
                this.imageZoomer.dispose();
            }
            $(document).unbind("keyup");
        },
        showImage: function() {
            var me = this;
            var index = $("#imageViewer .thumbnails li.selected").index();

            if (this.imageZoomer != null) {
                this.imageZoomer.dispose();
            }
            this.imageZoomer = new ImageZoomer("#imageViewer .image", "#imageViewer .zoom");
            this.imageZoomer.init();
            this.imageZoomer.loadImage(this.getFullUrl(me.thumbnails[index]));
            this.slider.slideToHiddenSelectedIfNecessary();
        },
        getFullUrl: function(src) {
            return src.toLowerCase().replace(/s50x50/g, 'full');
        },
        renderThumbnails: function(selectedIndex) {
            var me = this;
            var $ul = $("#imageViewer .thumbnails > ul").empty();
            for (var i = 0; i < this.thumbnails.length; i++) {
                var src = this.thumbnails[i];
                var $li = $('<li><a href="javascript:void(0);"><img src="' + src + '"/></a></li>');
                $ul.append($li);
                if (selectedIndex == i) {
                    $li.addClass("selected");
                }
            }
            if ($ul.find("li.selected").length == 0) {
                $ul.children().first().addClass("selected");
            }

            me.slider = new Slider($ul, "#imageViewer .nav-left", "#imageViewer .nav-right");
            me.slider.itemWidth = 100;
            me.slider.init();

            $("#imageViewer .thumbnails li").click(function () {
                $(this).siblings().removeClass("selected");
                $(this).addClass("selected");
                me.showImage();
            });
        },
        _renderFrame: function() {
            var html = '\
<div id="imageViewer">\
    <div class="main-content">\
        <a class="close"></a>\
        <div class="viewer-main">\
            <div class="img-wrapper">\
                <a class="btn-left"></a>\
                <a class="btn-right"></a>\
                <div class="image"></div>\
                <div class="info"></div>\
                <a class="zoom" href="javascript:void(0);"></a>\
            </div>\
            <div class="bottom-nav">\
                <a class="nav-left"></a>\
                <a class="nav-right"></a>\
                <div class="thumbnails">\
                    <ul></ul>    \
                </div>\
            </div>\
        </div>    \
    </div>\
</div>';
            $("body").append(html);
        },
        _resize: function() {
            var $content = $("#imageViewer .main-content");
            var height = $(window).height() - this.margins.top - this.margins.bottom;
            $content.css("left", "20px").css("top", "20px")
                .width($(window).width() - this.margins.left - this.margins.right + "px")
                .height(height + "px");
            var $bottonNav = $("#imageViewer .bottom-nav");
            var $imgWrapper = $("#imageViewer .img-wrapper");
            $bottonNav.height("80px");
            $imgWrapper.height(height - 80 + "px");
        }
    };
})();