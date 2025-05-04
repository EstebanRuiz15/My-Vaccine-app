
using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Interfaces;

namespace My_vaccine_app.Repositories.Implementations
{
    public class BaseRepository <T>: IBaseRepository<T> where T: class, new()
    {
        private readonly MyVaccineAppDbContext _context;
        public BaseRepository(MyVaccineAppDbContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();

            var CreateAt = entity.GetType().GetProperty("CreateAt");
            if (CreateAt != null) entity.GetType().GetProperty("CreateAt").SetValue(entity, DateTime.Now);

            var UpdateAt = entity.GetType().GetProperty("UpdateAt");
            if(UpdateAt != null)entity.GetType().GetProperty("UpdateAt").SetValue(entity, DateTime.Now);

        }

        public async Task AddRange(List<T> entity)
        {
            entity = entity.Select(x =>
            {
                if( x.GetType().GetProperty("CreateAt") != null) x.GetType().GetProperty("CreateAt").SetValue(x, DateTime.Now);
                if (x.GetType().GetProperty("UpdateAt") != null) x.GetType().GetProperty("UpdateAt").SetValue(x, DateTime.Now);
                return x;
            }).ToList();

            _context.AddRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<T> entity)
        {
            _context.RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
           return GetAll().Where(predicate);
        }

        public IQueryable<T> FindByAsNoTracking(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
           return GetAll().AsNoTracking().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            var EntitySet= _context.Set<T>();
            return EntitySet.AsQueryable();
        }

        public async Task Patch(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var key = entity.GetType().GetProperty("Id").GetValue(entity, null);
                var originalEntity= await _context.Set<T>().FindAsync(key);
            
                entry= _context.Entry(originalEntity);
                entry.CurrentValues.SetValues(entity);
            }

            var updateAt = entity.GetType().GetProperty("UpdateAt");
            if (updateAt != null)
            {
                updateAt.SetValue(entity, DateTime.Now);
                entry.Property("UpdateAt").IsModified = true;
            }

            var ChangeProperties = entry.Properties
                .Where(p => p.IsModified)
                .Select(p => p.Metadata.Name);

            foreach(var name in ChangeProperties)
            {
                entry.Property(name).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task PatchRange(List <T> entities)
        {
            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    var key = entity.GetType().GetProperty("Id").GetValue(entity, null);
                    var originalEntity = await _context.Set<T>().FindAsync(key);

                    entry = _context.Entry(originalEntity);
                    entry.CurrentValues.SetValues(entity);
                }

                var updateAt = entity.GetType().GetProperty("UpdateAt");
                if (updateAt != null)
                {
                    updateAt.SetValue(entity, DateTime.Now);
                    entry.Property("UpdateAt").IsModified = true;
                }

                var ChangeProperties = entry.Properties
                    .Where(p => p.IsModified)
                    .Select(p => p.Metadata.Name);

                foreach (var name in ChangeProperties)
                {
                    entry.Property(name).IsModified = true;
                }

            }
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            var UpdateAt = entity.GetType().GetProperty("UpdateAt");
            if(UpdateAt != null)
            {
                UpdateAt.SetValue(entity, DateTime.Now);
            }

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(List<T> entity)
        {
            entity = entity.Select(x =>
            {
                if (x.GetType().GetProperty("UpdateAt") != null) x.GetType().GetProperty("UpdateAt").SetValue(x, DateTime.Now);
                return x;
            }).ToList();

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
