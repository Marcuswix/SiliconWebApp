using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        private readonly UserContext _userContext;

        public UserRepository(UserContext userContext) : base(userContext)
        {
            _userContext = userContext;
        }

        public override async Task<RepositoriesResult> GetAllAsync()
        {
            try
            {
                var result = await _userContext.Users
                    .Include(x => x.Address)
                    .ToListAsync();

                if(result != null)
                {
                    return ResponseFactory.Ok(result);
                }
                    return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllAsyncUsers" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<RepositoriesResult> GetOneUserAsync(SignInModel model)
        {
            try
            {
                var result = await _userContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (result != null)
                {
                    return ResponseFactory.Ok(result);
                }

                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOneAsyncUsers" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<RepositoriesResult> GetUserInfoAsync(UserModel model)
        {
            try
            {
                var result = await _userContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (result != null)
                {
                    return ResponseFactory.Ok(result);
                }

                return ResponseFactory.NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetUserInfoAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
