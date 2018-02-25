using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    public interface IItemRepo
    {
        List<IItem> Items { get; }

        bool SaveItem(IItem item);

        bool DeleteItemByID(Guid id);
    }
}
