(function () {

    $(document).ready(function () {
        window.setTimeout(function () {
            Query();
        }, 300);
    });

    function onSearch() {
        Query();
    }

    function onDataGridLoadError() {
        mini.alert("加载数据出错, 请稍后再试或联系管理员!", demo.title);
    }

    function Query() {
        var username = mini.get("txtUserName").getValue();

        mini.get("datagrid1").load({
            UserName: username
        });
    }


    window.onSearch = onSearch;
    window.onDataGridLoadError = onDataGridLoadError;

})();