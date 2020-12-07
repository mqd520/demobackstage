(function () {

    var reg = {
        isUserName: function (str) {
            var reg1 = /^\w{1}[\w]{5,19}$/gi;
            return reg1.test(str);
        },

        isPwd: function (str) {
            var reg1 = /^\w{1}[\w]{5,19}$/gi;
            return reg1.test(str);
        }
    };


    if (window.demo === undefined) {
        window.demo = {};
    }
    if (demo.reg === undefined) {
        demo.reg = reg;
    }

})();