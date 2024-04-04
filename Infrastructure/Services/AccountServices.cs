using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class AccountServices
    {
        private readonly UserContext _userContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;

        public AccountServices(IConfiguration configuration, UserManager<UserEntity> userManager, UserContext userContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _userContext = userContext;
        }

        public async Task<bool> UploadUserProfileImageAsync(ClaimsPrincipal user, IFormFile file)
        {
            try
            {
                if(user != null && file != null && file.Length != 0)
                {
                    var userEntity = await _userManager.GetUserAsync(user);
                    
                    if(userEntity != null)
                    {
                        //Denna del kopierar och flyttar över bilden till servern!

                        //Denna del är för att sätta ett unikt id på bilden.
                        //Denna del hämtar först användarens ID, sedan sätter den ett unikt ID och till sist så hämtar den ut filändelsen... 
                        var fileName = $"p_{userEntity.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                        //Path.Combine måste ha redan befintliga kataloger! Ta sökvägen lägger på en "/" och sedan lägga på filepath och sedan filnamnet.
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!, fileName);

                        using var fs = new FileStream(filePath, FileMode.Create);

                        await file.CopyToAsync(fs);

                        userEntity.UrlImage = fileName;
                        _userContext.Update(userEntity);
                        await _userContext.SaveChangesAsync();

                        return true;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine("UploadUserProfileImageAsync" + ex.Message);
            }
            return false;
        }

    }
}
