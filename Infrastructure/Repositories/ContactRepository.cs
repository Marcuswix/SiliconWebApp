using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public class ContactRepository 
    {
        private readonly DataContext _context;

        public ContactRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<RepositoriesResult> SendMessage(ContactMessageModel model)
        {
            try
            {
                if(model != null)
                {
                    var contactEntity = new ContactMessageEntity
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Service = model.Service,
                        Message = model.Message,
                    };

                    if(contactEntity != null)
                    {
                        var result = await _context.ContactMessages.AddAsync(contactEntity);
                        await _context.SaveChangesAsync();
                        return ResponseFactory.Ok();
                    }

                }

                return ResponseFactory.Error();

            }
            catch (Exception ex) {
                return ResponseFactory.Error();
            }
        }

        public async Task<RepositoriesResult> SendApplication(ContactCareersModel model)
        {
            try
            {
                if (model != null)
                {
                    var applicationEntity = new ContactCareersEntity
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Message = model.Message,
                        Career = model.Career,
                    };

                    var result = await _context.ContactCareers.AddAsync(applicationEntity);
                    await _context.SaveChangesAsync();
                    return ResponseFactory.Ok();
                }

                return ResponseFactory.Error();

            }
            catch (Exception ex)
            {
                return ResponseFactory.Error();
            }
        }
    }
}
