$(function () {

    /// Init datatable
    $('#tblCategories').DataTable({
        "sPaginationType": "full_numbers",
        "bFilter": true
    });

    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });


    /// delete event
    $('#tblCategories').delegate('a[name="btnDelete"]', 'click', function (e) {
        e.preventDefault();
        $this = $(this);
        bootbox.confirm("Bạn có chắc muốn xoá ?", function () {
            $.ajax({
                url: '/Administrator/Category/Delete',
                type: 'POST',
                data: { cateId: $this.attr('data-id') },
                async: true,
                success: function (data) {
                    loadWaiting(false);
                    if (data == true) {
                        $.notify("Xoá thành công!", "success");
                        location.reload();
                    }
                    else
                        $.notify("Xoá không thành công!", "warn");
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

        return false;
    });

    /// visibled event
    $('.visibled').on('ifChanged', function (event) {
        event.preventDefault();
        $this = $(this);
        var propVisibled = $this.prop("checked");// ? 0 : 1;
        debugger
        bootbox.confirm("Bạn có chắc muốn thay đổi trạng thái hiển thị ?", function () {
            $.ajax({
                url: '/Administrator/Category/ChageVisibled',
                type: 'POST',
                data: { cateId: $this.val(), cateVisibled: propVisibled == true ? 1 : 0 },
                async: true,
                success: function (data) {
                    loadWaiting(false);
                    if (data == true) {
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


    /// textbox orderby change
    $('#tblCategories').delegate('.orderBy', 'change', function (e) {
        e.preventDefault();
        $this = $(this);
        var value = $this.val();
        var oldValue = $this.attr('data-value');
        if (parseInt(value) != parseInt(oldValue))
            $this.attr('data-change', 'true');
    });

    ///Update All OrderBy
    $('#btnUpdateOrderBy').click(function (e) {
        e.preventDefault();
        var lstOrderBy = $('#tblCategories').find('.orderBy');
        var list = Array();
        $.each(lstOrderBy, function (index, value) {
            $this = $(value);
            if ($this.attr('data-change') === 'true') {
                var item = {
                    'CateId': $this.attr('data-id'),
                    'OrderBy': $this.val()
                };
                list.push(item);
            }
        });
        if (list.length > 0) {
            $.ajax({
                url: '/Administrator/Category/ChangeOrderBy',
                type: 'POST',
                data: { json: JSON.stringify(list) },
                async: true,
                success: function (data) {
                    loadWaiting(false);
                    if (data.status == true) {
                        $.notify("Cập nhật thành công!", "success");
                        //location.reload();
                    }
                    else
                        $.notify("Cập nhật hiển thị không thành công!", "warn");
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
        }
    });
});