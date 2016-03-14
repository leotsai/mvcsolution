(function () {
    if (window.Grid) {
        return;
    }
    window.Grid = function (dataWrapper, pagerWarper, sortWrapper) {
        this.url = '';
        this.dataWrapper = dataWrapper;
        this.pagerWarper = pagerWarper;
        this.sortWrapper = sortWrapper == undefined ? ".grid > thead" : sortWrapper;
        this.pageIndex = 0;
        this.pageSize = 15;
        this.maxPageNumbers = 10;
        this.totalPages = 0;
        this.totalRows = 0;
        this.getUrlFunc = null;
        this.getDataFunc = null;
        this.showRefresh = false;
        this.showSummary = true;
        this.selectorTotalPages = ".total-pages";
        this.selectorPageIndex = ".page-index";
        this.selectorTotalRows = ".page-rows";
        this.isSeletable = true;

        this._request = null;
    };

    Grid.prototype = {
        init: function () {
            var me = this;
            var $pager = $(this.pagerWarper);
            if ($pager.hasClass("pager") == false) {
                $pager.addClass("pager");
            }
            if (this.showRefresh) {
                $pager.append('<a href="javascript:void(0);" class="refresh">刷新</a>');
                $pager.find(".refresh").click(function () {
                    me.refresh();
                });
            }
            if (this.showSummary) {
                $pager.append('<span class="pager-summary"></span>');
            }
            $pager.append("<ul></ul>");
            $(this.sortWrapper).find(".sortable").click(function () {
                var $dom = $(this);
                var newDirection = "asc";
                if ($dom.hasClass("sort-asc")) {
                    newDirection = "desc";
                }
                else if ($dom.hasClass("sort-desc")) {
                    newDirection = "asc";
                }
                $(me.sortWrapper).find(".sortable").removeClass("sort-asc").removeClass("sort-desc");
                $dom.addClass("sort-" + newDirection);
                me.pageTo(0);
            });
        },
        pageTo: function (pageIndex) {
            var url = '';
            var me = this;
            if (me._request != null) {
                me._request.abort();
                me._request = null;
                me.removeBusy();
            }
            url = me._getUrl(pageIndex, me);
            var data = me.getDataFunc == null ? null : me.getDataFunc();
            me.showBusy();
            me._request = MvcSolution.ajax.post(url, data, function (html) {
                me.removeBusy();
                me._triggerSuccess(html);
                $(me.dataWrapper).html(html);
                me._triggerComplete(me);
                me.totalPages = parseInt($(me.dataWrapper).find(me.selectorTotalPages).val());
                me.pageIndex = parseInt($(me.dataWrapper).find(me.selectorPageIndex).val());
                me.totalRows = parseInt($(me.dataWrapper).find(me.selectorTotalRows).val());
                me._renderPages();
                if (me.isSeletable) {
                    $(me.dataWrapper).find("tr").click(function () {
                        $(this).addClass("selected").siblings().removeClass("selected");
                    });
                }
            });
        },
        refresh: function () {
            this.pageTo(this.pageIndex);
        },
        showBusy: function () {
            var $pager = $(this.pagerWarper);
            var $busy = $pager.find(".busy");
            if ($busy.length == 0) {
                $busy = $('<span class="busy">加载中...</span>');
                $pager.prepend($busy);
            }
            $pager.find(".refresh").hide();
        },
        removeBusy: function () {
            var $pager = $(this.pagerWarper);
            $pager.find(".busy").remove();
            $pager.find(".refresh").show();
        },
        parse: function () {
            var me = this;
            me.totalPages = parseInt($(me.dataWrapper).find(me.selectorTotalPages).val());
            me.pageIndex = parseInt($(me.dataWrapper).find(me.selectorPageIndex).val());
            me._bindPageClicks();
        },
        _getUrl: function (pageIndex, me) {
            var url = '';
            if (me.getUrlFunc != null) {
                url = me.getUrlFunc(pageIndex);
            } else {
                url = me.url;
            }
            if (me.url.indexOf("?") < 0) {
                url += "?";
            }
            url += "&pageIndex=" + pageIndex + "&pageSize=" + me.pageSize;
            var sortDirection = "asc";
            var $sort = $(this.sortWrapper).find(".sort-asc");
            if ($sort.length == 0) {
                $sort = $(this.sortWrapper).find(".sort-desc");
                sortDirection = "desc";
            }
            if ($sort.length > 0) {
                url += "&sort=" + $sort.attr("data-sort")
                    + "&sortDirection=" + sortDirection;
            }
            return url;
        },
        _renderPages: function () {
            var me = this;
            var html = "";
            var startPageNumber = me.pageIndex + 1 - (Math.floor(me.maxPageNumbers / 2));

            if (me.pageIndex + 1 + Math.floor(me.maxPageNumbers / 2) >= me.totalPages) {
                startPageNumber = me.totalPages - me.maxPageNumbers + 1;
            }
            startPageNumber = startPageNumber < 1 ? 1 : startPageNumber;
            var endPageNumber = startPageNumber - 1 + (me.totalPages > me.maxPageNumbers ? me.maxPageNumbers : me.totalPages);
            endPageNumber = endPageNumber < 1 ? 1 : endPageNumber;
            html += '<li class="previous' + (me.pageIndex > 0 ? "" : " disabled") + '"><a href="javascript:void(0);">上一页</a></li>';
            for (var i = startPageNumber; i <= endPageNumber; i++) {
                var css = i == me.pageIndex + 1 ? "selected" : "";
                html += '<li class="page ' + css + '"><a href="javascript:void(0);">' + i + '</a></li>';
            }
            html += '<li class="next' + (me.pageIndex < me.totalPages - 1 ? "" : " disabled") + '"><a href="javascript:void(0);">下一页</a></li>';
            var $pager = $(me.pagerWarper);
            $pager.find("ul").html(html);

            if (me.showSummary) {
                var start = me.pageIndex * me.pageSize + 1;
                var end = Math.min((me.pageIndex + 1) * me.pageSize, me.totalRows);
                $pager.find(".pager-summary").html(start + " - " + end + "/" + me.totalRows);
            }
            me._bindPageClicks();
        },
        _bindPageClicks: function () {
            var me = this;
            var $pager = $(me.pagerWarper);
            $pager.find(".previous a").click(function (e) {
                e.preventDefault();
                if (me.pageIndex > 0) {
                    me.pageIndex--;
                    me.pageTo(me.pageIndex);
                }
            });

            $pager.find(".next a").click(function (e) {
                e.preventDefault();
                if (me.pageIndex < me.totalPages - 1) {
                    me.pageIndex++;
                    me.pageTo(me.pageIndex);
                }
            });

            $pager.find(".page a").click(function (e) {
                e.preventDefault();
                me.pageIndex = parseInt($(this).text()) - 1;
                me.pageTo(me.pageIndex);
            });
        },
        _triggerSuccess: function (data) {
            var evt = $.Event("Success");
            evt.value = data;
            $(this).trigger(evt);
        },
        _triggerComplete: function (data) {
            var evt = $.Event("Complete");
            evt.value = data;
            $(this).trigger(evt);
        },
        success: function (callback) {
            if (callback == null) {
                return;
            }
            $(this).bind("Success", callback);
        },
        beforeSend: function (callback) {
            if (callback == null) {
                return;
            }
            $(this).bind("BeforeSend", callback);
        },
        complete: function (callback) {
            if (callback == null) {
                return;
            }
            $(this).bind("Complete", callback);
        }
    };

})();
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
(function () {
    var urls = {
        list: "/admin/user/list",
        tags: "/admin/tag/entityList",
        saveUser: "/admin/user/save",
        userEditor: "/admin/user/editor",
        sendWeixin: "/admin/user/sendWeixin"
    };
    var criteria = "";
    var grid = null;

    $(document).ready(function() {
        $("#navUser").addClass("selected");
        grid = new Grid("tbody", ".pager");
        grid.pageSize = 20;
        grid.init();
        grid.url = urls.list;
        grid.getDataFunc = function () {
            return criteria;
        };
        grid.complete(function () {
            $("a.btn-edit").click(function() {
                showEditor($(this).closest("tr").attr("data-id"));
            });
            $(".cell-avartar img").click(function() {
                new ImageViewer().show([$(this).attr("src")], 0);
            });
        });
        $("#btnSearch").click(function () {
            search();
        });
        $("#btnTags").click(function () {
            page.showTagManager();
        });
        $("#btnMessage").click(function () {
            sendMessage();
        });
        search();
    });

    function search() {
        criteria = $("#formSearch").serialize();
        grid.pageTo(0);
    };

    page.refreshTagsAndGrid = function () {
        MvcSolution.ajax.post(urls.tags, null, function (result) {
            var id = $("#ddlTags").val();
            var html = '<option value="">Any/All</option>';
            for (var i = 0; i < result.Value.length; i++) {
                var item = result.Value[i];
                html += '<option value="' + item.Id + '">' + item.Name + '</option>';
            }
            $("#ddlTags").html(html).val(id);
            grid.refresh();
        });
    };

    function sendMessage() {
        var html = '<div class="group">\
                        Are you sure you want send message to the searched <b>' + grid.totalRows + '</b> users？\
                    </div>\
                    <div class="group">\
                        <label>Message Content：</label>\
                        <textarea id="txtWeixinContent" placeholder="less than 250 chars, html is allowed"></textarea>\
                    </div>\
                    <div class="group">\
                        Message Type：\
                        <input type="checkbox" id="cbWeixin" name="types" value="Weixin"/>\
                        <label for="cbWeixin">WeChat</label>\
                        <input type="checkbox" id="cbSystemMmessage" name="types" value="SystemMessage"/>\
                        <label for="cbSystemMmessage">System Message</label>\
                    </div>';
        MvcSolution.notification.dialog("dialogWeixin", "Send WeChat Message And System Message", {
            width: 500,
            height: 'auto',
            minHeight: 300,
            buttons: {
                "Cancel": function () {
                    $(this).dialog("close");
                },
                "Send": function () {
                    doSendMessage();
                }
            },
            open: function () {
                $("#dialogWeixin").html(html);
            }
        });
    };

    function doSendMessage() {
        var htmlContent = $("#txtWeixinContent").val();
        var text = $("<div>").html(htmlContent).text();
        var data = criteria + "&htmlContent=" + htmlContent + "&text=" + text;
        if ($("#cbWeixin").is(":checked")) {
            data += "&types=Weixin";
        }
        if ($("#cbSystemMmessage").is(":checked")) {
            data += "&types=SystemMessage";
        }

        MvcSolution.ajax.busyPost(urls.sendWeixin, data, function (result) {
            if (result.Success) {
                $("#dialogWeixin").dialog("close");
                MvcSolution.notification.alert("Success", "Message sent successfully！");
            } else {
                MvcSolution.notification.alertError(result.Message);
            }
        }, "sending...");
    };

    function showEditor(userId) {
        var dialogId = "dialogUserEditor";
        MvcSolution.notification.dialog(dialogId, "Edit User", {
            width: 600,
            height: 'auto',
            minHeight: 300,
            buttons: {
                "Cancel": function () {
                    $(this).dialog("close");
                },
                "Save": function () {
                    var dialog = this;
                    saveUser(function () {
                        $(dialog).dialog("close");
                        grid.refresh();
                    });
                }
            },
            open: function () {
                MvcSolution.ajax.load("#" + dialogId, urls.userEditor + "?userId=" + userId);
            }
        });

        function saveUser(callback) {
            var $form = $("#formEditor");
            $("#cellTags input").removeAttr("name");
            $("#cellTags input:checked").each(function (i) {
                $(this).attr("name", "tagIds[" + i + "]");
            });
            var data = $form.serialize();
            MvcSolution.ajax.busyPost(urls.saveUser, data, function (result) {
                if (result.Success) {
                    callback && callback();
                } else {
                    MvcSolution.notification.alertError(result.Message);
                }
            }, "Saving...");
        }
    };

})();
(function() {
    var dialogId = "dialogTagManager";
    var hasChanges = false;
    var urls = {
        saveTag: "/admin/tag/save",
        tagList: "/admin/tag/list",
        deleteTag: "/admin/tag/delete"
    };

    page.showTagManager = function () {
        hasChanges = false;
        MvcSolution.notification.dialog(dialogId, "Manage Tags", {
            width: 600,
            height: 400,
            open: function () {
                var html = '<div class="group">\
                                <input type="text" class="txt" id="txtNewTagName" placeholder="Tag Name"/>\
                                <a href="javascript:void(0);" id="btnAddNewTag" class="btn">Add a Tag</a>\
                            </div>\
                            <div>\
                                <table class="grid">\
                                    <colgroup>\
                                        <col width="auto"/>\
                                        <col width="100px"/>\
                                    </colgroup>\
                                    <thead>\
                                        <tr>\
                                            <th>Name</th>\
                                            <th class="center">Options</th>\
                                        </tr>\
                                    </thead>\
                                    <tbody></tbody>\
                                </table>\
                            </div>';
                $("#" + dialogId).html(html);
                loadTagList();
                bindAddTagButton();
            },
            close: function () {
                if (hasChanges) {
                    page.refreshTagsAndGrid();
                }
            }
        });
    };

    function bindRowClicks() {
        $("#" + dialogId + " a.btn-edit").click(function () {
            var $tr = $(this).closest("tr");
            $tr.find("input").val($tr.find("label").html()).show();
            $tr.find("label").hide();
            showSaveBtn($tr);
        });
        $("#" + dialogId + " a.btn-cancel").click(function () {
            var $tr = $(this).closest("tr");
            $tr.find("input").hide();
            $tr.find("label").show();
            hideSaveBtn($tr);
        });
        $("#" + dialogId + " a.btn-delete").click(function () {
            if (!confirm("Are you sure you want to delete this tag?")) {
                return;
            }
            var $tr = $(this).closest("tr");
            var id = $tr.attr("data-id");
            MvcSolution.ajax.busyPost(urls.deleteTag, "tagid=" + id, function (result) {
                if (result.Success) {
                    MvcSolution.notification.toastSuccess("The tag was deleted!");
                    $tr.remove();
                    hasChanges = true;
                } else {
                    MvcSolution.notification.toastError(result.Message);
                }
            });
        });
        $("#" + dialogId + " a.btn-save").click(function () {
            var $tr = $(this).closest("tr");
            var id = $tr.attr("data-id");
            var name = $tr.find("input").val();
            MvcSolution.ajax.busyPost(urls.saveTag, "id=" + id + "&name=" + name, function (result) {
                if (result.Success) {
                    loadTagList();
                    hasChanges = true;
                } else {
                    MvcSolution.notification.toastError(result.Message);
                }
            }, "Saving...");
        });
    };

    function bindAddTagButton() {
        $("#" + dialogId + " a#btnAddNewTag").click(function () {
            var name = $("#txtNewTagName").val();
            if (name == '') {
                MvcSolution.notification.alert("please enter a name");
                return;
            }
            MvcSolution.ajax.busyPost(urls.saveTag, "id=newid&name=" + name, function (result) {
                if (result.Success) {
                    $("#txtNewTagName").val('');
                    loadTagList();
                    hasChanges = true;
                } else {
                    MvcSolution.notification.toastError(result.Message);
                }
            }, "Saving...");
        });
    };

    function showSaveBtn($tr) {
        $tr.find("a.btn-edit,a.btn-delete").hide();
        $tr.find("a.btn-cancel,a.btn-save").show();
    };

    function hideSaveBtn($tr) {
        $tr.find("a.btn-edit,a.btn-delete").show();
        $tr.find("a.btn-cancel,a.btn-save").hide();
    };

    function loadTagList() {
        $("#" + dialogId + " tbody").html('<tr><td colspan="2" class="loading">Loading...</td></tr>');
        MvcSolution.ajax.post(urls.tagList, null, function (html) {
            $("#" + dialogId + " tbody").html(html);
            bindRowClicks();
        });
    };

})();
