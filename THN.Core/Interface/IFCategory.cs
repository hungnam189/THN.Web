using System.Collections.Generic;
using THN.Core.Models;

namespace THN.Core.Interface
{
    interface IFCategory
    {
        List<CategoryModel> getAll();
        List<CategoryModel> getByParent(int parent);
        List<CategoryModel> getByCategoryName(string name);
        CategoryModel getById(int id);
        int changeVisibled(int catId, int visibled);
        int insert(CategoryModel add);
        int update(CategoryModel update);
        int delete(int id);
        int changeOrderBy(List<CategoryChangeOrderByModel> lst);
    }
}
