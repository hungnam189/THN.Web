$(function () {
   
    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });

    // Khởi tạo datatable
    $('#tblProducts').DataTable({
        "sPaginationType": "full_numbers",
        "bFilter": true
    });

    var oTable = $('#tblProducts').DataTable();
    $('#tblProducts').delegate('.orderBy', 'change', function (e) {
        e.preventDefault();
        $this = $(this);
        var value = $this.val();
        var oldValue = $this.attr('data-value');
        if (parseInt(value) != parseInt(oldValue))
            $this.attr('data-change', 'true');
    });

    $('#Category').change(function (e) {
        e.preventDefault();
        oTable.columns(9).search($(this).val()).draw();
    });

    /* Table Event */
    // kết thúc thông báo tin
    $('a[name="btnDelete"]').on('click', function (e) {
        e.preventDefault();
        return confirm("Bạn có chắc muốn xoá sản phẩm này ?");
    });

    //
    /// visibled event
    //$('.visibled').off('ifChanged').on('ifChanged', function (event) {
    $('#tblProducts').delegate('.visibled', 'ifChanged', function (event) {
        event.preventDefault();
        $this = $(this);
        var propVisibled = $this.prop("checked");// ? 0 : 1;
        bootbox.confirm("Bạn có chắc muốn thay đổi trạng thái hiển thị ?", function () {
            $.ajax({
                url: '/Administrator/Product/ChageVisibled',
                type: 'POST',
                data: { pId: $this.val(), visibled: propVisibled == true ? 1 : 0 },
                async: true,
                success: function (data) {
                    loadWaiting(false);
                    if (data.status == true) {
                        $.notify("Cập nhật hiển thị thành công!", "success", "center");
                        //location.reload();
                    }
                    else
                        $.notify("Cập nhật hiển thị không thành công!", "warn", "center");
                },
                error: function (xhr, status, text) {
                    if (xhr.status == 0) {//Error het session
                        alert('Bạn đã rời phiên làm việc quá lâu! Trang web sẽ tự động tải lại');
                        location.reload();
                    }
                },
                beforeSend: function () {
                    loadWaiting(true);
                }
            });
        });
    });

    $('#tblProducts').delegate('.isnew', 'ifChanged', function (event) {
        event.preventDefault();
        $this = $(this);
        var propStatus = $this.prop("checked");// ? 0 : 1;
        var pId = $this.val();
        var action = "isnew";
        changeProductStatus(pId, propStatus, action);
    });

    $('#tblProducts').delegate('.ishome', 'ifChanged', function (event) {
        event.preventDefault();
        $this = $(this);
        var propStatus = $this.prop("checked");// ? 0 : 1;
        var pId = $this.val();
        var action = "ishome";
        changeProductStatus(pId, propStatus, action);
    });

    $('#tblProducts').delegate('.ishot', 'ifChanged', function (event) {
        event.preventDefault();
        $this = $(this);
        var propStatus = $this.prop("checked");// ? 0 : 1;
        var pId = $this.val();
        var action = "ishot";
        changeProductStatus(pId, propStatus, action);
    });

    /* END Table Event */
});

function changeProductStatus(pId, pStatus, pAction) {
    bootbox.confirm("Bạn có chắc muốn thay đổi trạng thái sản phẩm ?", function () {
        $.ajax({
            url: '/Administrator/Product/ChangeStatus',
            type: 'POST',
            data: { pId: pId, pStatus: pStatus == true ? 1 : 0, pAction: pAction },
            async: true,
            success: function (data) {
                loadWaiting(false);
                if (data.status == true) {
                    $.notify("Cập nhật trạng thái thành công!", "success");
                    //location.reload();
                }
                else
                    $.notify("Cập nhật trạng thái không thành công!", "warn");
            },
            error: function (xhr, status, text) {
                if (xhr.status == 0) {//Error het session
                    alert('Bạn đã rời phiên làm việc quá lâu! Trang web sẽ tự động tải lại');
                    location.reload();
                }
            },
            beforeSend: function () {
                loadWaiting(true);
            }
        });
    });
}