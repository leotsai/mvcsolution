(function() {
    $(document).ready(function () {
        $(".btn-menu").click(function(e) {
            if ($(this).hasClass("open")) {
                $(this).removeClass("open");
                $(this).find("ul").hide();
            } else {
                $(this).addClass("open");
                $(this).find("ul").show();
            }
        });
        $("a.nohistory").on("click", function(e) {
            e.preventDefault();
            window.location.replace($(this).attr("href"));
        });

        $(".header > .btn-back").on("click", function (e) {
            e.preventDefault();
            history.go(-1);
        });
    });

    window.onerror = function(err) {
        //setTimeout(function() {
        //    alert("出错啦：" + err);
        //}, 3000);
    };

})();