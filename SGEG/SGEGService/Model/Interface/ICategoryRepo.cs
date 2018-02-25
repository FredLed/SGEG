using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    public interface ICategoryRepo
    {
        List<ICategory> Caterogies { get; }

        bool SaveCategory(ICategory category);

        bool DeleteCategoryByID(Guid id);

        ICategory GetCategoryByID(Guid id);
    }
}
