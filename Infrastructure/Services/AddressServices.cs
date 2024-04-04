using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Factories;
using System.Diagnostics;
using Infrastructure.ViewModels;

namespace Infrastructure.Services
{
    public class AddressServices
    {
        private readonly AddressRepository _repository;

        public AddressServices(AddressRepository repository)
        {
            _repository = repository;
        }

        //Create
        public async Task<RepositoriesResult> CreateAddress(AccountDetailsAddressModel address)
        {
            try
            {
                if(address != null)
                {
                    var addressToCreate = new AddressEntity
                    {
                        StreetName = address.Address,
                        StreetName2 = address.Address2,
                        PostalCode = address.PostalCode,
                        City = address.City,
                    };

                    var exists = await _repository.AlreadyExistAsync(x => x.StreetName == address.Address && x.PostalCode == address.PostalCode && x.City == address.City);

                    if(exists == null)
                    {
                        var result = await _repository.CreateOneAsync(addressToCreate);

                        if (result.StatusCode == StatusCodes.OK)
                        {
                            return ResponseFactory.Ok(result);
                        }
                    }

                    else if(exists != null)
                    {
                        return ResponseFactory.AlreadyExist();
                    }

                }
                return ResponseFactory.Error();
            }
            catch (Exception ex)
            { Debug.WriteLine("CreateUser" + ex.Message);
                return ResponseFactory.Error();
            }
        }

        public async Task<bool> UpdateAddresses(AccountAddressDetailsViewModel model, UserEntity user)
        {
            try
            {
                if(model != null && user != null)
                {
                    var addressEntity = new AddressEntity
                    {
                        StreetName = model.AddressInfo!.Address,
                        StreetName2 = model.AddressInfo.Address2,
                        PostalCode = model.AddressInfo.PostalCode,
                        City = model.AddressInfo.City,
                    };

                    var result = await _repository.UpdateAddressAsync(user, addressEntity);
                    
                    if(result == true)
                    {
                        return true;
                    }
                }


                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateAddresses" + ex.Message);
                return false;
            }
        }

        public async Task<AddressEntity> GetOneAddresses(UserEntity entity)
        {
            try
            {
                var result = await _repository.GetOneAddressAsync(entity);


                if (result != null)
                {
                    return result;
                }

                return null!;
            }
            catch (Exception ex) { Debug.WriteLine("CreateUser" + ex.Message);
            return null!;
            }
        }
    }
}
