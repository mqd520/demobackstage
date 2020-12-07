(function () {

    $(document).ready(function () {
        mini.parse();

        $("#btnLogin").click(onLoginClick);
        $("#btnReset").click(onResetClick);

        mini.get("")

        var loginWindow = mini.get("loginWindow");
        loginWindow.show();

        refreshCode();
    });

    function onLoginClick() {
        var form = new mini.Form("#loginWindow");

        form.validate();
        if (form.isValid() == false) return;

        loginWindow.hide();
        mini.loading("登录成功，马上转到系统...", "登录成功");
        setTimeout(function () {
            window.location = "../outlooktree/outlooktree.html";
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