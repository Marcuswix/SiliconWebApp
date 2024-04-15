using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration, UserContext userContext, SignInManager<UserEntity> signInManager, UserCourseRepository userCourseRepository) : ControllerBase
    {
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly UserContext _userContext = userContext;
        private readonly UserCourseRepository _userCourseRepository = userCourseRepository;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(SignUpModel model)
        {
            if (!await _userContext.Users.AnyAsync(x => x.Email == model.Email))
            {
                var userEntity = new UserEntity
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PasswordHash = model.Password,
                };

                _userContext.Users.Add(userEntity);
                await _userContext.SaveChangesAsync();
                return Created();
            }
            return Conflict();
        }

        [HttpPost]
        [Route("registerReact")]
        public async Task<IActionResult> RegisterReact([FromBody] SignUpRequest signUpRequest)
        {
            var userEntity = new UserEntity
            {
                Email = signUpRequest.Email,
                FirstName = signUpRequest.FirstName,
                LastName = signUpRequest.LastName,
                PasswordHash = signUpRequest.Password,
            };

            _userContext.Users.Add(userEntity);
            await _userContext.SaveChangesAsync();
            return Created();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(SignInModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userEntity = await _userContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                    if (userEntity != null)
                    {

                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                        {
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Sercret"]!);
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                    new(ClaimTypes.Email, model.Email),
                                    new(ClaimTypes.Name, model.Email),

                                }),
                                Expires = DateTime.UtcNow.AddMinutes(60),
                                Issuer = _configuration["Jwt:Issuer"],
                                Audience = _configuration["Jwt:Audience"],
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };

                            var token = tokenHandler.CreateToken(tokenDescriptor);
                            var tokenString = tokenHandler.WriteToken(token);

                            return Ok(new
                            {
                                Token = tokenString
                            });
                        }
                    }
                    return BadRequest();
                }
                return Unauthorized();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }

        [HttpPost]
        [Route("token")]
        public IActionResult Token(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                                    new(ClaimTypes.Email, model.Email),
                                    new(ClaimTypes.Name, model.Email),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    Token = tokenString
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("userCourse")]
        public async Task<IActionResult> UserCourse(int courseId, string userId)
        {
            var result = await _userCourseRepository.UserCourse(courseId, userId);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();

        }

        //[UseApiKey]
        //[HttpPost]
        //[Route("/tokenTwo")]
        //public IActionResult GetToken()
        //{
        //    try
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            //Vem är utfärdaren?
        //            Issuer = _configuration["Token:Issuer"],
        //            //Vem ska tSystem.ArgumentOutOfRangeException: 'IDX10653: The encryption algorithm 'http://www.w3.org/2001/04/xmldsig-more#hmac-sha512' requires a key size of at least '128' bits. Key '[PII of type 'Microsoft.IdentityModel.Tokens.SymmetricSecurityKey' is hidden. For more details, see https://aka.ms/IdentityModel/PII.]', is of size: '96'. Arg_ParamName_Name'a emot?
        //            Audience = _configuration["Token:Audience"],
        //            //Hur länge gäller denna token
        //            Expires = DateTime.Now.AddMinutes(15),
        //            //Den hemliga nyckeln...
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Secret"]!)), SecurityAlgorithms.HmacSha512Signature)
        //        };


        //        //Här skapas själva token...
        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        return Ok(tokenHandler.WriteToken(token));
        //    }
        //    catch (Exception ex) {
        //        Debug.WriteLine("" + ex.Message);
        //        return Unauthorized();
        //    }
        //}
    }
}
