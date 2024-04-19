using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class SubscribeRepository
    {
        private readonly DataContext _dataContext;

        public SubscribeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //Create
        public async Task<RepositoriesResult> AddSubscriber(SubscribeModel model)
        {
            try
            {
                if (model != null)
                {
                    var exist = await _dataContext.Subscribers.AnyAsync(x => x.Email == model.Email);
                    
                    if(exist == false)
                    {
                        var entity = new SubscriberEntity
                        {
                            Email = model.Email,
                            Newsletter = model.Newsletter,
                            AdvertisingUpdates = model.AdvertisingUpdates,
                            EventUpdates = model.EventUpdates,
                            StartUps = model.StartUps,
                            Podcasts = model.Podcasts,
                            WeekInReview = model.WeekInReview,
                        };

                        var result = await _dataContext.Subscribers.AddAsync(entity);
                        await _dataContext.SaveChangesAsync();

                        return ResponseFactory.Ok(result);
                    }
                    if(exist == true)
                    {
                        return ResponseFactory.AlreadyExist();
                    }

                }
                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AddSubscriber" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        //Read
        public async Task<RepositoriesResult> GetAll()
        {
            try
            {
                    var result = await _dataContext.Subscribers.ToListAsync();
                    
                    if (result != null)
                    {
                        return ResponseFactory.Ok(result);
                    }
                    if (result!.Count == 0)
                    {
                        return ResponseFactory.NotFound();
                    }

            
                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAllSubscriber" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<RepositoriesResult> GetOne(string email)
        {
            try
            {

                    var result = await _dataContext.Subscribers.FirstOrDefaultAsync(x => x.Email == email);

                    if (result != null)
                    {
                        return ResponseFactory.Ok(result);
                    }
                    if (result == null)
                    {
                        return ResponseFactory.NotFound();
                    }

                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOneSubscriber" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<RepositoriesResult> Update(int id, SubscribeModel model)
        {
            try
            {
                if (model != null)
                {
                    var result = await _dataContext.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

                    if (result != null)
                    {
                        result.Email = model.Email;

                        await _dataContext.SaveChangesAsync();

                        return ResponseFactory.Ok();
                    }
                    if (result == null)
                    {
                        return ResponseFactory.NotFound();
                    }
                }

                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteSubscriber" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public async Task<RepositoriesResult> Delete(SubscribeModel model)
        {
            try
            {
                if (model != null)
                {
                    var result = await _dataContext.Subscribers.FirstOrDefaultAsync(x => x.Email == model.Email);

                    if (result != null)
                    {
                        _dataContext.Subscribers.Remove(result);
                        await _dataContext.SaveChangesAsync();

                        return ResponseFactory.Ok();
                    }
                    if (result == null)
                    {
                        return ResponseFactory.NotFound();
                    }
                }

                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteSubscriber" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }
    }
}
