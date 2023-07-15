using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.DTOs.Base
{
    public class BaseDto<TKey> : ISerializable
        where TKey : struct //Constraint TKey
    {
        [HiddenInput(DisplayValue = false)]
        public TKey Id { get; set; }

        
        public bool IsActive_ { get; set; } = true;
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.ObjectType.GetProperties()
            //    .Where(w => !(typeof(System.Collections.IEnumerable).IsAssignableFrom(w.PropertyType) && w.PropertyType != typeof(string)))
            //    .ToList()
            //    .ForEach(prop => info.AddValue(prop.Name, prop.GetValue(this), prop.PropertyType));
        }
    }
}
