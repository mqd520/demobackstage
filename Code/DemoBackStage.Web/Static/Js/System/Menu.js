(function () {
    var _menuData = [];

    $(document).ready(function () {
        mini.parse();
        bind();
        loadTree();
    });

    function bind() {

    }

    function loadTree() {
        demo.ui.showMask();

        $.ajax("/System/Menu/List", {
            method: "POST",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                demo.ui.hideMask();

                if (data.Success) {
                    _menuData = data.Data;
                    mini.get("treegrid1").loadList(_menuData, "Id", "ParentId");
                }
                else {
                    mini.alert(data.Msg, demo.cfg.title);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                demo.ui.hideMask();

                _menuData = [];
                mini.alert("查询数据失败, 请稍后再试或联系管理员", demo.cfg.title);
            }
        });
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

    }

    function onAddOkClick() {
        var form = new mini.Form("#win1");
        form.validate();
        if (form.isValid() == false) {
            return;
        }

        mini.confirm("是否确定新增数据?", demo.cfg.title, function (e) {
            if (e == "ok") {
                demo.ui.showMask();

                $.ajax("", {
                    datatype: "json",
                    method: "POST",
                    success: function (data, textStatus, jqXHR) {
                        demo.ui.hideMask();

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        demo.ui.hideMask();

                        mini.alert("新增数据失败, 请稍后再试或联系管理员", demo.cfg.title);
                    }
                });
            }
        });
    }

    function onAddCancelClick() {
        var win = mini.get("win1");
        win.hide();
    }


    window.onUrlRender = onUrlRender;
    window.onLevelRender = onLevelRender;
    window.onAdd = onAdd;
    window.onAddOkClick = onAddOkClick;
    window.onAddCancelClick = onAddCancelClick;

})();