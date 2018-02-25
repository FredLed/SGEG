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
    [KnownType(typeof(ICategory))]
    public class Category : ICategory
    {
        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public ICategory ParentCategory { get; set; }

        [DataMember]
        public List<ICategory> SubCategories { get; set; }
    }
}
