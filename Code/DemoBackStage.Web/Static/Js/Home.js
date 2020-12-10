(function () {

    $(document).ready(function () {
        isLogin();
        //$("#btnCheckLogin").click(onCheckLoginClick);
    });

    function isLogin() {
        $.ajax("/Home/IsLogin", {
            method: "POST",
            success: function (data, textStatus, jqXHR) {
                console.log("Success");

                if (data.Success) {

                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log("error");
                if (XMLHttpRequest) {

                }
            },
            complete: function () {
                console.log("complete");
            }
        });
    }

    function onCheckLoginClick() {
        isLogin();
    }

})();