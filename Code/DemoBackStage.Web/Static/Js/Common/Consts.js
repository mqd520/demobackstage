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

    var EPermissionType = {
        view: 1,
        update: 2,
        del: 3,
        add: 4
    };
    var _permissionTypeInfo = {
        view: "查看",
        update: "更新",
        del: "删除",
        add: "增加"
    };

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
        },

        getPermissionTypeInfo: function (permission) {
            var name = "";

            for (var key in EPermissionType) {
                if (EPermissionType[key] == permission) {
                    for (var key1 in _permissionTypeInfo) {
                        if (key == key1) {
                            name = _permissionTypeInfo[key1];

                            break;
                        }
                    }

                    break;
                }
            }

            return name;
        },

        getPermissionTypeArr: function () {
            var arr = [];

            for (var key in EPermissionType) {
                arr.push(EPermissionType[key]);
            }

            return arr;
        },

        enum: {
            EPermissionType: EPermissionType
        }
    };

})();