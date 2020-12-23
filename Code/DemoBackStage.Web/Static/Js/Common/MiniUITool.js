(function () {

    function showMask(msg) {
        var msg1 = msg || "加载中...";

        mini.mask({
            el: document.body,
            cls: 'mini-mask-loading',
            html: msg1
        });
    }

    function hideMask() {
        mini.unmask();
    }

    function sortMenuList(arr) {
        var arr1 = [];

        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];

        }

        return arr1;
    }

    function convertToMenuData(ls, idField, levelField, parentField, rankField, isDirField, nameField) {
        var idField1 = idField || "Id";
        var levelField1 = levelField || "Level";
        var parentField1 = parentField || "ParentId";
        var rankField1 = rankField || "Rank";
        var isDirField1 = isDirField || "IsDir";
        var nameField1 = nameField || "Name";

        var ls3 = convertToTreeData(ls, 1, 0);
        // console.log(ls3);
        var ls4 = convertToMiniUINavTree(ls3, idField1, nameField1);
        // console.log(ls4);

        return ls4;
    }

    function convertToTreeData(ls, level, parentId, idField, levelField, parentField, rankField, isDirField) {
        var arr = [];

        var idField1 = idField || "Id";
        var levelField1 = levelField || "Level";
        var parentField1 = parentField || "ParentId";
        var rankField1 = rankField || "Rank";
        var isDirField1 = isDirField || "IsDir";

        // 找出指定等级的菜单
        for (var i = 0; i < ls.length; i++) {
            var item = ls[i];
            if (item[levelField1] == level && item[parentField1] == parentId) {
                arr.push(item);
            }
        }

        // 排序
        arr = arr.sort(function (a, b) {
            return a[rankField1] - b[rankField1];
        });

        // 找出下级子节点
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            if (item[isDirField1]) {
                var arr1 = convertToTreeData(ls, level + 1, item[idField1], idField1, levelField1, parentField1, rankField1);
                item.children = arr1;
            }
        }

        return arr;
    }

    function convertToMiniUINavTree(arr, idField, nameField) {
        var arr1 = [];

        var idField1 = idField || "Id";
        var nameField1 = nameField || "Name";

        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];

            if (item.children) {
                arr1.push({
                    id: item[idField1].toString(),
                    text: item[nameField1],
                    children: convertToMiniUINavTree(item.children)
                });
            }
            else {
                arr1.push({
                    id: item[idField1].toString(),
                    text: item[nameField1],
                    url: item.Url
                });
            }
        }

        return arr1;
    }

    function bindComboBox(id, defaultValue) {
        var data = [
            { value: 1, text: "是" },
            { value: 0, text: "否" }
        ];
        var obj = mini.get("selAddMenuIsDir");
        obj.load(data);
        if (defaultValue != undefined) {
            obj.setValue(defaultValue);
        } else {
            obj.setValue(1);
        }
    }

    function isHasPermission(permission) {
        var b = false;

        var arr = __$$hMyPermissions || []; // 当前页面所拥有的权限
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];

            if (item == permission) {
                b = true;

                break;
            }
        }

        return b;
    }


    if (window.demo === undefined) {
        window.demo = {};
    }
    if (demo.ui === undefined) {
        demo.ui = {};
    }
    demo.ui.showMask = showMask;
    demo.ui.hideMask = hideMask;
    demo.ui.convertToMenuData = convertToMenuData;
    demo.ui.convertToTreeData = convertToTreeData;
    demo.ui.convertToMiniUINavTree = convertToMiniUINavTree;
    demo.ui.bindComboBox = bindComboBox;
    demo.ui.isHasPermission = isHasPermission;

})();