using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    // specification is design pattern used to build dynamic query 
    public interface ISpecification<T> where T : BaseEntity
    {
        //interface contain segniture for prop for each query component

        public Expression<Func<T, bool>> Criteria { get; set; } // this criteria contain condition passed to where 

        public List<Expression<Func<T,object>>> Includes { get; set; }

    }
}
