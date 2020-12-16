(function () {

    $(document).ready(function () {
        //$("#btnCheckLogin").click(onCheckLoginClick);

        var menu = new Menu("#mainMenu", {
            itemclick: function (item) {
                if (!item.children) {
                    activeTab(item);
                }
            }
        });

        $(".sidebar").mCustomScrollbar({ autoHideScrollbar: true });

        new MenuTip(menu);

        $.ajax("/Home/Nav", {
            dataType: "json",
            method: "POST",
            success: function (data) {
                var ls = convertToMenuData(data);
                menu.loadData(ls);
            },
            error: function () {

            }
        });

        //toggle
        $("#toggle, .sidebar-toggle").click(function () {
            $('body').toggleClass('compact');
            mini.layout();
        });

        //dropdown
        $(".dropdown-toggle").click(function (event) {
            $(this).parent().addClass("open");
            return false;
        });

        $(document).click(function (event) {
            $(".dropdown").removeClass("open");
        });
    });

    function isLogin() {
        $.ajax("/Home/IsLogin", {
            method: "POST"
        });
    }

    function onCheckLoginClick() {
        isLogin();
    }

    function activeTab(item) {
        var tabs = mini.get("mainTabs");
        var tab = tabs.getTab(item.id);
        if (!tab) {
            tab = { name: item.id, title: item.text, url: item.url, iconCls: item.iconCls, showCloseButton: true };
            tab = tabs.addTab(tab);
        }
        tabs.activeTab(tab);
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

})();