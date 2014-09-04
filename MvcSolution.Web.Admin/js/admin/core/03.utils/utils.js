

admin.utils.bindDatePicker = function (input) {
    $(input).datepicker();
};

admin.utils.onEnterKeydown = function (inputSelector, callback) {
    $(inputSelector).keydown(function (e) {
        if (e.keyCode == 13) {
            callback();
        }
    });
};

admin.utils.getCookieValue = function (name) {
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

admin.utils.centerDialog = function (selector) {
    var $dialogs = selector ? $(selector).closest(".ui-dialog") : $(".ui-dialog:visible");
    $dialogs.each(function () {
        var $dialog = $(this);
        var height = $dialog.height();
        var winHeight = $(window).height();
        var top = (winHeight - height) / 2;
        top = Math.max(top, 0);
        $dialog.animate({ top: top }, 200);
    });
};

admin.utils.parseUnobtrusive = function (formSelector) {
    $.validator.unobtrusive.parse($(formSelector));
};