(function() {
    mvcsolution.utils.bindDatePicker = function (input) {
        $(input).datepicker({
            dateFormat: "yy-mm-dd",
            monthNames: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"]
        });
    };

    mvcsolution.utils.onEnterKeydown = function(inputSelector, callback) {
        $(inputSelector).keydown(function(e) {
            if (e.keyCode == 13) {
                callback();
            }
        });
    };

    mvcsolution.utils.getCookieValue = function(name) {
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    };

    mvcsolution.utils.removeCookie = function (name) {
        var value = this.getCookieValue(name);
        if (value != null) {
            var exp = new Date();
            exp.setFullYear(2000);
            document.cookie = name + "=" + value + ";path=/;expires=" + exp.toGMTString();
        }
    };

    mvcsolution.utils.centerDialog = function(selector) {
        var $dialogs = selector ? $(selector).closest(".ui-dialog") : $(".ui-dialog:visible");
        $dialogs.each(function() {
            var $dialog = $(this);
            var height = $dialog.height();
            var winHeight = $(window).height();
            var top = (winHeight - height) / 2;
            top = Math.max(top, 0);
            $dialog.animate({ top: top }, 200);
        });
    };

    mvcsolution.utils.parseUnobtrusive = function(formSelector) {
        $.validator.unobtrusive.parse($(formSelector));
    };

    mvcsolution.utils.validator.checkEmail = function(val) {
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        return reg.test(val);
    };

    mvcsolution.utils.validator.checkPassword = function(val) {
        var reg = /^[\@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{1,}$/;
        return reg.test(val);
    };

    mvcsolution.utils.validator.checkDomain = function (domain) {
        var reg = /^(http\:\/\/|https\:\/\/)?[0-9a-zA-Z]+[0-9a-zA-Z\.-]*\.[a-zA-Z]{2,4}\/?$/;
        return reg.test(domain);
    };

    mvcsolution.utils.validator.checkUrl = function (url) {
        var reg = /(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?/;
        return reg.test(url);
    };
})();