MvcSolution.notification.toastSuccess = function (message) {
    $.toast({
        message: message,
        css: "success"
    });
};

MvcSolution.notification.toastError = function (message) {
    if (message == undefined || message == "") {
        message = "Error";
    }
    $.toast({
        message: message,
        css: "error",
        displayDuration: 3000
    });
};

MvcSolution.notification.toastInfo = function (message) {
    $.toast({
        message: message,
        css: "info"
    });
};


MvcSolution.notification.messageBox = function (title, content, buttons, onClose, width, height) {
    var id = "messagebox" + new Date().getTime();
    width = width == undefined ? 400 : width;
    height = height == undefined ? 250 : height;
    var $dialog = $("<div id='" + id + "' class='message-box'>" + content + "</div>");
    $dialog.appendTo("body");
    $dialog.dialog({
        modal: true,
        title: title,
        width: width,
        minHeight: height,
        height: 'auto',
        buttons: buttons,
        close: function () {
            $(this).dialog("destroy").remove();
            if (onClose != undefined && onClose != null) {
                onClose();
            }
        }
    });
};

MvcSolution.notification.alert = function (title, content, onClose, width, height) {
    MvcSolution.notification.messageBox(title, content, {
        "OK": function () {
            $(this).dialog("close");
        }
    }, onClose, width, height);
};

MvcSolution.notification.alertError = function (message, title) {
    if (title == undefined) {
        title = "Error";
    }
    if (message == undefined || message == '') {
        message = "The server returned an unknown error";
    }
    message = "<p class='error'>" + message + "</p>";
    MvcSolution.notification.alert(title, message);
};

MvcSolution.notification.dialog = function (id, title, options, destoryOnClose) {
    destoryOnClose = destoryOnClose == undefined ? true : destoryOnClose;
    var $dialog = $("#" + id);
    if ($dialog.length == 0) {
        $dialog = $("<div id='" + id + "' class='dialog' title='" + title + "'></div>").appendTo("body");
    }
    if (options.autoOpen == undefined) {
        options.autoOpen = false;
    }
    if (options.modal == undefined) {
        options.modal = true;
    }
    if (destoryOnClose) {
        if (options.close == undefined) {
            options.close = function () {
                $dialog.dialog("destroy");
                $dialog.remove();
            };
        } else {
            (function(closeFunc) {
                options.close = function () {
                    closeFunc();
                    $dialog.dialog("destroy");
                    $dialog.remove();
                };
            })(options.close);
        }
    }
    var createFunc = options.create;
    options.create = function() {
        createFunc && createFunc();
        //(function(idd) {
        //    setTimeout(function() {
        //        $('#' + idd).css("height", "auto");
        //    });
        //})(id);
    };
    $dialog.dialog(options);
    $dialog.dialog("open");
};