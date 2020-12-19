(function () {

    var reg = {
        isUserName: function (str) {
            var reg1 = /^[a-zA-Z]{1}\w{5,19}$/gi;
            return reg1.test(str);
        },

        isPwd: function (str) {
            var reg1 = /^[a-zA-Z]{1}\w{5,19}$/gi;
            return reg1.test(str);
        },

        isCode: function (str) {
            var reg1 = /^\w{4}$/gi;
            return reg1.test(str);
        },

        isName: function (str) {
            var reg = /^[\u4e00-\u9fa5a-zA-Z0-9]{1,20}$/gi;
            return reg.test(str);
        }
    };


    if (window.demo === undefined) {
        window.demo = {};
    }
    if (demo.reg === undefined) {
        demo.reg = reg;
    }

})();