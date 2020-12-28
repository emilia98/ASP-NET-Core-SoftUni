using HomeForMe.Data.Common.Models;
using HomeForMe.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeForMe.Data.Repositories
{
    public class DeletableEntityRepository<TEntity> : Repository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        public DeletableEntityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IQueryable<TEntity>> All() => await base.All();

        public async Task<IQueryable<TEntity>> AllWithDeleted() => (await base.All()).IgnoreQueryFilters();

        public async Task<TEntity> GetByIdWithDeletedAsync(params object[] id)
        {
            var getByIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);
            return await (await this.AllWithDeleted()).FirstOrDefaultAsync(getByIdPredicate);
        }

        public void HardDelete(TEntity entity) => base.Delete(entity);

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedAt = null;
            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}
