using System.Collections.Generic;
using THN.Core.EntityFramework;

namespace THN.Core.Interface
{
    public interface IFFunction
    {
        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        List<Function> GetAll();
        /// <summary>
        /// Get Function By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Function GetByID(int id);

        List<Function> GetByParent(int parent);

        /// <summary>
        /// Insert new the Function
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        bool Insert(Function add);
        /// <summary>
        /// Edit Function
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        bool Update(Function add);

        /// <summary>
        /// Delete the function 
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        bool Delete(int id);


        bool GetAccess(string controller, string action, int userID);
    }
}
