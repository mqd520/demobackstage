(function () {

    $(document).ready(function () {
        mini.parse();

        $("#btnLogin").click(onLoginClick);
        $("#btnReset").click(onResetClick);

        mini.get("txtUserName").onvalidation = onUserNameValidation;
        mini.get("txtPwd").onvalidation = onPwdValidation;

        var loginWindow = mini.get("loginWindow");
        loginWindow.show();

        refreshCode();
    });

    function onLoginClick() {
        var form = new mini.Form("#loginWindow");

        form.validate();
        if (form.isValid() == false) return;

        mini.mask({
            el: document.body,
            cls: 'mini-mask-loading',
            html: '登录中, 请稍后...'
        });

        setTimeout(function () {
            mini.unmask();
        }, 1500);
    }


    function onResetClick() {
        var form = new mini.Form("#loginWindow");
        form.clear();
    }

    function onUserNameValidation(e) {
        if (e.isValid) {
            if (!demo.reg.isUserName(e.value)) {
                e.errorText = "用户名格式不正确!";
                e.isValid = false;
            }
        }
    }

    function onPwdValidation(e) {
        if (e.isValid) {
            if (!demo.reg.isPwd(e.value)) {
                e.errorText = "密码格式不正确!";
                e.isValid = false;
            }
        }
    }

    function onCodeValidation(e) {
        if (e.isValid) {

        }
    }

    function refreshCode() {
        var url = "/User/Code";
        url += "?ran=" + Math.random().toString().substring(2);

        $("#imgCode").attr("src", url);
    }

    window.onPwdValidation = onPwdValidation;
    window.onUserNameValidation = onUserNameValidation;

})();