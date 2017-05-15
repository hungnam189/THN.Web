using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THN.Core.Helper;

namespace THN.Core.Models
{
    [Serializable]
    public class CategoryModel
    {
        public int Id { get; set; }

        [Display(Name="Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [MinLength(4, ErrorMessage = "Tên quá ngắn")]
        [MaxLength(130, ErrorMessage = "Tên quá dài")]
        public string Name { get; set; }

        [Display(Name = "Link SEO")]
        public string Slug { get; set; }

        [Display(Name = "Hình ảnh")]
        public string Picture { get; set; }

        [Display(Name = "Danh mục cha")]
        public int Parent { get; set; }

        [Display(Name = "Hiển thị")]
        public int Visibled { get; set; }

        [Display(Name = "Sắp xếp")]
        public int OrderBy { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Mô tả SEO")]
        public string MetaDescription { get; set; }

        [Display(Name = "Tiêu đề SEO")]
        public string MetaTitle { get; set; }

        [Display(Name = "Từ khoá SEO")]
        public string MetaKeyWord { get; set; }

        public CategoryModel()
        {
            this.Name = this.Slug = this.Picture = this.MetaTitle = this.MetaKeyWord = this.MetaDescription = String.Empty;
            this.Parent = 0; this.Visibled = 1; this.OrderBy = 1;
        }
    }

    public class CategoryChangeOrderByModel
    {
        public int CateId { get; set; }
        public int OrderBy { get; set; }
    }
}
