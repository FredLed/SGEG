using SGEGService.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Repository.SQLRepo
{
    class SQLUserRepo : IUserRepo
    {
        public IEnumerable<IUser> Users => throw new NotImplementedException();

        public bool DeleteUserByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool SaveUser(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
