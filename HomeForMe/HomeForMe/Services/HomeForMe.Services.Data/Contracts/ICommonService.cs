using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeForMe.Services.Data.Contracts
{
    public interface ICommonService<TEntity>
    {
        Task Create(TEntity entity);

        Task<IEnumerable<T>> GetAll<T>(bool? withDeleted = false, int? count = null);

        Task<T> GetById<T>(int id, bool? withDeleted = false);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);

        Task Undelete(TEntity entity);
    }
}
