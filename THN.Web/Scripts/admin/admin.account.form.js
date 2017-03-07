$(function () {

    $("#frmAccount").validate({
        rules: {
            Name: { required: true, minlength: 5, maxlength: 100 },
            Email: { required: true, email: true },
            Username: { required: true, minlength: 2, maxlength: 50 },
            Phone: { required: true, minlength: 10, maxlength: 20 },
            Password:{required:true, minlength: 6, maxlength: 50}
        },
        messages: {
            Name: { required: "Tên không được để trống.", minlength: "Tên it nhất 5 kí tự.", maxlength: "Tên nhiều nhất 100" },
            Email: { required: "Email không được để trống.", email: "Không đúng định dạng Email." },
            Username: { required: "Username không được để trống.", minlength: "Tên đăng nhập ít nhất 2 kí tự.", maxlength: "Tên đăng nhập nhiều nhất 50 kí tự." },
            Phone: { required: "Số điện thoại không được để trống.", minlength:"Số điện thoại ít nhất 10 số.", maxlength:"Số điện thoại không đúng." },
            Password: { required: "Password không được để trống.", minlength: "Password ít nhất 6 kí tự.", maxlength: "Password không được quá 50 kí tự." }

        }
    });
});