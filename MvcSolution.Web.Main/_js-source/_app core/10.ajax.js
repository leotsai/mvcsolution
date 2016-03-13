(function() {
    MvcSolution.ajax = {
        post: function(url, data, successCallBack) {
            data = "ajax=true&ts=" + new Date().getTime() + "&" + data;
            $.ajax({
                type: "post",
                url: url,
                contentType: "application/x-www-form-urlencoded",
                data: data,
                success: function(result) {
                    successCallBack(result);
                },
                error: function(err) {
                    if (err.status != 0) {
                        successCallBack({
                            Success: false,
                            Message: '网络出错了'
                        });
                    }
                }
            });
        },
        postForm: function($form, callback) {
            this.post($form.attr("action"), $form.serialize(), callback);
        }
    };
})();

