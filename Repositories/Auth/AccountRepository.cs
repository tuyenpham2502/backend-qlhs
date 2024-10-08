using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using QlhsServer.Data;
using QlhsServer.Helpers;
using QlhsServer.Models;
using QlhsServer.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QlhsServer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDistributedCache _distributedCache;
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IDistributedCache distributedCache)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            _distributedCache = distributedCache;
        }

        public async Task<RequestResponse> SignInAsync(SignInModel model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

                if (user == null || !passwordValid)
                {
                    return AppErrors.LoginEmailOrPasswordIncorrect;
                }

                string token = await GenerateJwtToken(user);


                return new SuccessModel
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Sign in successfully",
                    Data = new
                    {
                        token
                    }
                };
            }
            catch (Exception ex)
            {
                return new ErrorModel
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = ex.Source,
                            Message = ex.Message
                        }
                    }
                };
            }
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(60),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token).ToString();
            await _distributedCache.SetStringAsync(user.Id.ToString(), tokenString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });

            return tokenString;
        }

        public async Task<RequestResponse> SignUpAsync(SignUpModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };


                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //kiểm tra role Customer đã có
                    if (!await roleManager.RoleExistsAsync(AppRole.SuperAdmin))
                    {
                        await roleManager.CreateAsync(new IdentityRole(AppRole.SuperAdmin));
                    }

                    await userManager.AddToRoleAsync(user, AppRole.SuperAdmin);
                    return new SuccessModel
                    {
                        Status = StatusCodes.Status200OK,
                        Message = "Sign up successfully",
                        Data = new
                        {
                            user.Id
                        }
                    };
                }
                return AppErrors.EmailIsAlreadyTakenError;
            }
            catch (Exception ex)
            {
                return new ErrorModel
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = ex.Source,
                            Message = ex.Message
                        }
                    }
                };
            }
        }

        public async Task<RequestResponse> LogoutAsync()
        {
            try
            {
                await signInManager.SignOutAsync();
                return new SuccessModel
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Logout successfully"
                };
            }
            catch (Exception ex)
            {
                return new ErrorModel
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Errors = new List<ErrorModel.ErrorItem>
                    {
                        new ErrorModel.ErrorItem
                        {
                            FieldName = ex.Source,
                            Message = ex.Message
                        }
                    }
                };
            }
        }
    }
}
