(function () {

    var menuLevelInfo = [
        { value: 0, text: "Root" },
        { value: 1, text: "一级菜单" },
        { value: 2, text: "二级菜单" },
        { value: 3, text: "三级菜单" },
        { value: 4, text: "四级菜单" },
        { value: 5, text: "五级菜单" },
        { value: 6, text: "六级菜单" },
        { value: 7, text: "七级菜单" },
        { value: 8, text: "八级菜单" },
        { value: 9, text: "九级菜单" },
        { value: 10, text: "十级菜单" }
    ];

    if (window.demo === undefined) {
        window.demo = {};
    }
    if (demo.consts === undefined) {
        demo.consts = {};
    }

    demo.consts = {
        getAllMenuLevelInfo: function () {
            return menuLevelInfo;
        },

        getMenuLevelText: function (level) {
            var text = "";

            for (var i = 0; i < menuLevelInfo.length; i++) {
                var item = menuLevelInfo[i];
                if (item.value.toString() == level.toString()) {
                    text = item.text;

                    break;
                }
            }

            return text;
        },

        getMenuLevelValue: function (text) {
            var level = 0;

            for (var i = 0; i < menuLevelInfo.length; i++) {
                var item = menuLevelInfo[i];
                if (item.text == text) {
                    level = item.value;

                    break;
                }
            }

            return text;
        }
    };

})();