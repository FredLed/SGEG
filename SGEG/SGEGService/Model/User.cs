using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SGEGService.Model.Interface;

namespace SGEGService.Model
{
    [DataContract]
    public class User : IUser
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }
    }
}
