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
