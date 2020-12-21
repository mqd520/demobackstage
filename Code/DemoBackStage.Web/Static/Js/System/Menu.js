(function () {
    var _isHasPermission = demo.ui.isHasPermission;
    var _EPermissionType = demo.consts.enum.EPermissionType;
    var _convertToTreeData = demo.ui.convertToTreeData;
    var _getMenuLevelText = demo.consts.getMenuLevelText;
    var _bindComboBox = demo.ui.bindComboBox;
    var _showMask = demo.ui.showMask;
    var _hideMask = demo.ui.hideMask;
    var _title = demo.cfg.title;

    var _menuTreeData = [];
    var _menuData = [];
    var _isAdd = false;
    var _id = 0;


    // 页面入口
    $(document).ready(function () {
        mini.parse();
        bind();
        initPermission();
        loadTree();
    });

    function bind() {

    }

    function initPermission() {
        if (!_isHasPermission(_EPermissionType.add)) {
            $("#btnMenuAdd").remove();
        }
        if (!_isHasPermission(_EPermissionType.update) && !_isHasPermission(_EPermissionType.del)) {
            mini.get("treegrid1").hideColumn("operation");
        }
    }

    function loadTree() {
        _showMask();

        $.ajax("/System/Menu/List", {
            method: "POST",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                _hideMask();

                if (data.Success) {
                    _menuData = data.Data;
                    var data1 = _convertToTreeData(data.Data, 1, 0);
                    //console.log(data1);
                    _menuTreeData = data1.dCopy();
                    mini.get("treegrid1").loadData(data1, "Id", "ParentId");
                }
                else {
                    mini.alert(data.Msg, _title);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                _hideMask();

                _menuData = [];
                _menuTreeData = [];
                mini.alert("查询数据失败, 请稍后再试或联系管理员", _title);
            }
        });
    }

    function onRefresh() {
        loadTree();
    }

    function onUrlRender(e) {
        if (e.row.Url == undefined || e.row.Url == null || e.row.Url == "") {
            return "--";
        }

        return e.row.Url;
    }

    function onLevelRender(e) {
        var str = _getMenuLevelText(e.value);

        return str;
    }

    function onOperationRender(e) {
        var id = e.row.Id;

        var str = '';

        if (_isHasPermission(_EPermissionType.update)) {
            str += '<a href="javascript: onUpdateMenuClick(' + id + ');">更新</a>';
        }
        if (_isHasPermission(_EPermissionType.del)) {
            if (str != "") {
                str += "&nbsp;&nbsp;";
            }
            str += '<a href="javascript: onDeleteMenuClick(' + id + ');">删除</a>';
        }

        if (str == "") {
            str = "--";
        }

        return str;
    }

    function onAdd() {
        var win = mini.get("win1");

        var arr = [];
        for (var i = 0; i < _menuTreeData.length; i++) {
            var item = _menuTreeData[i];
            if (item.IsDir == true) {
                arr.push(item);
            }
        }

        var root = {
            Id: 0,
            Name: "Root",
            Url: "",
            IsDir: true,
            Remark: "Root",
            Rank: 1,
            Level: 0,
            ParentId: -1
        };
        arr.unshift(root);

        var tree = mini.get("select1");
        tree.tree.loadList(arr);
        tree.setValue("0");

        var text = _getMenuLevelText(1);
        $("#lbAddMenuLevel").text(text);
        tree.on("valuechanged", function (e) {
            var row = getSelectData(e.value);
            var level = row.Level + 1;
            var text1 = _getMenuLevelText(level);
            $("#lbAddMenuLevel").text(text1);
        });

        _bindComboBox("selAddMenuIsDir", 0);
        mini.get("selAddMenuIsDir").on("valuechanged", onDirValueChanged);
        toggleDir();

        var nameObj = mini.get("txtAddMenuName");
        nameObj.setValue("");
        nameObj.on("validation", onNameValidation);

        mini.get("txtAddMenuUrl").setValue("");
        mini.get("txtAddMenuRemark").setValue("");
        mini.get("txtAddMenuRank").setValue("1");

        _isAdd = true;

        win.show();
        win.setTitle("新增菜单");
    }

    function getSelectData(value) {
        var row = {
            Id: 0,
            Name: "Root",
            Url: "",
            IsDir: true,
            Remark: "Root",
            Rank: 1,
            Level: 0,
            ParentId: -1
        };

        var id = parseInt(value, 10);
        for (var i = 0; i < _menuData.length; i++) {
            var item = _menuData[i];
            if (item.Id = id) {
                row = item;

                break;
            }
        }

        return row;
    }

    function onDirValueChanged() {
        toggleDir();
    }

    function toggleDir() {
        var nameObj = mini.get("txtAddMenuUrl");
        var el = $("#win1 tr[urlAttr=true]").eq(0);
        var value = mini.get("selAddMenuIsDir").getValue();
        if (value == 1) {
            el.hide();
            nameObj.required = false;
            nameObj.un("validation");
        } else {
            el.show();
            nameObj.required = true;
            nameObj.on("validation", onUrlValidation);
        }
    }

    function onUrlValidation(e) {

    }

    function onNameValidation(e) {
        if (e.isValid) {
            if (!demo.reg.isName(e.value)) {
                e.isValid = false;
            }
        }
    }

    function onOkClick() {
        var form = new mini.Form("#win1");
        form.validate();
        if (form.isValid() == false) {
            return;
        }

        var parentId = parseInt(mini.get("select1").getValue(), 10);
        var row = getSelectData(mini.get("select1").getValue());
        var level = row.Level + 1;
        var isDir = parseInt(mini.get("selAddMenuIsDir").getValue(), 10) == 1;
        var rank = parseInt(mini.get("txtAddMenuRank").getValue(), 10);
        var name = mini.get("txtAddMenuName").getValue();
        var url = mini.get("txtAddMenuUrl").getValue();
        var remark = mini.get("txtAddMenuRemark").getValue();

        if (_isAdd) {
            addMenu(parentId, level, isDir, rank, name, url, remark);
        }
        else {
            updateMenu(parentId, level, isDir, rank, name, url, remark);
        }
    }

    function addMenu(parentId, level, isDir, rank, name, url, remark) {
        mini.confirm("是否确定新增数据?", _title, function (e) {
            if (e == "ok") {
                _showMask();

                $.ajax("/System/Menu/Add", {
                    dataType: "json",
                    method: "POST",
                    data: {
                        ParentId: parentId,
                        Level: level,
                        IsDir: isDir,
                        Rank: rank,
                        Name: name,
                        Url: url,
                        Remark: remark
                    },
                    success: function (data, textStatus, jqXHR) {
                        _hideMask();

                        if (data.Success) {
                            mini.alert("新增菜单数据成功", _title, function () {
                                var win = mini.get("win1");
                                win.hide();

                                loadTree();
                            });
                        } else {
                            mini.alert(data.Msg, _title);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        _hideMask();

                        mini.alert("新增数据失败, 请稍后再试或联系管理员", _title);
                    }
                });
            }
        });
    }

    function onCancelClick() {
        var win = mini.get("win1");
        win.hide();
    }

    function onDeleteMenuClick(id) {
        //console.log(id);

        mini.confirm("是否确定要删除此菜单及其所属所有菜单数据?", _title, function (e) {
            if (e == "ok") {
                _showMask();

                $.ajax("/System/Menu/Delete", {
                    method: "POST",
                    dataType: "json",
                    data: {
                        Id: id
                    },
                    success: function (data, textStatus, jqXHR) {
                        _hideMask();

                        if (data.Success) {
                            mini.alert("删除菜单数据成功", _title, function () {
                                loadTree();
                            });
                        } else {
                            mini.alert(data.Msg, _title);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        _hideMask();

                        mini.alert("删除菜单数据失败, 请稍后再试或联系管理员", _title);
                    }
                });
            }
        });
    }

    function onUpdateMenuClick(id) {
        _id = id;
        var row = mini.get("treegrid1").findRow(function (e) {
            if (e.Id == id) {
                return true;
            }
            return false;
        });
        //console.log("row: %o", row);

        var win = mini.get("win1");
        //console.log("win: %o", win);        

        var arr = [];
        for (var i = 0; i < _menuTreeData.length; i++) {
            var item = _menuTreeData[i];
            if (item.IsDir == true) {
                arr.push(item);
            }
        }

        var root = {
            Id: 0,
            Name: "Root",
            Url: "",
            IsDir: true,
            Remark: "Root",
            Rank: 1,
            Level: 0,
            ParentId: -1
        };
        arr.unshift(root);

        var tree = mini.get("select1");
        tree.tree.loadList(arr);
        tree.setValue(row.ParentId.toString());

        var text = _getMenuLevelText(row.Level);
        $("#lbAddMenuLevel").text(text);
        tree.on("valuechanged", function (e) {
            var row = getSelectData(e.value);
            var level = row.Level + 1;
            var text1 = _getMenuLevelText(level);
            $("#lbAddMenuLevel").text(text1);
        });

        _bindComboBox("selAddMenuIsDir", row.IsDir == true ? 1 : 0);
        mini.get("selAddMenuIsDir").on("valuechanged", onDirValueChanged);
        toggleDir();

        var nameObj = mini.get("txtAddMenuName");
        nameObj.setValue(row.Name);
        nameObj.on("validation", onNameValidation);

        mini.get("txtAddMenuUrl").setValue(row.Url);
        mini.get("txtAddMenuRemark").setValue(row.Remark);
        mini.get("txtAddMenuRank").setValue(row.Rank.toString());

        _isAdd = false;

        win.show();
        win.setTitle("更新菜单");
    }

    function updateMenu(parentId, level, isDir, rank, name, url, remark) {
        mini.confirm("是否确定更新菜单数据?", _title, function (e) {
            if (e == "ok") {
                _showMask();

                $.ajax("/System/Menu/Update", {
                    dataType: "json",
                    method: "POST",
                    data: {
                        Id: _id,
                        ParentId: parentId,
                        Level: level,
                        IsDir: isDir,
                        Rank: rank,
                        Name: name,
                        Url: url,
                        Remark: remark
                    },
                    success: function (data, textStatus, jqXHR) {
                        _hideMask();

                        if (data.Success) {
                            mini.alert("更新菜单数据成功", _title, function () {
                                var win = mini.get("win1");
                                win.hide();

                                loadTree();
                            });
                        } else {
                            mini.alert(data.Msg, _title);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        _hideMask();

                        mini.alert("更新数据失败, 请稍后再试或联系管理员", _title);
                    }
                });
            }
        });
    }


    window.onUrlRender = onUrlRender;
    window.onLevelRender = onLevelRender;
    window.onOperationRender = onOperationRender;
    window.onAdd = onAdd;
    window.onOkClick = onOkClick;
    window.onCancelClick = onCancelClick;
    window.onDeleteMenuClick = onDeleteMenuClick;
    window.onUpdateMenuClick = onUpdateMenuClick;
    window.onRefresh = onRefresh;

})();