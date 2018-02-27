using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SGEGService.Model;
using SGEGService.Model.Interface;
using SGEGService.Repository.SQLRepo;

namespace SGEGService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "SGEGService" à la fois dans le code et le fichier de configuration.
    public class SGEGService : ISGEGPublicService, ISGEGPrivateService
    {
        private ICategoryRepo categoryRepo;
        private IProductRepo productRepo;
        private IItemRepo itemRepo;
        private IUserRepo userRepo;
        private IUser currentUser;

        public SGEGService()
        {
            categoryRepo = new SQLCategoryRepo();
            productRepo = new SQLProductRepo();
            itemRepo = new SQLItemRepo();
            userRepo = new SQLUserRepo();
        }

        public bool AddProduct(IProduct product)
        {
            try
            {
                return productRepo.SaveProduct(product);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public bool AddProductCategory(ICategory category)
        {
            try
            {
                return categoryRepo.SaveCategory(category);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool AddProductItem(IItem item)
        {
            try
            {
                 return itemRepo.SaveItem(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool SignUp(IUser user)
        {
            try
            {
                return userRepo.SaveUser(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetPublicMessage()
        {
            return "Public Message : Hello world!";
        }

        public IEnumerable<ICategory> GetAllCategories()
        {
            try
            {
                return categoryRepo.Caterogies;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<IItem> GetAllItems()
        {
            try
            {
                return itemRepo.Items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<IProduct> GetAllProducts()
        {
            try
            {
                return productRepo.Products;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ICategory GetCategoryByID(Guid id)
        {
            try
            {
                return categoryRepo.GetCategoryByID(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IItem GetItemByID(Guid id)
        {
            // return itemRepo.GetItemByID(id); TODO
            return null;
        }

        /// <summary>
        /// Get an dictionary of items by products. If a product parameter is supplied,
        /// there will be only one entry.
        /// </summary>
        /// <param name="product">To get items from this product</param>
        /// <returns></returns>
        public Dictionary<IProduct, IEnumerable<IItem>> GetItemsByProduct(IProduct product = null)
        {
            var dic = new Dictionary<IProduct, IEnumerable<IItem>>();

            try
            {
                if (product == null)
                {
                    foreach (var prod in productRepo.Products)
                    {
                        var items = itemRepo.GetItemsByProductID(prod.ID);

                        dic.Add(prod, items);
                    }
                }
                else
                {
                    dic.Add(product, itemRepo.GetItemsByProductID(product.ID));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            

            return dic;
        }

        public string GetPrivateMessage()
        {
            return "Private Message : YOLO !!";
        }

        public IProduct GetProductByID(Guid id)
        {
            try
            {
                return productRepo.GetProductByID(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get an dictionary of products by categories. If a category parameter is supplied,
        /// there will be only one entry.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Dictionary<ICategory, IEnumerable<IProduct>> GetProductsByCategory(ICategory category = null)
        {
            var dic = new Dictionary<ICategory, IEnumerable<IProduct>>();

            try
            {
                if (category == null)
                {
                    foreach (var cat in categoryRepo.Caterogies)
                    {
                        var products = productRepo.GetProductsByCategory(cat);

                        dic.Add(cat, products);
                    }
                }
                else
                {
                    dic.Add(category, productRepo.GetProductsByCategory(category));
                }
            }
            catch (Exception ex)
            {
                throw;
            }


            return dic;
        }

        public IUser GetUser(string username)
        {
            try
            {
                return userRepo.GetUserByName(username);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Login(string username, string password)
        {
            try
            {
                if (currentUser == null || currentUser.ID == Guid.Empty)
                {
                    currentUser = userRepo.GetUserByName(username);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           

            return false;
        }

        public void Logout()
        {
            currentUser = null;
        }
    }
}
