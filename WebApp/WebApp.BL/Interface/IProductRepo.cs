using System;
using System.Collections.Generic;

namespace WebApp.BL.Interface
{
    public interface IProductRepo
    {
        List<IProduct> Products { get; }

        bool SaveProduct(IProduct product);

        bool DeleteProductById(Guid id);

        IProduct GetProductById(Guid id);

        IEnumerable<IProduct> GetProductsByCategory(ICategory category);
    }
}