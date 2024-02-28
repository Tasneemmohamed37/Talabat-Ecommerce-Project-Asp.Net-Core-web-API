using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //IReadOnlyList best performance in case of casing & readable random access without filtretion or Iteration or insert , update ,delete
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
    }
}
