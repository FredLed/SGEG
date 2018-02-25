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
    [KnownType(typeof(ICategory))]
    public class Product : IProduct
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double MSRP { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public string CUP { get; set; }

        [DataMember]
        public ICategory Category { get; set; }
    }
}
