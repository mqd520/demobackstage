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
        var ip = mini.get("txtIp").getValue();
        var startTime = mini.get("txtStartTime").getFormValue();
        var endTime = mini.get("txtEndTime").getFormValue();

        mini.get("datagrid1").load({
            UserName: username,
            Ip: ip,
            StartTime: startTime,
            EndTime: endTime
        });
    }


    window.onSearch = onSearch;
    window.onDataGridLoadError = onDataGridLoadError;

})();