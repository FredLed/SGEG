using SGEGService.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Repository.SQLRepo
{
    class SQLProductRepo : IProductRepo
    {
        public IEnumerable<IProduct> Products => throw new NotImplementedException();

        public bool DeleteProductByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool SaveProduct(IProduct product)
        {
            throw new NotImplementedException();
        }
    }
}
