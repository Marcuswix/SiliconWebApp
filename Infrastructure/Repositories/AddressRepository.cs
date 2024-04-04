
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<AddressEntity>
    {
        private readonly UserContext _userContext;

        public AddressRepository(UserContext userContext) : base(userContext)
        {
            _userContext = userContext;
        }

        public async Task<AddressEntity> GetOneAddressAsync(UserEntity entity)
        {
            try
            {
                if (entity.Id != null)
                {
                    var result = await _userContext.Addresses.FirstOrDefaultAsync(x => x.Id == entity.AddressId);
                    
                    if(result != null)
                    {
                        return result;
                    }
                }
                return null!;
            }
            catch (Exception ex)
            { Debug.WriteLine("GetAddress" + ex.Message);
                return null!;
                 }
        }

        public async Task<bool> UpdateAddressAsync(UserEntity entity, AddressEntity addressEntity)
        {
            try
            {
                if (entity != null && addressEntity != null)
                {
                    var addressToUpdate = await _userContext.Addresses.FirstOrDefaultAsync(x => x.Id == entity.AddressId);

                    if (addressToUpdate != null)
                    {
                        addressToUpdate.StreetName = addressEntity.StreetName;
                        addressToUpdate.City = addressEntity.City;
                        addressToUpdate.StreetName2 = addressEntity.StreetName2;
                        addressToUpdate.PostalCode = addressEntity.PostalCode;

                        await _userContext.SaveChangesAsync();

                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetAddress" + ex.Message);
                return false;
            }
        }
    }
}
