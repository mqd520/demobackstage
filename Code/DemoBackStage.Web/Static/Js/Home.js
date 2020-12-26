(function () {
    var _showMask = demo.ui.showMask;
    var _hideMask = demo.ui.hideMask;
    var _title = demo.cfg.title;


    $(document).ready(function () {
        //$("#btnCheckLogin").click(onCheckLoginClick);
        mini.parse();

        var menu = new Menu("#mainMenu", {
            itemclick: function (item) {
                if (!item.children) {
                    activeTab(item);
                }
            }
        });

        $(".sidebar").mCustomScrollbar({ autoHideScrollbar: true });

        new MenuTip(menu);

        $.ajax("/Home/Nav", {
            dataType: "json",
            method: "POST",
            success: function (data) {
                var ls = demo.ui.convertToMenuData(data);
                menu.loadData(ls);
            },
            error: function () {

            }
        });

        //toggle
        $("#toggle, .sidebar-toggle").click(function () {
            $('body').toggleClass('compact');
            mini.layout();
        });

        //dropdown
        $(".dropdown-toggle").click(function (event) {
            $(this).parent().addClass("open");
            return false;
        });

        $(document).click(function (event) {
            $(".dropdown").removeClass("open");
        });

        mini.get("txtOldPwd").on("validation", onOldPwdValidate);
        mini.get("txtNewPwd").on("validation", onNewPwdValidate);
        mini.get("txtNewPwd1").on("validation", onNewPwd1Validate);
    });

    function isLogin() {
        $.ajax("/Home/IsLogin", {
            method: "POST"
        });
    }

    function onCheckLoginClick() {
        isLogin();
    }

    function activeTab(item) {
        var tabs = mini.get("mainTabs");
        var tab = tabs.getTab(item.id);
        if (!tab) {
            tab = { name: item.id, title: item.text, url: item.url, iconCls: item.iconCls, showCloseButton: true };
            tab = tabs.addTab(tab);
        }
        tabs.activeTab(tab);
    }

    function showResetPwdDialog(el) {
        var win = mini.get("win1");

        mini.get("txtOldPwd").setValue("");
        mini.get("txtNewPwd").setValue("");
        mini.get("txtNewPwd1").setValue("");

        win.show();
    }

    function onOkClick() {
        var form = new mini.Form("#win1");
        form.validate();
        if (form.isValid() == false) return;

        var oldPwd = mini.get("txtOldPwd").getValue();
        var newPwd = mini.get("txtNewPwd").getValue();

        oldPwd = hex_md5(oldPwd);
        newPwd = hex_md5(newPwd);

        mini.confirm("是否确定重设密码", _title, function (e) {
            if (e == "ok") {
                _showMask();

                $.ajax("/Home/ResetPwd", {
                    method: "POST",
                    dataType: "json",
                    data: { OldPwd: oldPwd, NewPwd: newPwd },
                    success: function (data, textStatus, jqXHR) {
                        _hideMask();

                        if (data.Success) {
                            mini.alert("重设用户密码成功", _title, function () {
                                var win = mini.get("win1");
                                win.hide();
                            });
                        } else {
                            mini.alert(data.Msg, _title);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        _hideMask();

                        mini.alert("重设用户密码失败, 请稍后再试或联系管理员", _title);
                    }
                });
            }
        });
    }

    function onCancelClick() {
        var win = mini.get("win1");
        win.hide();
    }

    function onOldPwdValidate(e) {
        if (e.isValid) {
            if (!demo.reg.isPwd(e.value)) {
                e.isValid = false;
            }
        }
    }

    function onNewPwdValidate(e) {
        if (e.isValid) {
            if (!demo.reg.isPwd(e.value)) {
                e.isValid = false;
            }
        }
    }

    function onNewPwd1Validate(e) {
        if (e.isValid) {
            var str = mini.get("txtNewPwd").getValue();
            if (!demo.reg.isPwd(e.value) || e.value != str) {
                e.isValid = false;
            }
        }
    }

    function showUserInfoDialog() {
        var win = mini.get("win2");
        win.show();

        _showMask();
        $.ajax("/Home/UserInfo", {
            method: "POST",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                _hideMask();

                $("#lbUserName").text(data.UserName);
                $("#lbNickName").text(data.NickName);
                $("#lbIp").text(data.RegIp);
                $("#lbTime").text(data.RegTime);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                _hideMask();

                mini.alert("获取用户个人信息失败, 请稍后再试或联系管理员", _title);
            }
        });
    }


    window.showResetPwdDialog = showResetPwdDialog;
    window.onOkClick = onOkClick;
    window.onCancelClick = onCancelClick;
    window.showUserInfoDialog = showUserInfoDialog;

})();