using HomeForMe.Data.Common.Repositories;
using HomeForMe.Data.Models;
using HomeForMe.Services.Data.Contracts;
using HomeForMe.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeForMe.Services.Data
{
    public class PropertyService : IPropertyService
    {
        private readonly IDeletableEntityRepository<Property> _propertyRepository;

        public PropertyService(IDeletableEntityRepository<Property> propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task Create(Property entity)
        {
            await _propertyRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll<T>(bool? withDeleted = false, int? count = null)
        {
            IQueryable<Property> query = null;

            if (withDeleted != null && withDeleted.Value)
            {
                query = (await _propertyRepository.AllWithDeleted()).OrderByDescending(x => x.Id);
            }
            else
            {
                query = (await _propertyRepository.All()).OrderByDescending(x => x.AddedAt);
            }

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            if (typeof(T) == typeof(Property))
            {
                return query.Select(x => (T)(object)x).ToList();
            }

            return query.To<T>().ToList();
        }

        public async Task<T> GetById<T>(int id, bool? withDeleted = false)
        {
            IQueryable<Property> query = null;

            if (withDeleted != null && withDeleted.Value)
            {
                query = await _propertyRepository.AllWithDeleted();
            }
            else
            {
                query = await _propertyRepository.All();
            }

            if (typeof(T) == typeof(Property))
            {
                return (T)(object)(query.Where(x => x.Id == id).FirstOrDefault());
            }

            return query.Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public async Task Update(Property entity)
        {
            _propertyRepository.Update(entity);
            await _propertyRepository.SaveChangesAsync();
        }

        public async Task Delete(Property entity)
        {
            _propertyRepository.Delete(entity);
            await _propertyRepository.SaveChangesAsync();
        }

        public async Task Undelete(Property entity)
        {
            _propertyRepository.Undelete(entity);
            await _propertyRepository.SaveChangesAsync();
        }
    }
}
