(function () {
    var _showMask = demo.ui.showMask;
    var _hideMask = demo.ui.hideMask;
    var _title = demo.cfg.title;
    var _isHasPermission = demo.ui.isHasPermission;
    var _EPermissionType = demo.consts.enum.EPermissionType;
    var _getPermissionTypeInfo = demo.consts.getPermissionTypeInfo;
    var _convertToTreeData = demo.ui.convertToTreeData;
    var _getPermissionTypeArr = demo.consts.getPermissionTypeArr;

    var _roleId = 0;
    var _roleMenuList = [];


    // 页面入口
    $(document).ready(function () {
        mini.parse();
        initPermission();
        loadDataGrid();
    });

    function initPermission() {
        if (!_isHasPermission(_EPermissionType.update)) {
            $("#btnEdit").remove();
            $("#btnEdit1").remove();
        }
    }

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

        loadTreeGrid1();
    }

    function loadTreeGrid1() {
        _showMask();

        _roleMenuList = [];

        $.ajax("/System/Role/RoleMenuList", {
            method: "POST",
            dataType: "json",
            data: { RoleId: _roleId },
            success: function (data, textStatus, jqXHR) {
                _hideMask();

                if (data.Success) {
                    _roleMenuList = data.Data.dCopy();
                    var data1 = _convertToTreeData(data.Data, 1, 0, "MenuId", "MenuLevel", "MenuParentId", "MenuRank", "MenuIsDir", "MenuName");
                    //console.log(data1);
                    mini.get("treegrid1").loadData(data1, "MenuId", "MenuParentId");
                }
                else {
                    mini.alert(data.Msg, _title);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                _hideMask();

                mini.alert("查询数据失败, 请稍后再试或联系管理员", _title);
            }
        });
    }

    function getPermissionText(permissions) {
        var str = '';

        var arr = [];
        if (permissions != undefined && permissions != null) {
            arr = permissions.split(",");
        }

        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];
            var p = parseInt(item, 10);
            str += " " + _getPermissionTypeInfo(p);
        }

        if (str != "") {
            str = str.substring(1);
        }

        return str;
    }

    function onPermissionsRender(e) {
        var row = e.row;
        var str = getPermissionText(row.Permissions);

        return str;
    }

    function onRefreshClick() {
        loadTreeGrid1();
    }

    function onResetClick() {
        var win = mini.get("win2");

        loadAllMenus();

        win.show();
    }

    function loadAllMenus() {
        _showMask();

        $.ajax("/System/Role/MenuList", {
            method: "POST",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                _hideMask();

                if (data.Success) {
                    var data1 = _convertToTreeData(data.Data, 1, 0);
                    //console.log(data1);
                    mini.get("treegrid2").loadData(data1, "Id", "ParentId");
                }
                else {
                    mini.alert(data.Msg, _title);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                _hideMask();

                mini.alert("查询数据失败, 请稍后再试或联系管理员", _title);
            }
        });
    }

    function onPermissions1Render(e) {
        var row = e.row;

        var arr = [];  // 预选权限数组
        if (row.IsDir && row.IsDir == true) {
            arr = [_EPermissionType.view];
        } else {
            arr = _getPermissionTypeArr();
        }

        var arr2 = [];  // 权限结构数据列表, [{permission: Number(权限), checked: boolean(是否选中)}]
        for (var i = 0; i < arr.length; i++) {
            var item = arr[i];

            var b = false;
            for (var j = 0; j < _roleMenuList.length; j++) {
                var item2 = _roleMenuList[j];

                if (item2.MenuId == row.Id) {
                    var arr3 = [];
                    if (item2.Permissions != undefined && item2.Permissions != null) {
                        arr3 = item2.Permissions.split(",");
                    }

                    for (var x = 0; x < arr3.length; x++) {
                        var item3 = arr3[x];

                        var p = parseInt(item3, 10);
                        if (p == item) {
                            b = true;

                            break;
                        }
                    }

                    break;
                }
            }

            arr2.push({ permission: item, checked: b });
        }

        var str = '';
        for (var i = 0; i < arr2.length; i++) {
            var item = arr2[i];

            var checked = '';
            if (item.checked) {
                checked = 'checked="checked"';
            }

            var name = row.Id.toString() + "_" + item.permission;
            var onChange = 'onchange="onPermissionChanged(this);"';
            str += ' <input type="checkbox" ' + checked + ' MenuId="' + row.Id + '" MenuPermission="' + item.permission + '" ' + onChange + ' />' + _getPermissionTypeInfo(item.permission);
        }

        return str;
    }

    function getChildrenById(id, ls) {
        var arr = [];

        for (var i = 0; i < ls.length; i++) {
            var item = ls[i];

            if (item.ParentId == id) {
                arr.push(item);
                if (item.IsDir == true) {
                    arr.push(getChildrenById(item.Id, ls));
                }
            }
        }

        return arr;
    }

    function getParentById(parentId, ls, arr) {
        for (var i = 0; i < ls.length; i++) {
            var item = ls[i];

            if (item.Id == parentId) {
                if (item.IsDir == true) {
                    arr.push(item);
                    getParentById(item.ParentId, ls, arr);

                    break;
                }
            }
        }
    }

    function onPermissionChanged(e) {
        var tree = mini.get("treegrid2");
        var ls = tree.getList();

        //console.log("onViewChanged.e: %o", e);
        var e1 = $(e);
        if (e1.prop("checked") == false) {
            if (e1.attr("MenuPermission") == _EPermissionType.view.toString()) {
                var id = parseInt(e1.attr("MenuId"), 10);
                var row = tree.findRow(function (row) {
                    if (row.Id == id) {
                        return true;
                    }

                    return false;
                });
                //console.log("onViewChanged.row: %o", row);
                if (row.IsDir == true) {
                    var arr = getChildrenById(row.Id, ls);
                    //console.log("onViewChanged.arr: %o", arr);
                    for (var j = 0; j < arr.length; j++) {
                        var item = arr[j];

                        var cbs = $("#treegrid2 input:checkbox[MenuId=" + item.Id + "]");
                        cbs.removeProp("checked");
                    }
                } else {
                    var cbs = $("#treegrid2 input:checkbox[MenuId=" + id + "][MenuPermission!=" + _EPermissionType.view + "]");
                    cbs.removeProp("checked");
                }
            }
        } else {
            var id = parseInt(e1.attr("MenuId"), 10);
            if (e1.attr("MenuPermission") != _EPermissionType.view.toString()) {
                var cb = $("#treegrid2 input:checkbox[MenuId=" + id + "][MenuPermission=" + _EPermissionType.view + "]");
                cb.prop("checked", "checked");
            }

            var row = tree.findRow(function (row) {
                if (row.Id == id) {
                    return true;
                }

                return false;
            });
            //console.log("onViewChanged.row: %o", row);
            var arr = [];
            getParentById(row.ParentId, ls, arr);
            for (var i = 0; i < arr.length; i++) {
                var item = arr[i];

                var cbs = $("#treegrid2 input:checkbox[MenuId=" + item.Id + "]");
                cbs.prop("checked", "checked");
            }
        }
    }

    function onResetOkClick() {
        var items = [];
        var tree = mini.get("treegrid2");
        var ls = tree.getList();
        //console.log(ls);
        for (var i = 0; i < ls.length; i++) {
            var item = ls[i];

            var cbArr = $("#treegrid2 input:checkbox:checked[MenuId=" + item.Id + "]");
            //console.log("Id: %d, cbArr: %o", item.Id, cbArr.length);
            var permissions = "";
            cbArr.each(function (index, element) {
                permissions += "," + $(element).attr("MenuPermission");
            });
            if (permissions != "") {
                permissions = permissions.substring(1);
            }
            //console.log("Id: %d, permissions: %s", item.Id, permissions);

            items.push({
                MenuId: item.Id,
                Permissions: permissions
            });
        }
        //console.log("items: %o", items);


        mini.confirm("是否确定重新设置权限数据?", _title, function (e) {
            if (e == "ok") {
                _showMask();

                $.ajax("/System/Role/Update", {
                    dataType: "json",
                    method: "POST",
                    data: {
                        RoleId: _roleId,
                        Items: items
                    },
                    success: function (data, textStatus, jqXHR) {
                        _hideMask();

                        if (data.Success) {
                            mini.alert("重设权限数据成功", _title, function () {
                                var win = mini.get("win2");
                                win.hide();

                                loadTreeGrid1();
                            });
                        } else {
                            mini.alert(data.Msg, _title);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        _hideMask();

                        mini.alert("重设权限数据失败, 请稍后再试或联系管理员", _title);
                    }
                });
            }
        });
    }


    window.onSearchClick = onSearchClick;
    window.onDataGridLoadError = onDataGridLoadError;
    window.onOperateRender = onOperateRender;
    window.showMenuPermissionDialog = showMenuPermissionDialog;
    window.onPermissionsRender = onPermissionsRender;
    window.onRefreshClick = onRefreshClick;
    window.onResetClick = onResetClick;
    window.onPermissions1Render = onPermissions1Render;
    window.onResetOkClick = onResetOkClick;
    window.onPermissionChanged = onPermissionChanged;

})();