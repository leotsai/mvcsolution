(function() {
    MvcSolution.notification = {
        toast: function (message) {
            var options = {
                showDuration: 500,
                displayDuration: 2000,
                hideDuration: 500,
                css: ""
            };

            var $toast = $("<div class='toast'><div class='toast-inner'>" + message + "</div></div>");
            if (options.css != "") {
                $toast.addClass(options.css);
            }
            $toast.appendTo("#body");
            var centerX = $(window).width() / 2;
            var left = centerX - $toast.outerWidth() / 2;
            var bottom = $(window).height() / 10;
            $toast.css("bottom", 0 + "px");
            $toast.css("left", left + "px");
            $toast.animate({
                bottom: "+=" + bottom,
                opacity: 1
            }, {
                duration: options.showDuration,
                complete: function () {
                    setTimeout(function () {
                        $toast.animate({
                            bottom: "-=35",
                            opacity: 0
                        }, {
                            duration: options.hideDuration,
                            complete: function () {
                                $toast.remove();
                            }
                        });
                    }, options.displayDuration);
                }
            });
        },
        toastError: function (message) {
            this.toast(message);
        },
        modalTopRight: function (message) {
            var html = '<div id="modal">\
                            <div class="message">' + message + '</div>\
                            <div class="tip">轻按退出</div>\
                        </div>';
            $("#body").append(html);
            $("#modal").click(function () {
                $(this).remove();
            });
        },
        showPageSuccess: function (msg, btn) {
            var btnHtml = btn == undefined ? '<a class="btn" href="javascript:history.go(-1);">返回</a>' : btn;
            var html = '<div class="action-result">\
                    <div class="success"><span class="icon-check"></span> ' + msg + '</div>\
                    ' + btnHtml + '\
                </div>';
            $(".content").addClass("inner").html(html);
        }
    };
})();