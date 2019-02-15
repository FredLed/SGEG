using System;
using System.Collections.Generic;

namespace WebApp.BL.Interface
{
    public interface IItemRepo
    {
        List<IItem> Items { get; }

        bool SaveItem(IItem item);

        bool DeleteItemById(Guid id);

        IEnumerable<IItem> GetItemsByProductId(Guid id);

        IItem GetItemById(Guid id);
    }
}