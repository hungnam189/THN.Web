//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THN.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductImage
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AltImages { get; set; }
        public string FileSave { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<bool> Visibled { get; set; }
        public int ProductID { get; set; }
    
        public virtual Product Product { get; set; }
    }
}