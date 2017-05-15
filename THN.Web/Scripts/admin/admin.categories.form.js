$(function(){
    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        $('#btnSubmitForm').click();
    });

    $('#Name').off('keyup').on('keyup', function () {
        $this = $(this);
        var value = $this.val();
        $('#Slug').val(changeTitleToSlug(value));
    });

    $('#Visibled').on('ifChanged', function (event) {
        var isChecked = $(this).prop(":checked");
        isChecked == true ? $(this).val(1) : $(this).val(0);
    });

    $('#btnCancel').click(function (e) {
        e.preventDefault();
        var form = $('.box-body').find('form');
        $(form).get(0).reset();
    });

});