using System;
using System.Collections.Generic;

namespace WebApp.BL.Interface
{
    public interface ICategory
    {
        Guid Id { get; }

        string Name { get; }

        string Description { get; }

        ICategory ParentCategory { get; }

        List<ICategory> SubCategories { get; }
    }
}