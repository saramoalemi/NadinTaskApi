using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.Mapping.Generic
{
    public class Source<T>
    {
        public T Value { get; set; }
    }

    public class Destination<T>
    {
        public T Value { get; set; }
    }
    public class AutoMapperGenericsHelper<TSource, TDestination> //: Profile
    {
        public AutoMapperGenericsHelper()
        {
          //  CreateMap(typeof(TSource), typeof(TDestination));
        }

    }
}
