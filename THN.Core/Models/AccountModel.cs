using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace THN.Core.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IPAdress { get; set; }
        public DateTime LastDateLogin { get; set; }
    }

    public class AccountModelView
    {
        [ReadOnly(true)]
        public int ID { get; set; }

        [Display(Name="Tên")]
        [Required(ErrorMessage ="Tên không được để trống.")]
        [MinLength(5, ErrorMessage ="Tên dài ít nhất 5 kí tự.")]
        [MaxLength(100, ErrorMessage ="Tên không được quá dài, tối đa 100 kí tự.")]
        public string Name { get; set; }

        [Display(Name="Email")]
        [Required(ErrorMessage ="Email không được để trống.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Tên đăng nhập không được để trống.")]
        [MinLength(5, ErrorMessage ="Tên đăng nhập ít nhất 5 kí tự.")]
        [MaxLength(50, ErrorMessage ="Tên đăng nhập tối đa 50 ki tự.")]
        public string Username { get; set; }

        [Display(Name="Địa chỉ")]
        public string Address { get; set; }

        [Display(Name="Điện thoại")]
        [Required(ErrorMessage = "Điện thoại không được để trống.")]
        [MinLength(10, ErrorMessage ="Số điện thoại không đúng.")]
        [MaxLength(11, ErrorMessage ="Số điện thoại không đúng, kiểm tra lại.")]
        public string Phone { get; set; }

        [Display(Name="Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password không được để trống.")]
        [MinLength(6, ErrorMessage = "Password ít nhất 6 kí tự.")]
        [MaxLength(50, ErrorMessage = "Password không quá 50 kí tự.")]
        public string Password { get; set; }
    }

    public class AccountModelLogin
    {
        [Display(Name = "Username hoặc Email")]
        [Required(ErrorMessage = "Tên đăng nhập hoặc Email không được để trống.")]
        [MinLength(2, ErrorMessage = "Tên đăng nhập ít nhất 2 kí tự.")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập không quá 50 kí tự.")]
        public string UsernameOrEmail { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password ít nhất 6 kí tự.")]
        [MaxLength(50, ErrorMessage = "Password không quá 50 kí tự.")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ")]
        public bool Remember { get; set; }
    }
}
