using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    interface IProductRepo
    {
        IEnumerable<IProduct> Products { get; }

        bool SaveProduct(IProduct product);

        bool DeleteProductByID(Guid id);
    }
}
