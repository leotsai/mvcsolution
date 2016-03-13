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