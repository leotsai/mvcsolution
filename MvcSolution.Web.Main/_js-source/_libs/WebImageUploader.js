(function() {
    if (window.WebImageUploader) {
        return;
    }
    window.WebImageUploader = function (wrapperSelector, imgKeys) {
        this.wrapperSelector = wrapperSelector;
        this.imgKeys = imgKeys == undefined ? [] : imgKeys;
        this.maxImages = 6;
        this.size = 's80x80';
        this.imageType = 0;
        this.cropCenter = false;
        this.showTip = true;

        this.url = '/main/img/upload';
        this.extensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
        this.accept = 'image/*';
        this.idPrefix = 'multiFileUpload';
        this.optimizeImage = false;

        this._uploadCount = 0;
    };

    WebImageUploader.prototype = {
        init: function() {
            var $wrapper = $(this.wrapperSelector);
            var $ul = $('<ul class="files-uploader"></ul>');
            $wrapper.append($ul);

            for (var i = 0; i < this.imgKeys.length; i++) {
                this.append$Li(this.imgKeys[i]);
            }
            this.appendNewUploaderIfNeeded();
        },
        append$Li: function (imgKey) {
            var me = this;
            var id = this.idPrefix + this.getAndIncreamentCount();
            var imgHtml = imgKey == null ? "+" : '<img src="' + this.getSrc(imgKey) + '"/>';
            var html = '<li data-key="' + imgKey + '" class="uploader-file' + (imgKey == null ? ' uploader-new' : '') + '">\
                            <input type="file" id="' + id + '" accept="' + me.accept + '"/>\
                            <label for="' + id + '">' + imgHtml + '</label>\
                            <a class="btn-delete"></a>\
                        </li>';
            var $li = $(html);
            $li.find("a.btn-delete").click(function () {
                $(this).closest("li").remove();
                me.onImageRemoved();
                me.appendNewUploaderIfNeeded();
            });
            $(this.wrapperSelector).find("ul").append($li);
            this._initUploader(id);
        },
        getSrc: function (imgKey) {
            if (imgKey == null || imgKey == '') {
                return '';
            }
            if (imgKey.indexOf('/') > -1) {
                return imgKey;
            }
            return "/img/" + this.size + "/" + imgKey;
        },
        appendNewUploaderIfNeeded: function () {
            var $wrap = $(this.wrapperSelector);
            if ($wrap.find("li.uploader-new").length == 0 && $wrap.find("li.uploader-file").length < this.maxImages) {
                this.append$Li(null);
            }
            if (this.showTip == false) {
                return;
            }
            $wrap.find(".upload-tip").remove();
            $wrap.find(".files-uploader > li").removeClass("has-tip");
            if ($wrap.find(".files-uploader > li").length == 1) {
                var $li = $wrap.find(".files-uploader > li").eq(0).addClass("has-tip");
                $li.append('<label class="upload-tip" for="' + $li.find(":file").attr("id") + '">点击添加照片</label>');
            }
        },
        onImageRemoved: function () {

        },
        getImageKeys: function () {
            if ($(this.wrapperSelector).find("li.uploading").length > 0) {
                MvcSolution.notification.toast("请等待图片上传完成");
                return null;
            }
            var imgs = [];
            $(this.wrapperSelector).find(".files-uploader > li").each(function () {
                if ($(this).hasClass(".uploader-new")) {
                    return;
                }
                var imgKey = $.trim($(this).attr("data-key"));
                if (imgKey == '' || imgKey == 'null' || imgKey == 'undefined') {
                    return;
                }
                imgs.push(imgKey);
            });
            return imgs;
        },
        getAndIncreamentCount: function () {
            this._uploadCount++;
            return this._uploadCount;
        },
        _getActualUrl: function() {
            if (typeof (this.url) == "function") {
                return this.url();
            }
            return this.url + "?type=" + this.imageType + "&cropCenter=" + this.cropCenter;
        },
        _initUploader: function(fileId) {
            var me = this;
            (function(fid) {
                var uploader = new FileUpload(fid, me._getActualUrl());
                uploader.extensions = me.extensions;
                var $file = $("#" + fid);
                var $li = $file.closest("li");

                uploader._showProgress = function () {
                    $li.find("label").html("上传中...");
                    $li.removeClass("uploader-new");
                    me.appendNewUploaderIfNeeded();
                };
                uploader._changeProgress = function (loaded, total) {
                    var text = ((loaded * 1) * 100 / (total * 1)).toFixed(1) + "%";
                    $li.find("label").html(text);
                };
                uploader.onUploaded = function (json) {
                    $file.val("");
                    if (json.Success) {
                        me._updateUploadedLi($li, json.Value);
                    } else {
                        alert(json.Message);
                    }
                };
                uploader.optimizeImage = me.optimizeImage;
                uploader.init();
            })(fileId);
        },
        _updateUploadedLi: function ($li, imgKey) {
            $li.find("label").html('<img src="' + this.getSrc(imgKey) + '" />');
            $li.attr("data-key", imgKey);
        }
    };

})();