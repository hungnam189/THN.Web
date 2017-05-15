using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THN.Core.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [MinLength(4, ErrorMessage = "Tên sản phẩm quá ngắn.")]
        [MaxLength(150, ErrorMessage = "Tên sản phẩm quá dài.")]
        public string Name { get; set; }

        [Display(Name = "Link rút gọn")]
        [Required(ErrorMessage = "Link rút gọn không được để trống.")]
        public string Slug { get; set; }

        [Display(Name = "Mô tả ngắn")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Mô tả ngắn không được để trống.")]
        [MinLength(4, ErrorMessage = "Mô tả ngắn quá ngắn.")]
        [MaxLength(200, ErrorMessage = "Mô tả ngắn quá dài.")]
        public string ShortDescription { get; set; }

        [Display(Name = "Nội dung")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Nội dung không được để trống.")]
        [MinLength(10, ErrorMessage = "Nội dung quá ngắn.")]
        public string Detail { get; set; }

        [Display(Name = "Hình đại diện")]
        public string Picture { get; set; }

        [Display(Name = "Tiêu đề SEO")]
        [MinLength(5, ErrorMessage = "Tiêu đề SEO quá ngắn.")]
        [MaxLength(150, ErrorMessage = "Tiêu đề SEO quá dài.")]
        public string MetaTitle { get; set; }

        [Display(Name = "Mô tả SEO")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Mô tả SEO không được để trống.")]
        [MinLength(10, ErrorMessage = "Mô tả SEO quá ngắn.")]
        public string MetaDescription { get; set; }

        [Display(Name = "Từ khoá SEO")]
        public string MetaKeyword { get; set; }

        //[Display(Name = "Từ khoá SEO")]
        //public string ProductTag { get; set; }
        [Display(Name="Giá sản phẩm")]
        public decimal PriceOld { get; set; }
        [Display(Name = "Giá sản phẩm Sale")]
        public decimal PriceNew { get; set; }

        [Display(Name = "Hiển thị")]
        public bool Visibled { get; set; }

        [Display(Name = "Sản phẩm mới")]
        public bool IsNew { get; set; }

        [Display(Name = "Hiển thị trang chủ")]
        public bool IsHome { get; set; }
        
        [Display(Name = "Sản phẩm Hot")]
        public bool IsHot { get; set; }

        [Display(Name = "Sắp xếp")]
        public int OrderBy { get; set; }

        public int CategoryID { get; set; }
        public int ManufactureID { get; set; }

    }
}
