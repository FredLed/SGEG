using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    interface IProduct
    {
        Guid ID { get; }

        string Name { get; }

        double Price { get; }

        string Description { get; }

        DateTime CreationDate { get; }

        string CUP { get; }
    }
}
