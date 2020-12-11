(function () {

    $(document).ready(function () {
        //isLogin();
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

        var ls = ConvertToNavTree([]);

        menu.loadData(ls);

        //$.ajax({
        //    //url: "data/menu.txt",
        //    //success: function (text) {
        //    //    var data = mini.decode(text);
        //    //    menu.loadData(data);
        //    //}
        //});

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

    function ConvertToNavTree(ls) {
        ls = [
            { Id: "10", Name: "系统管理", Url: "", IsDir: true, Level: 1, Rank: 1, ParentId: 0 },
            { Id: "11", Name: "用户管理", Url: "/System/Menu", IsDir: false, Level: 2, Rank: 1, ParentId: 10 },
            { Id: "12", Name: "用户登录日志", Url: "/System/UserLoginLog", IsDir: false, Level: 2, Rank: 2, ParentId: 10 },
            { Id: "13", Name: "菜单管理", Url: "/System/Menu", IsDir: false, Level: 2, Rank: 3, ParentId: 10 },
            { Id: "14", Name: "角色管理", Url: "/System/Role", IsDir: false, Level: 2, Rank: 4, ParentId: 10 }
        ];

        var ls3 = fun1(ls, 1, 0);
        // console.log(ls3);
        var ls4 = convertToMiniUINavTree(ls3);
        // console.log(ls4);

        return ls4;
    }

    function fun1(ls, level, parentId) {
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

        // 找出下级节点
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            if (item.IsDir) {
                var arr1 = fun1(ls, level + 1, item.Id);
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
                    id: item.Id,
                    text: item.Name,
                    children: convertToMiniUINavTree(item.children)
                });
            }
            else {
                arr1.push({
                    id: item.Id,
                    text: item.Name,
                    url: item.Url
                });
            }
        }

        return arr1;
    }

})();