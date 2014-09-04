window.FrequencySelector = function(selector) {
    this.selector = selector;
};

FrequencySelector.prototype = {
    init: function() {
        var obj = this;
        $(obj.selector + " li").click(function() {
            var changed = $(this).hasClass("selected") == false;
            var $ul = $(this).closest("ul");
            $ul.children().removeClass("selected");
            $(this).addClass("selected");
            if (changed) {
                obj.onSelectionChanged();
            }
        });
    },
    getFrequency: function() {
        var text = $(this.selector + " li.selected").html();
        var options = {
            "小时": 3,
            "日": 0,
            "周": 1,
            "月": 2
        };
        return options[text];
    },
    onSelectionChanged: function() {

    }
};