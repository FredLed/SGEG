using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SGEGService.Data
{
    [DataContract]
    class User
    {
        [DataMember]
        string UserName { get; set; }

        [DataMember]
        string Password { get; set; }

        [DataMember]
        string Email { get; set; }
    }
}
