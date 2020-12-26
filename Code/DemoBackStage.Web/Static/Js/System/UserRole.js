(function () {
    var _showMask = demo.ui.showMask;
    var _hideMask = demo.ui.hideMask;
    var _title = demo.cfg.title;
    var _isHasPermission = demo.ui.isHasPermission;
    var _EPermissionType = demo.consts.enum.EPermissionType;
    var _getPermissionTypeInfo = demo.consts.getPermissionTypeInfo;
    var _convertToTreeData = demo.ui.convertToTreeData;
    var _getPermissionTypeArr = demo.consts.getPermissionTypeArr;

    var _userId = 0;
    var _roleIdArr = [];


    // 页面入口
    $(document).ready(function () {
        mini.parse();
        initPermission();
        loadDataGrid();
    });

    function initPermission() {
        if (!_isHasPermission(_EPermissionType.update)) {
            $("#btnUserRoleReset").remove();
        }
    }

    function loadDataGrid() {
        var username = mini.get("txtUserName").getValue();

        var dg = mini.get("datagrid1");
        dg.load({
            UserName: username
        });
    }

    function onSearchClick() {
        loadDataGrid();
    }

    function onDataGridLoadError(e) {
        //console.log("onDataGridLoadError.e: %o", e);
        mini.alert("系统异常, 请稍后再试或联系管理员", _title);
    }

    function onOperateRender(e) {
        var row = e.row;

        var str = '<a href="javascript: onViewPermissionClick(' + row.Id + ');">查看权限</a>';

        return str;
    }

    function onViewPermissionClick(Id) {
        //console.log("onViewPermissionClick.Id: %d", Id);

        _userId = Id;

        var win = mini.get("win1");
        win.show();

        loadDataGrid2();
    }

    function loadDataGrid2() {
        _roleIdArr = [];

        var dg = mini.get("datagrid2");
        dg.load({
            UserId: _userId
        }, function (e) {
            if (e && e.result && e.result.data) {
                var data = e.result.data;
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];

                    _roleIdArr.push(item.Id);
                }
            }
        });
    }

    function onUserRoleRefreshClick() {
        loadDataGrid2();
    }

    function onUserRoleResetClick() {
        var win = mini.get("win2");
        win.show();

        loadDataGrid3();
    }

    function loadDataGrid3() {
        var dg = mini.get("datagrid3");
        dg.load({}, function (e) {
            //console.log("loadDataGrid3.Success: %o", e);
            var rows = dg.findRows(function (row1) {
                var b = false;

                for (var i = 0; i < _roleIdArr.length; i++) {
                    var item = _roleIdArr[i];

                    if (item == row1.Id) {
                        b = true;

                        break;
                    }
                }

                return b;
            });
            //console.log("loadDataGrid3.rows: %o", rows);
            dg.selects(rows);
        });
    }

    function onUserRoleResetOkClick() {
        var dg = mini.get("datagrid3");

        var ids = [];
        var rows = dg.getSelecteds();
        //console.log("onUserRoleResetOkClick.rows: %o", rows);
        for (var i = 0; i < rows.length; i++) {
            var item = rows[i];

            ids.push(item.Id);
        }

        mini.confirm("是否确定重新设置用户权限数据", _title, function (e) {
            if (e == "ok") {
                _showMask();

                $.ajax("/System/UserRole/Update", {
                    dataType: "json",
                    method: "POST",
                    data: {
                        UserId: _userId,
                        RoleIds: ids
                    },
                    success: function (data, textStatus, jqXHR) {
                        _hideMask();

                        if (data.Success) {
                            mini.alert("重设用户权限数据成功", _title, function () {
                                var win = mini.get("win2");
                                win.hide();

                                loadDataGrid2();
                            });
                        } else {
                            mini.alert(data.Msg, _title);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        _hideMask();

                        mini.alert("重设用户权限数据失败, 请稍后再试或联系管理员", _title);
                    }
                });
            }
        });
    }


    window.onSearchClick = onSearchClick;
    window.onDataGridLoadError = onDataGridLoadError;
    window.onOperateRender = onOperateRender;
    window.onViewPermissionClick = onViewPermissionClick;
    window.onUserRoleRefreshClick = onUserRoleRefreshClick;
    window.onUserRoleResetClick = onUserRoleResetClick;
    window.onUserRoleResetOkClick = onUserRoleResetOkClick;

})();