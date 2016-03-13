(function() {
    if (window.WeixinRecharger) {
        return;
    }
    var _url = '/app/weixin/PreRecharge';
    var _successCallback = null;
    var _errorCallback = null;

    window.WeixinRecharger = function() {
        
    };

    WeixinRecharger.post = function (amount, successCallback, errorCallback) {
        _successCallback = successCallback;
        _errorCallback = errorCallback;
        MvcSolution.ajax.post(_url, "amount=" + amount, function (result) {
            if (result.Success) {
                showWeixinPay({
                    appId: result.Value.AppId,
                    "package": result.Value.Package,
                    paySign: result.Value.PaySign,
                    timeStamp: result.Value.TimeStamp,
                    nonceStr: result.Value.NonceStr,
                    signType: result.Value.SignType
                });
            } else {
                _errorCallback && _errorCallback(result.Message);
            }
        });

    };

    function showWeixinPay(data) {
        if (window.WeixinJSBridge == undefined) {
            _errorCallback && _errorCallback("找不到微信组件，请在微信中打开");
            return;
        }
        WeixinJSBridge.invoke(
            'getBrandWCPayRequest', {
                "appId": data.appId, //公众号名称，由商户传入     
                "timeStamp": data.timeStamp, //时间戳，自1970年以来的秒数     
                "nonceStr": data.nonceStr, //随机串     
                "package": data.package,
                "signType": data.signType, //微信签名方式:     
                "paySign": data.paySign //微信签名 
            },
            function(res) {
                if (res.err_msg == "get_brand_wcpay_request:ok") {
                    _successCallback && _successCallback();
                } else {
                    _errorCallback && _errorCallback(res.err_msg);
                }
            });
    };

})();