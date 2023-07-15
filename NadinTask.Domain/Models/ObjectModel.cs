using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.Models
{
    public class ObjectModel<TKey> : ISerializable
        where TKey : struct //Constraint TKey
    {
        public TKey ID { get; set; }
        public bool IsActive_ { get; set; } = true;
        public bool IsDeleted_ { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.ObjectType.GetProperties()
                .Where(w => !(typeof(System.Collections.IEnumerable).IsAssignableFrom(w.PropertyType) && w.PropertyType != typeof(string)))
                .ToList()
                .ForEach(prop => info.AddValue(prop.Name, prop.GetValue(this), prop.PropertyType));
        }
    }
}
