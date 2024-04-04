using Infrastructure.Repositories;
using Infrastructure.Models;
using System.Diagnostics;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Helpers;
using Infrastructure.ViewModels;

namespace Infrastructure.Services
{
    //public class UserServices
    //{
    //    private readonly UserRepository _repository;
    //    private readonly AddressRepository _addressRepository;

    //    public UserServices(UserRepository repository, AddressRepository addressRepository)
    //    {
    //        _repository = repository;
    //        _addressRepository = addressRepository;
    //    }

        //CRUD

        //Create
    //    public async Task<RepositoriesResult> CreateUser(SignUpModel user)
    //    {
    //        try
    //        {
    //            var (password, securityKey) = PasswordHasher.GenerateSecurePassword(user.Password);

    //            if (user != null)
    //            {
    //                var userToSignUp = new UserEntity
    //                {
    //                    Id = Guid.NewGuid().ToString(),
    //                    FirstName = user.FirstName,
    //                    LastName = user.LastName,
    //                    Email = user.Email,
    //                    PasswordHash = password,
    //                    Created = DateTime.Now,
    //                    Updated = null!,
    //                };

    //                var existUser = await _repository.AlreadyExistAsync(x => x.Email == user.Email);

    //                if (existUser.StatusCode == StatusCodes.NOT_FOUND)
    //                {
    //                    var result = await _repository.CreateOneAsync(userToSignUp);
    //                    return ResponseFactory.Ok(result);
    //                }

    //                else
    //                {
    //                    return ResponseFactory.AlreadyExist();
    //                }
    //            }

    //            return ResponseFactory.Error();
    //        }

    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine("CreateUser" + ex.Message);
    //            return ResponseFactory.Error();
    //        }
    //    }

    //    public async Task<UserModel> SignInUserAsync(SignInModel user)
    //    {
    //        try
    //        {
    //            if (user != null)
    //            {
    //                var result = await _repository.GetOneUserAsync(user);

    //                if (result.StatusCode == StatusCodes.OK && result != null!)
    //                {
    //                    UserEntity userEntity = (UserEntity)result.ContentResult!;

    //                        return new UserModel
    //                        {
    //                            FirstName = userEntity.FirstName,
    //                            LastName = userEntity.LastName,
    //                            Email = userEntity.Email,
    //                            Biography = userEntity.Biography ?? string.Empty,
    //                            Phone = userEntity.PhoneNumber ?? string.Empty,
    //                        };
    //                }
    //            }
    //            return null!;
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine("SignInUserAsync" + ex.Message);
    //            return null!;

    //        }
    //    }

    //    public async Task<RepositoriesResult> UpdateUserInfo(AccountDetailsModel model)
    //    {
    //        try
    //        {
    //            if (model != null)
    //            {
    //                var dataToUpdate = new UserEntity
    //                {
    //                    FirstName = model.FirstName,
    //                    LastName = model.LastName,
    //                    Email = model.Email,
    //                    PhoneNumber = model.Phone,
    //                    Biography = model.Biography,
    //                };

    //                var result = await _repository.UpdateOneAsync(x => x.Email == model.Email, dataToUpdate);

    //                if (result.StatusCode == StatusCodes.OK) 
    //                {
    //                    return ResponseFactory.Ok(result);
    //                }
    //                else
    //                {
    //                    return ResponseFactory.Error();
    //                }
    //            }

    //            return ResponseFactory.Error();
    //        }
    //        catch (Exception ex){ Debug.WriteLine("UpdateUserInfo" + ex.Message);
    //        return ResponseFactory.Error(); ;
    //        }
    //    }

    //    public async Task<RepositoriesResult> CreateAddress(AccountAddressDetailsViewModel model)
    //    {
    //        try
    //        {
    //            if (model != null)
    //            {
    //                var address = new AddressEntity
    //                {
    //                    StreetName = model.AddressInfo.Address,
    //                    StreetName2 = model.AddressInfo.Address2,
    //                    PostalCode = model.AddressInfo.PostalCode,
    //                    City = model.AddressInfo.City,
    //                };

    //                var result = _addressRepository.CreateOneAsync(address);

    //                if (result.Result.StatusCode == StatusCodes.OK)
    //                {
    //                    return ResponseFactory.Ok(address);
    //                }
    //            }

    //            return ResponseFactory.Error();
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine("CreateAddres" + ex.Message);
    //            return ResponseFactory.Error(); ;
    //        }
    //    }
    //}
}
