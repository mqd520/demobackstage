(function () {

    $(document).ready(function () {

    });

    function onSearch() {
        var username = $("#txtUserName").val();

        mini.get("datagrid1").load({
            username: username
        });
    }


    window.onSearch = onSearch;

})();