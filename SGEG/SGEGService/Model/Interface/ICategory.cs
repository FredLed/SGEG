using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    public interface ICategory
    {
        Guid ID { get; }

        string Name { get; }

        string Description { get; }

        ICategory ParentCategory { get; }

        List<ICategory> SubCategories { get; }
    }
}
