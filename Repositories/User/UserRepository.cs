using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QlhsServer.Data;
using QlhsServer.Models;
using System.Security.Claims;
using QlhsServer.Models.Response;
namespace QlhsServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;


        public UserRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        public async Task<RequestResponse> GetUserAsync(string userId)
        {
            try
            {

                var user = await userManager.FindByIdAsync(userId);
                var Roles = await userManager.GetRolesAsync(user);
                return new SuccessModel
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Get user successfully",
                    Data = new
                    {
                        user.Id,
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.PhoneNumber,
                        user.Avatar,
                        user.Address,
                        Roles
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

        public async Task<RequestResponse> UpdateUserAsync(UserModel model, string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new ErrorModel
                    {
                        Status = StatusCodes.Status404NotFound,
                        Errors = new List<ErrorModel.ErrorItem>
                        {
                            new ErrorModel.ErrorItem
                            {
                                FieldName = "User",
                                Message = "User not found"
                            }
                        }
                    };
                    
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Avatar = model.Avatar;
                user.Address = model.Address;
                var result = await userManager.UpdateAsync(user);
                return new SuccessModel
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Update user successfully",
                    Data = new
                    {
                        user.Id,
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
    }
}