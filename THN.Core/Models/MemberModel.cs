using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THN.Core.Models
{
    public class MemberModel
    {
        public int ID { get; set; }
        public int FuncID { get; set; }
        public int UserID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MemberModelView
    {
        public int ID { get; set; }
        public int FuncID { get; set; }
        public int UserID { get; set; }
    }
}
