using SGEGService.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SGEGService.Model
{
    [DataContract]
    public class Item : IItem
    {
        [DataMember]
        public Guid ID { get; set; }
        
        [DataMember]
        public IProduct Product { get; set; }
        
        [DataMember]
        public double Cost { get; set; }
       
        [DataMember]
        public string SerialNumber { get; set; }
 
        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime ReceptionDate { get; set; }
    }
}
