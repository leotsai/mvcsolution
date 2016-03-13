(function() {
    if (window.LikeReader) {
        return;
    }
    window.LikeReader = function() {

    };

    LikeReader.read = function(ids, url) {
        if (ids.length == 0) {
            return;
        }
        var data = '';
        for (var i = 0; i < ids.length; i++) {
            data += "&ids[" + i + "]=" + ids[i];
        }
        MvcSolution.ajax.post(url, data, function() {});
    };

    LikeReader.onLikeBtnClicked = function (sender, url, id) {
        var $btn = $(sender);
        var id = id == undefined ? $btn.closest("li").attr("data-id") : id;
        var wasLiked = $btn.hasClass("liked");
        var num = $btn.find("span").html() * 1;
        if (wasLiked) {
            $btn.removeClass("liked");
            $btn.find("span").html(num - 1);
        } else {
            $btn.addClass("liked");
            $btn.find("span").html(num + 1);
        }
        MvcSolution.ajax.post(url, "id=" + id + "&isToLike=" + (!wasLiked), function (result) {
            if (result.Success == false) {
                MvcSolution.notification.toast(result.Message);
            }
        });
    };

})();