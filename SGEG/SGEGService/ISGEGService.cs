using SGEGService.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SGEGService
{
    /// <summary>
    /// Public fonctionnality for every users.
    /// </summary>
    [ServiceContract(Name = "ISGEGPublicService")]
    public interface ISGEGPublicService
    {
        [OperationContract]
        bool SignUp(IUser user);

        [OperationContract]
        IUser GetUser();

        [OperationContract]
        bool Login(IUser user);

        [OperationContract]
        bool Logout();

        [OperationContract]
        bool AddProduct(IProduct product);

        [OperationContract]
        IEnumerable<IProduct> GetAllProducts();

        [OperationContract]
        Dictionary<ICategory, IEnumerable<IProduct>> GetProductsByCategory(ICategory category = null);

        [OperationContract]
        IProduct GetProductByID(Guid id);

        [OperationContract]
        bool AddProductCategory(ICategory category);

        [OperationContract]
        IEnumerable<ICategory> GetAllCategories();

        [OperationContract]
        ICategory GetCategoryByID(Guid id);

        [OperationContract]
        bool AddProductItem(IItem item);

        [OperationContract]
        IEnumerable<IItem> GetAllItems();

        [OperationContract]
        IItem GetItemByID(Guid id);

        [OperationContract]
        Dictionary<IProduct, IEnumerable<IItem>> GetItemsByProduct(IProduct IProduct = null);
    }

    /// <summary>
    /// Private fonctionnality for local users.
    /// </summary>
    [ServiceContract(Name = "ISGEGPrivateService")]
    public interface ISGEGPrivateService
    {
        [OperationContract(Name = "GetPrivateMessage")]
        string GetPrivateMessage();
    }
}
