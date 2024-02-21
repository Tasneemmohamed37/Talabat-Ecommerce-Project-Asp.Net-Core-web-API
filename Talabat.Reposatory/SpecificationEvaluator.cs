using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Reposatory
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        // return Iqueryable to filter list in DB first then got it in app
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (currentQuery, inculdeExpression) => currentQuery.Include(inculdeExpression));

            return query;
        }
    }
}
