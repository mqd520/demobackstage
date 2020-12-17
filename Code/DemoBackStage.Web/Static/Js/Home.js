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
                var ls = demo.ui.convertToMenuData(data);
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

})();