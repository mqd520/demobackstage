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
        var startTime = mini.get("txtStartTime").getValue();
        var endTime = mini.get("txtEndTime").getValue();

        alert($("#txtStartTime").val());

        console.log("startTime: %s", startTime);
        console.log("endTime: %s", endTime);

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