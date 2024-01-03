using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using QlhsServer.Data;
using QlhsServer.Helpers;
using QlhsServer.Models;
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

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;

        }

        public async Task<object> SignInAsync(SignInModel model)
        {
                var user = await userManager.FindByEmailAsync(model.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

                if (user == null || !passwordValid)
                {
                    return new SignInResponseFailModel
                    {
                        ErrorMessage = "Email or password is incorrect",
                    };
                }

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
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


                return new SignInResponseSuccessModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "Sign in successfully"
                };
            }

        public async Task<object> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };


            var result = await userManager.CreateAsync(user, model.Password);

            System.Console.WriteLine(result.Succeeded);
            if (result.Succeeded)
            {
                //kiểm tra role Customer đã có
                if (!await roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }

                await userManager.AddToRoleAsync(user, AppRole.Customer);
                return new SignUpResponseSuccessModel
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Sign up successfully"
                };
            }
            else
            {
                return new SignUpResponseFailModel
                {
                    Status = StatusCodes.Status400BadRequest,
                    ErrorMessage = result.Errors.First().Description
                };
            }

        }
    }
}
