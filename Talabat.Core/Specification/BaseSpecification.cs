using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {

        // this class include [ auto prop ] => compiler will generate backing field or hidden private attribute 'can`t access this field out side class'

        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set; } = new List<Expression<Func<T, object>>> ();

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        #region Pagination

        public int Take { get ; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        #endregion

        // this ctor used in case no criteria 'GetAll'
        public BaseSpecification()
        {
                
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
           Criteria = criteria;
        }

        public void AddOrderBy(Expression<Func<T , object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
                Skip = skip;
                Take = take;
            
        }


    }
}
