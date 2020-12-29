using HomeForMe.Data.Common.Repositories;
using HomeForMe.Data.Models;
using HomeForMe.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeForMe.Services.Data
{
    public class LocationService : ILocationService
    {
        private readonly IDeletableEntityRepository<Location> _locationRepository;

        public LocationService(IDeletableEntityRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task Create(Location entity)
        {
            await _locationRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll<T>(bool? withDeleted = false, int? count = null)
        {
            IQueryable<Location> query = null;

            if (withDeleted != null && withDeleted.Value)
            {
                query = (await _locationRepository.AllWithDeleted()).OrderByDescending(x => x.Id);
            }
            else
            {
                query = (await _locationRepository.All()).OrderByDescending(x => x.AddedAt);
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            /*
            if (typeof(T) == typeof(Property))
            {
                return query.Select(x => (T)(object)x).ToList();
            }
            */
            // return 

            //TODO: FIX
            return query.Select(x => (T)(object)x).ToList();
        }

        public async Task<T> GetById<T>(int id, bool? withDeleted = false)
        {
            IQueryable<Location> query = null;

            if (withDeleted != null && withDeleted.Value)
            {
                query = await _locationRepository.AllWithDeleted();
            }
            else
            {
                query = await _locationRepository.All();
            }


            //TODO Fix
            return (T)(object)(await query.Where(x => x.Id == id).FirstOrDefaultAsync());
        }

        public async Task Update(Location entity)
        {
            _locationRepository.Update(entity);
            await _locationRepository.SaveChangesAsync();
        }

        public async Task Delete(Location entity)
        {
            _locationRepository.Delete(entity);
            await _locationRepository.SaveChangesAsync();
        }

        public async Task Undelete(Location entity)
        {
            _locationRepository.Undelete(entity);
            await _locationRepository.SaveChangesAsync();
        }
    }
}
