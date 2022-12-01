using AspNetIdentity.Shared;
using DataAccessLayers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApplication1_API.Repository
{
    public class UserRepository : IUserInterface
    {
        private UserManager<IdentityUser> _userManger;
        public IConfiguration _configuration;
        private object isSuccess;

        public UserRepository(UserManager<IdentityUser> userManger , IConfiguration config)
        {
            _userManger = userManger;
            _configuration = config;
        }

       
        //Registration 
        public async Task <UserMangerResponse> RegisterUser(RegisterModel registerModel)
        {
            if(registerModel == null)
            {
                throw new ArgumentNullException("register Model is null");
            }
            if(registerModel.Password != registerModel.ConfirmPassword)
            {
                UserMangerResponse res = new UserMangerResponse();
                res.Message = "Password and confirm password Must Match";
                res.IsSuccess = false;
                return res;
            }
            var CheckUser = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email
            };
        var userRecord =await _userManger.CreateAsync(CheckUser,registerModel.Password);

            if(userRecord.Succeeded)
            {
                return new UserMangerResponse
                {
                    Message = "User Added Successfully",
                    IsSuccess = true
                };
            }
            return new UserMangerResponse
            {
                Message = "User Not added",
                IsSuccess = false,
                Errors = userRecord.Errors.Select(x => x.Description)
            };


            

        }
        //Login
        public async Task<UserMangerResponse> Login(loginModel loginModel)
        {
            var user = await _userManger.FindByEmailAsync(loginModel.Email);
            if(user ==null )
            {
                return new UserMangerResponse
                {
                    Message = "User not Found",
                    IsSuccess = false

                };
            }
            var record = await _userManger.CheckPasswordAsync(user,loginModel.Password);
            if(record==null)
            {
                return new UserMangerResponse
                {
                    Message = "Invalid Password",
                    IsSuccess = false
                };
            }
            var claims = new[] {
                        
                        new Claim("email", loginModel.Email),
                        new Claim(ClaimTypes.NameIdentifier,user.Id)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            var ValidToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserMangerResponse
            {
                Message = "login successfully",
                Token = ValidToken,
                IsSuccess = true,
                ExpireDate = token.ValidTo,

            };


        }

    }
     
}
