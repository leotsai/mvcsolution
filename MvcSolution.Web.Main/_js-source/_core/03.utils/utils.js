(function() {
    var urls = {
        myNotifications: '/me/me/notifications'
    };
    
    MvcSolution.utils.bindDatePicker = function (input) {
        $(input).datepicker({
            dateFormat: "yy-m-d",
            monthNames: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            monthNamesShort: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
            changeYear: true,
            changeMonth: true
        });
    };

    MvcSolution.utils.onEnterKeydown = function (inputSelector, callback) {
        $(inputSelector).keydown(function (e) {
            if (e.keyCode == 13) {
                callback();
            }
        });
    };

    MvcSolution.utils.getCookieValue = function (name) {
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

    MvcSolution.utils.removeCookie = function (name) {
        var value = this.getCookieValue(name);
        if (value != null) {
            var exp = new Date();
            exp.setFullYear(2000);
            document.cookie = name + "=" + value + ";path=/;expires=" + exp.toGMTString();
        }
    };

    MvcSolution.utils.setCookie = function (name, value, expire) {
        document.cookie = name + "=" + value + ";path=/;expires=" + expire.toGMTString();
    };

})();