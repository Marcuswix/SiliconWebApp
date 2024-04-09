using Infrastructure.Contexts;
using Infrastructure.Models;
using Infrastructure.Factories;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly UserContext _userContext;

        protected BaseRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        //CRUD - SOLID (Ska bara göra en sak)

        //Create
        //virtual = går att ändra på, async = att den ska inväntas, Task<???> = detta är vad den ska skicka tillbaka, och det i parentesen är vad den tar med sig in... 
        public virtual async Task<RepositoriesResult> CreateOneAsync(TEntity entity)
        {
            try 
            {
                await _userContext.Set<TEntity>().AddAsync(entity);
                await _userContext.SaveChangesAsync();
                return ResponseFactory.Ok(entity);
            }
            catch (Exception ex) {Debug.WriteLine("CreateOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        //Read
        public virtual async Task<RepositoriesResult> GetAllAsync()
        {
            try
            {
                var list = await _userContext.Set<TEntity>().ToListAsync();
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
                var entityToGet = await _userContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

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
                var entityToUpdate = await _userContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if (entityToUpdate != null)
                {
                    // Uppdatera alla egenskaper utom Id
                    _userContext.Entry(entityToUpdate).CurrentValues.SetValues(updatedEntity);

                    await _userContext.SaveChangesAsync();
                }

                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }


        public virtual async Task<RepositoriesResult> DeleteOneAsync(Expression<Func<TEntity, bool>> predicate, string id)
        {
            try
            {   
                var result = await _userContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if(result != null)
                {
                    _userContext.Set<TEntity>().Remove(result);
                    await _userContext.SaveChangesAsync();
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
                var result = await _userContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

                if(result == null)
                {
                    return ResponseFactory.NotFound();
                }

                return ResponseFactory.Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
