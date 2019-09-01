using System;
using System.Collections.Generic;

namespace WebApp.BL.Interface
{
    public interface ICategoryRepository
    {
        List<ICategory> Caterogies { get; }

        bool SaveCategory(ICategory category);

        bool DeleteCategoryById(Guid id);

        ICategory GetCategoryById(Guid id);
    }
}