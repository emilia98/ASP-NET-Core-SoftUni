using HomeForMe.Data.Common.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HomeForMe.Data.Common.Repositories
{
    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        Task<IQueryable<TEntity>> AllWithDeleted();

        //IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
