(function () {

    $(document).ready(function () {
        mini.parse();

        $("#btnLogin").click(onLoginClick);
        $("#btnReset").click(onResetClick);
        $("#imgCode").click(onCodeClick);

        mini.get("txtUserName").on("validation", onUserNameValidation);
        mini.get("txtPwd").on("validation", onPwdValidation);
        mini.get("txtCode").on("validation", onCodeValidation);

        var loginWindow = mini.get("loginWindow");
        loginWindow.show("center", "200%");

        refreshCode();
    });

    function onLoginClick() {
        var form = new mini.Form("#loginWindow");
        form.validate();
        if (form.isValid() == false) return;

        var username = mini.get("txtUserName").getValue();
        var pwd = mini.get("txtPwd").getValue();
        var pwd1 = hex_md5(pwd);
        var code = mini.get("txtCode").getValue();


        mini.mask({
            el: document.body,
            cls: 'mini-mask-loading',
            html: '登录中, 请稍后...'
        });

        $.ajax("/User/Login", {
            method: "POST",
            dataType: "json",
            data: { UserName: username, Pwd: pwd1, Code: code },
            success: function (data, textStatus, jqXHR) {
                mini.unmask();

                if (data.Success) {
                    window.location.href = "/Home";
                } else {
                    mini.alert(data.Msg, "Demo BackStage Admin...");
                    refreshCode();
                    mini.get("txtCode").setValue("");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                mini.unmask();
                mini.alert("登录失败, 请稍后再试!", "Demo BackStage Admin...");
                refreshCode();
                mini.get("txtCode").setValue("");
            }
        });
    }


    function onResetClick() {
        var form = new mini.Form("#loginWindow");
        form.clear();
    }

    function onCodeClick() {
        refreshCode();
    }

    function onUserNameValidation(e) {
        if (e.isValid) {
            if (!demo.reg.isUserName(e.value)) {
                mini.alert("用户名格式不正确！");
                e.isValid = false;
            }
        }
    }

    function onPwdValidation(e) {
        if (e.isValid) {
            if (!demo.reg.isPwd(e.value)) {
                mini.alert("密码格式不正确！");
                e.isValid = false;
            }
        }
    }

    function onCodeValidation(e) {
        if (e.isValid) {
            if (!demo.reg.isCode(e.value)) {
                mini.alert("验证码格式不正确！");
                e.isValid = false;
            }
        }
    }

    function refreshCode() {
        var url = "/User/Code";
        url += "?ran=" + Math.random().toString().substring(2);

        $("#imgCode").attr("src", url);
    }

})();