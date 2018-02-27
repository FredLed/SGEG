using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    public interface IItem
    {
        Guid ID { get; }

        IProduct Product { get; }

        double Cost { get; }

        string SerialNumber { get; }

        DateTime CreationDate { get; }

        DateTime ReceptionDate { get; }
    }
}
