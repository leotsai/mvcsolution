(function() {
    if (window.WeixinMenuManager) {
        return;
    }
    window.WeixinMenuManager = function() {
        this.data = null;
        this.maxLevel1Buttons = 3;
        this.maxLevel2Buttons = 5;
        this.urls = {
            readFromWeixin: "/admin/weixin/readMenuFromWeixin",
            saveToWeixin: "/admin/weixin/saveMenuToWeixin",
            readFromLocalDb: "/admin/weixin/readMenuFromLocalDb",
            saveToLocalDb: "/admin/weixin/saveMenuToLocalDb",
            deleteFromWeixin: "/admin/weixin/deleteMenuFromWeixin"
        };
    };

    WeixinMenuManager.prototype = {
        init: function() {
            var me = this;
            $("#btnReadFromWeixin").click(function() {
                if ($(this).hasClass("disabled")) {
                    return;
                }
                me.read(this, me.urls.readFromWeixin);
            });
            $("#btnSaveToWeixin").click(function () {
                if ($(this).hasClass("disabled")) {
                    return;
                }
                me.save(this, me.urls.saveToWeixin);
            });
            $("#btnDeleteFromWeixin").click(function () {
                if ($(this).hasClass("disabled")) {
                    return;
                }
                me.deleteFromWeixin(this, me.urls.deleteFromWeixin);
            });
            $("#btnReadFromLocalDb").click(function () {
                if ($(this).hasClass("disabled")) {
                    return;
                }
                me.read(this, me.urls.readFromLocalDb);
            });
            $("#btnSaveToLocalDb").click(function () {
                if ($(this).hasClass("disabled")) {
                    return;
                }
                me.save(this, me.urls.saveToLocalDb);
            });
            $("a.btn-add").live("click", function() {
                var btn = this;
                me.showButtonEditor(btn, null, function (button) {
                    var index = me.findButtonDataIndex(btn);
                    if (index.index2 != null) {
                        me.data.menu.button[index.index1].sub_button.push(button);
                    } else {
                        var oldButton = me.data.menu.button[index.index1];
                        if (oldButton == null) {
                            oldButton = { sub_button: [] };
                        }
                        button.sub_button = oldButton.sub_button;
                        me.data.menu.button[index.index1] = button;
                    }
                    me.render();
                });
            });
            $("div.button").live("click", function () {
                var btn = this;
                var index = me.findButtonDataIndex(btn);
                var buttonData = null;
                if (index.index2 != null) {
                    buttonData = me.data.menu.button[index.index1].sub_button[index.index2];
                } else {
                    buttonData = me.data.menu.button[index.index1];
                }

                me.showButtonEditor(btn, buttonData, function (button) {
                    if (button == null) {
                        if (index.index2 != null) {
                            me.data.menu.button[index.index1].sub_button.splice(index.index2, 1);
                        } else {
                            me.data.menu.button[index.index1] = null;
                        }
                    } else {
                        if (buttonData.sub_button) {
                            button.sub_button = buttonData.sub_button;
                        }
                        if (index.index2 != null) {
                            me.data.menu.button[index.index1].sub_button[index.index2] = button;
                        } else {
                            me.data.menu.button[index.index1] = button;
                        }
                    }
                    
                    me.render();
                });
            });
        },
        read: function(sender, url) {
            var me = this;
            var $btn = $(sender);
            var btnText = $btn.html();
            $btn.cssDisable().html("读取中...");
            MvcSolution.ajax.post(url, null, function(result) {
                $btn.cssEnable().html(btnText);
                if (result.Success) {
                    me.data = $.parseJSON(result.Value);
                    if (me.data == null) {
                        alert("没有读取到任何数据");
                    } else {
                        if (me.data.errmsg != null && me.data.errmsg !== "") {
                            alert(me.data.errmsg);
                        }
                    }
                    me.render();
                } else {
                    alert(result.Message);
                }
            });
        },
        save: function (sender, url) {
            var $btn = $(sender);
            var btnText = $btn.html();
            $btn.cssDisable().html("更新中...");
            var menu = this.data;
            if (url === this.urls.saveToWeixin) {
                menu = { button: [] };
                for (var i = 0; i < this.data.menu.button.length; i++) {
                    var button = this.data.menu.button[i];
                    if (button != null) {
                        menu.button.push(button);
                    }
                }
            }
            MvcSolution.ajax.post(url, "data=" + JSON.stringify(menu), function (result) {
                $btn.cssEnable().html(btnText);
                if (result.Success) {
                    alert(result.Value);
                } else {
                    alert(result.Message);
                }
            });
        },
        deleteFromWeixin: function() {
            if (!confirm("确定要删除全部菜单吗？")) {
                return;
            }
            $("#btnDeleteFromWeixin").cssDisable().html("删除中...");
            MvcSolution.ajax.post(this.urls.deleteFromWeixin, null, function (result) {
                $("#btnDeleteFromWeixin").cssEnable().html("从微信服务器删除菜单");
                if (result.Success) {
                    alert(result.Value);
                } else {
                    alert(result.Message);
                }
            });
        },
        render: function() {
            var $wrap = $("#data").empty();
            if (this.data == null) {
                this.data = {};
            }
            if (this.data.menu == null) {
                this.data.menu = {};
            }
            if (this.data.menu.button == null) {
                this.data.menu.button = [];
            }
            var html = '<ul class="buttons">';
            for (var i = 0; i < this.maxLevel1Buttons; i++) {
                html += '<li>';
                html += '<div class="top-button-wrap">';
                var level1Button = this.data.menu.button[i];
                if (level1Button == undefined) {
                    html += this.renderAddButton();
                    level1Button = {};
                } else {
                    html += this.renderButton(level1Button);
                }
                html += '</div>';

                if (level1Button.sub_button == null) {
                    level1Button.sub_button = [];
                }
                html += '<ul class="sub-buttons">';
                for (var j = 0; j < level1Button.sub_button.length; j++) {
                    var subButton = level1Button.sub_button[j];
                    html += '<li>' + this.renderButton(subButton) + '</li>';
                }
                if (level1Button.sub_button.length < this.maxLevel2Buttons) {
                    html += '<li>' + this.renderAddButton() + '</li>';
                }
                html += '</ul>';
                html += '</li>';
            }
            html += '</ul>';
            $wrap.html(html);
        },
        renderButton: function(button) {
            var trs = '';
            for (var property in button) {
                if (!button.hasOwnProperty(property)) {
                    continue;
                }
                var value = button[property];
                if (typeof value != "string") {
                    continue;
                }
                trs += '<tr><td>' + property + ': </td><td>' + value + '</td></tr>';
            }
            return '<div class="button">\
                        <table>\
                            <colgroup>\
                                <col width="80px"/>\
                                <col width="auto"/>\
                            </colgroup>\
                            <tbody>' + trs + '</tbody>\
                        </table>\
                    </div>';
        },
        renderAddButton: function() {
            return '<a href="javascript:void(0);" class="btn-add">+</a>';
        },
        showButtonEditor: function(sender, button, callback) {
            var me = this;
            var buttons = {
                "取消": function() {
                    $(this).dialog("close");
                },
                "保存": function() {
                    callback(me.parseButtonFromEditor());
                    $(this).dialog("close");
                }
            };
            if (button != null) {
                buttons["删除"] = function() {
                    $(this).dialog("close");
                    callback(null);
                };
            }
            if (button == null) {
                button = {};
            }
            MvcSolution.notification.dialog("dialogEditor", "button编辑器", {
                width: 500,
                height: 'auto',
                minHeight: 100,
                buttons: buttons,
                open: function() {
                    var html = '<table>\
                                    <colgroup>\
                                        <col width="100px"/>\
                                        <col width="auto"/>\
                                    </colgroup>\
                                    <tbody>\
                                        <tr>\
                                            <td>name: </td>\
                                            <td><input type="text" id="txtName" value="' + (button.name ? button.name : '') + '" class="txt"/></td>\
                                        </tr>\
                                        <tr>\
                                            <td>type: </td>\
                                            <td>\
                                                <select id="ddlTypes">\
                                                    <option value="click">click：点击推事件</option>\
                                                    <option value="view">view：跳转URL</option>\
                                                    <option value="scancode_push">scancode_push：扫码推事件</option>\
                                                    <option value="scancode_waitmsg">scancode_waitmsg：扫码推事件且弹出“消息接收中”提示框</option>\
                                                    <option value="pic_sysphoto">pic_sysphoto：弹出系统拍照发图</option>\
                                                    <option value="pic_photo_or_album">pic_photo_or_album：弹出拍照或者相册发图</option>\
                                                    <option value="pic_weixin">pic_weixin：弹出微信相册发图器</option>\
                                                    <option value="location_select">location_select：弹出地理位置选择器</option>\
                                                    <option value="media_id">media_id：下发消息（除文本消息）</option>\
                                                    <option value="view_limited">view_limited：跳转图文消息URL</option>\
                                                </select>\
                                            </td>\
                                        </tr>\
                                        <tr>\
                                            <td>key: </td>\
                                            <td><input type="text" id="txtKey" value="' + (button.key ? button.key : '') + '" class="txt"/></td>\
                                        </tr>\
                                        <tr>\
                                            <td>media_id: </td>\
                                            <td><input type="text" id="txtMediaId" value="' + (button.media_id ? button.media_id : '') + '" class="txt"/></td>\
                                        </tr>\
                                        <tr>\
                                            <td>url: </td>\
                                            <td><input type="text" id="txtUrl" value="' + (button.url ? button.url : '') + '" class="txt" /></td>\
                                        </tr>\
                                    </tbody>\
                                </table>';
                    $("#dialogEditor").html(html);
                    if (button.type) {
                        $("#ddlTypes").val(button.type);
                    }
                }
            });
        },
        parseButtonFromEditor: function() {
            var button = {
                name: $("#txtName").val(),
                type: $("#ddlTypes").val()
            };
            if ($("#txtKey").val() !== "") {
                button.key = $("#txtKey").val();
            }
            if ($("#txtMediaId").val() !== "") {
                button.media_id = $("#txtMediaId").val();
            }
            if ($("#txtUrl").val() !== "") {
                button.url = $("#txtUrl").val();
            }
            return button;
        },
        findButtonDataIndex: function(btn) {
            var index = {};
            if ($(btn).closest(".sub-buttons").length > 0) {
                index.index1 = $(btn).closest(".sub-buttons").closest("li").index();
                index.index2 = $(btn).closest("li").index();
            } else {
                index.index1 = $(btn).closest("li").index();
            }
            return index;
        }
    };

})();