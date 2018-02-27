using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    public interface IProductRepo
    {
        List<IProduct> Products { get; }

        bool SaveProduct(IProduct product);

        bool DeleteProductByID(Guid id);

        IProduct GetProductByID(Guid id);

        IEnumerable<IProduct> GetProductsByCategory(ICategory category);
    }
}
