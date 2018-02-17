using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    interface IUser
    {
        Guid ID { get; }

        string UserName { get; }

        string Password { get;  }
        
        string Email { get; }

        string Address { get; }
    }
}
