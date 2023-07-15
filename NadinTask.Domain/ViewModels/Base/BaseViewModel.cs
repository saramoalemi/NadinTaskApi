using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace NadinTask.Domain.ViewModels.Base
{
    public class BaseViewModel<TKey>
        where TKey : struct //Constraint TKey
    {
        public TKey Id { get; set; }
        public bool IsActive_ { get; set; }
    }
}
