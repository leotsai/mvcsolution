(function() {
    if (window.ListPager) {
        return;
    }
    window.ListPager = function (listSelector, url) {
        this.url = url;
        this.listSelector = listSelector;
        this.mode = "pageIndex";
        this.emptyText = '没有数据';
    };

    ListPager.prototype = {
        init: function() {
            this.load();
        },
        load: function(para) {
            var me = this;
            MvcSolution.ajax.post(me.url, me.getPostData(para), function (html) {
                var $list = $(me.listSelector);
                $list.find(".loading").remove();
                $list.append(html);
                if ($list.children().length == 0) {
                    me.renderEmpty();
                } else {
                    me._bindLoadMore();
                }
                me.onLoaded && me.onLoaded();
            });
        },
        renderEmpty: function() {
            $(this.listSelector).append("<li class='empty'>" + this.emptyText + "</li>");
        },
        _bindLoadMore: function() {
            var me = this;
            var $list = $(me.listSelector);
            $list.find(".btn-loadmore").click(function() {
                var $btn = $(this);
                if ($btn.hasClass("loading")) {
                    return;
                }
                $btn.addClass("loading").html("加载中...");
                var pageIndex = $btn.attr("data-pageIndex");
                if (pageIndex != undefined) {
                    me.load(pageIndex * 1 + 1);
                } else {
                    me.load($btn.attr("data-till"));
                }
            });
        },
        onLoaded: function() {

        },
        getPostData: function(para) {
            if (this.mode == "till") {
                return "till=" + para;
            }
            return "pageIndex=" + para;
        },
        reload: function() {
            $(this.listSelector).empty().append('<li class="loading">加载中...</li>');
            this.load();
        }
    };
})();