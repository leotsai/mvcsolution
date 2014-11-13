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