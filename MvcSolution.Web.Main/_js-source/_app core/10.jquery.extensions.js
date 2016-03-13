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

    jQuery.prototype.isCssDisabled = function () {
        return this.hasClass("disabled");
    };

})();