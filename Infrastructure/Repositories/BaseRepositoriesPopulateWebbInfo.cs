using Infrastructure.Contexts;
using Infrastructure.Models;
using Infrastructure.Factories;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepositoriesPopulateWebbInfo<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;

        protected BaseRepositoriesPopulateWebbInfo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //CRUD - SOLID (Ska bara göra en sak)

        //Create
        //virtual = går att ändra på, async = att den ska inväntas, Task<???> = detta är vad den ska skicka tillbaka, och det i parentesen är vad den tar med sig in... 
        public virtual async Task<RepositoriesResult> CreateOneAsync(TEntity entity)
        {
            try
            {
                await _dataContext.Set<TEntity>().AddAsync(entity);
                await _dataContext.SaveChangesAsync();
                return ResponseFactory.Ok(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CreateOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        //Read
        public virtual async Task<RepositoriesResult> GetAllAsync()
        {
            try
            {
                var list = await _dataContext.Set<TEntity>().ToListAsync();
                return ResponseFactory.Ok(list);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public virtual async Task<RepositoriesResult> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entityToGet = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if (entityToGet == null)
                {
                    return ResponseFactory.NotFound();
                }
                else
                {
                    return ResponseFactory.Ok(entityToGet);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        //Update
        public virtual async Task<RepositoriesResult> UpdateOneAsync(Expression<Func<TEntity, bool>> predicate, TEntity updatedEntity)
        {
            try
            {
                var entityToUpdate = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if (entityToUpdate != null)
                {
                    // Uppdatera alla egenskaper utom Id
                    _dataContext.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

                    await _dataContext.SaveChangesAsync();
                }

                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }


        public virtual async Task<RepositoriesResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate, int id)
        {
            try
            {
                var result = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if (result != null)
                {
                    _dataContext.Set<TEntity>().Remove(result);
                    await _dataContext.SaveChangesAsync();
                    return ResponseFactory.Ok("Successfully Removed");
                }

                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }

        }

        public virtual async Task<RepositoriesResult> AlreadyExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var result = await _dataContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if (result == null)
                {
                    return ResponseFactory.NotFound();
                }

                return ResponseFactory.Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
