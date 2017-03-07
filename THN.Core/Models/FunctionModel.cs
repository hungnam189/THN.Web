using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace THN.Core.Models
{
    public class FunctionModel
    {
        [ReadOnly(true)]
        [Display(Name = "Mã", Description ="Mã chức năng", AutoGenerateField = false)]
        public int ID { get; set; }

        [Display(Name="Tên")]
        [Required(ErrorMessage ="Tên chức năng không được để trống")]
        [MinLength(3, ErrorMessage ="Tên quá ngắn, Ít nhất 10 ký tự")]
        [MaxLength(150, ErrorMessage ="Tên quá dài, không được nhiều hơn 150 kí tự")]
        public string Name { get; set; }

        [Display(Name="Mô tả")]
        [DataType(DataType.MultilineText)]
        [MinLength(10, ErrorMessage = "Mô tả quá ngắn, Ít nhất 10 ký tự")]
        [MaxLength(150, ErrorMessage = "Mô tả quá dài, không được nhiều hơn 250 kí tự")]
        public string Description { get; set; }

        [Display(Name="Url")]
        [Required(ErrorMessage ="Url không được để trống")]
        [MinLength(5, ErrorMessage = "Url không đúng")]
        public string Path { get; set; }

        [Display(Name = "Controller")]
        [Required(ErrorMessage = "Controller không được để trống")]
        [MinLength(3, ErrorMessage = "Controller ít nhất 5 kí tự")]
        [MaxLength(50, ErrorMessage = "Controller quá dài, không được nhiều hơn 50 kí tự")]
        public string ControllerName { get; set; }

        [Display(Name = "Action")]
        [Required(ErrorMessage = "Action không được để trống")]
        [MinLength(3, ErrorMessage = "Action ít nhất 5 kí tự")]
        [MaxLength(50, ErrorMessage = "Action quá dài, không được nhiều hơn 50 kí tự")]
        public string ActionName { get; set; }

        [Display(Name = "Hiển thị")]
        public bool Visbled { get; set; }

        [Display(Name = "Menu cha")]
        [Required(ErrorMessage = "Vui lòng chọn menu cha")]
        public int Parent { get; set; }

        [Display(Name = "Cấp độ")]
        [Required(ErrorMessage = "Vui lòng nhập cấp menu")]
        public int Level { get; set; }
    }
}
