admin.notification.toastSuccess = function (message) {
    $.toast({
        message: message,
        css: "success"
    });
};

admin.notification.toastError = function (message) {
    if (message == undefined || message == "") {
        message = "Error occurred!";
    }
    $.toast({
        message: message,
        css: "error",
        displayDuration: 3000
    });
};

admin.notification.toastInfo = function (message) {
    $.toast({
        message: message,
        css: "info"
    });
};


admin.notification.messageBox = function (title, content, buttons, onClose, width, height) {
    var id = "messagebox" + new Date().getTime();
    width = width == undefined ? 400 : width;
    height = height == undefined ? 250 : height;
    var $dialog = $("<div id='" + id + "' class='message-box'>" + content + "</div>");
    $dialog.appendTo("body");
    $dialog.dialog({
        modal: true,
        title: title,
        width: width,
        height: height,
        buttons: buttons,
        close: function () {
            $(this).dialog("destroy");
            if (onClose != undefined && onClose != null) {
                onClose();
            }
        }
    });
};

admin.notification.alert = function (title, content, onClose, width, height) {
    admin.notification.messageBox(title, content, {
        "OK": function () {
            $(this).dialog("close");
        }
    }, onClose, width, height);
};

admin.notification.alertError = function (message, title) {
    if (title == undefined) {
        title = "Error Ocurred!";
    }
    if (message == undefined || message == '') {
        message = "We are sorry. An unknown error occurred.";
    }
    message = "<p class='error'>" + message + "</p>";
    admin.notification.alert(title, message);
};

admin.notification.dialog = function (id, title, options, destoryOnClose) {
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
            };
        } else {
            options.close = function () {
                options.close();
                $dialog.dialog("destroy");
            };
        }
    }
    var createFunc = options.create;
    options.create = function() {
        createFunc && createFunc();
        (function(idd) {
            setTimeout(function() {
                $('#' + idd).css("height", "auto");
            });
        })(id);
    };
    $dialog.dialog(options);
    $dialog.dialog("open");
};