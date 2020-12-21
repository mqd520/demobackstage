(function () {
    var _roleId = 0;


    // 页面入口
    $(document).ready(function () {
        mini.parse();
        loadDataGrid();
    });

    function loadDataGrid() {
        var name = mini.get("txtName").getValue();

        mini.get("datagrid1").load({
            Name: name
        });
    }

    function onDataGridLoadError() {
        mini.alert("加载数据出错, 请稍后再试或联系管理员!", demo.title);
    }

    function onSearchClick() {
        loadDataGrid();
    }

    function onOperateRender(e) {
        //console.log("e: %o", e);
        var row = e.row;
        var id = row.Id;

        var str = '';
        str += '<a href="javascript: showMenuPermissionDialog(' + id + ');">查看权限</a>';

        return str;
    }

    function showMenuPermissionDialog(id) {
        var dg = mini.get("datagrid1");
        var row = dg.findRow(function (e) {
            if (e.Id == id) {
                return true;
            }

            return false;
        });

        _roleId = id;

        var win = mini.get("win1");
        win.show();

        loadDataGrid2();
    }

    function loadDataGrid2() {
        mini.get("datagrid2").load({
            RoleId: _roleId
        });
    }




    window.onSearchClick = onSearchClick;
    window.onDataGridLoadError = onDataGridLoadError;
    window.onOperateRender = onOperateRender;
    window.showMenuPermissionDialog = showMenuPermissionDialog;

})();