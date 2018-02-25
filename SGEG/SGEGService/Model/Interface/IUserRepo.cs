using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model.Interface
{
    public interface IUserRepo
    {
        List<IUser> Users { get; }

        bool SaveUser(IUser user);

        bool DeleteUserByID(Guid id);
    }
}
