using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    public interface IProduct
    {
        Guid ID { get; }

        string Name { get; }

        double MSRP { get; }

        string Description { get; }

        DateTime CreationDate { get; }

        string CUP { get; }

        ICategory Category { get; }
    }
}
