(function () {
    var _isHasPermission = demo.ui.isHasPermission;
    var _EPermissionType = demo.consts.enum.EPermissionType;
    var _convertToTreeData = demo.ui.convertToTreeData;
    var _showMask = demo.ui.showMask;
    var _hideMask = demo.ui.hideMask;
    var _title = demo.cfg.title;
    var _menuData = [];


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
                    var data1 = _convertToTreeData(data.Data, 1, 0);
                    //console.log(data1);
                    _menuData = data1.dCopy();
                    mini.get("treegrid1").loadData(data1, "Id", "ParentId");
                }
                else {
                    mini.alert(data.Msg, _title);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                _hideMask();

                _menuData = [];
                mini.alert("查询数据失败, 请稍后再试或联系管理员", _title);
            }
        });
    }

    function onRefresh() {
        loadTree();
    }

    function onUrlRender(e) {
        if (e.value == "") {
            return "--";
        }

        return e.value;
    }

    function onLevelRender(e) {
        var str = demo.consts.getMenuLevelText(e.value);

        return str;
    }

    function onOperationRender(e) {
        var id = e.row.Id;

        var str = '';

        if (_isHasPermission(_EPermissionType.update)) {
            str += '<a href="javascript: update(' + id + ');">更新</a>';
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
        for (var i = 0; i < _menuData.length; i++) {
            var item = _menuData[i];
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

        var obj = mini.get("select1");
        obj.tree.loadList(arr);
        obj.setValue("0");

        var text = demo.consts.getMenuLevelText(1);
        $("#lbAddMenuLevel").text(text);
        obj.on("valuechanged", function (e) {
            var level = parseInt(e.value, 10);
            var text1 = demo.consts.getMenuLevelText(level + 1);
            $("#lbAddMenuLevel").text(text1);
        });

        demo.ui.bindComboBox("selAddMenuIsDir", 0);
        mini.get("selAddMenuIsDir").on("valuechanged", onDirValueChanged);
        toggleDir();

        var nameObj = mini.get("txtAddMenuName");
        nameObj.setValue("");
        nameObj.on("validation", onNameValidation);

        mini.get("txtAddMenuUrl").setValue("");
        mini.get("txtAddMenuRemark").setValue("");
        mini.get("txtAddMenuRank").setValue("1");

        win.show();
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

    function onAddOkClick() {
        var form = new mini.Form("#win1");
        form.validate();
        if (form.isValid() == false) {
            return;
        }

        var parentId = parseInt(mini.get("select1").getValue(), 10);
        var level = parseInt(mini.get("select1").getValue(), 10) + 1;
        var isDir = parseInt(mini.get("selAddMenuIsDir").getValue(), 10) == 1;
        var rank = parseInt(mini.get("txtAddMenuRank").getValue(), 10);
        var name = mini.get("txtAddMenuName").getValue();
        var url = mini.get("txtAddMenuUrl").getValue();
        var remark = mini.get("txtAddMenuRemark").getValue();

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

    function onAddCancelClick() {
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


    window.onUrlRender = onUrlRender;
    window.onLevelRender = onLevelRender;
    window.onOperationRender = onOperationRender;
    window.onAdd = onAdd;
    window.onAddOkClick = onAddOkClick;
    window.onAddCancelClick = onAddCancelClick;
    window.onDeleteMenuClick = onDeleteMenuClick;
    window.onRefresh = onRefresh;

})();