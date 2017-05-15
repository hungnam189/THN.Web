  $(function () {
    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });

    //var ckeditor = CKEDITOR.document.getById('Detail');
    CKEDITOR.replace('Detail');

    $('#btnSubmit').click(function (e) {
        e.preventDefault();
        var form = $('.box-body').find('form');
        $(form).submit();
    });
    
     $('#Name').off('keyup').on('keyup', function () {
        $this = $(this);
        var value = $this.val();
        $('#Slug').val(changeTitleToSlug(value));
    });
    
});