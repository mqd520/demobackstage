(function () {

    function showMask(msg) {
        mini.mask({
            el: document.body,
            cls: 'mini-mask-loading',
            html: msg + '...'
        });
    }

    function hideMask() {
        mini.unmask();
    }

    function convertToMenuData(ls) {
        var ls3 = convertToTreeData(ls, 1, 0);
        // console.log(ls3);
        var ls4 = convertToMiniUINavTree(ls3);
        // console.log(ls4);

        return ls4;
    }

    function convertToTreeData(ls, level, parentId) {
        var arr = [];

        // 找出指定等级的菜单
        for (var i = 0; i < ls.length; i++) {
            var item = ls[i];
            if (item.Level == level && item.ParentId == parentId) {
                arr.push(item);
            }
        }

        // 排序
        arr = arr.sort(function (a, b) {
            return a.Rank - b.Rank;
        });

        // 找出下级子节点
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            if (item.IsDir) {
                var arr1 = convertToTreeData(ls, level + 1, item.Id);
                item.children = arr1;
            }
        }

        return arr;
    }

    function convertToMiniUINavTree(arr) {
        var arr1 = [];

        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];

            if (item.children) {
                arr1.push({
                    id: item.Id.toString(),
                    text: item.Name,
                    children: convertToMiniUINavTree(item.children)
                });
            }
            else {
                arr1.push({
                    id: item.Id.toString(),
                    text: item.Name,
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

})();