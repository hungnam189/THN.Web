using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THN.Core.EntityFramework;
using THN.Core.Models;

namespace THN.Core.Interface
{
    interface IFProduct
    {
        List<Product> GetAll();
        List<Product> GetByCategoryID(int categoryId);
        List<Product> GetByName(string name);
        Product GetByID(int id);
        

        int Insert(ProductModel add);
        int Update(ProductModel update);
        int Delete(int id);
        int CountView(int id);
        int UpdateDetailView(int id);
        int ChangeVisibled(int id, int visibled);
        int ChangeIsNew(int id, int isnew);
        int ChangeIsHot(int id, int ishot);
        int ChangeIsHome(int id, int ishome);
        
    }
}
