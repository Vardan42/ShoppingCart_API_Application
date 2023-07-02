using Microsoft.IdentityModel.Tokens;
using ShoppingCart_API_Application.Data;
using ShoppingCart_API_Application.Models;
using ShoppingCart_API_Application.Models.DTO.LoginRequest;
using ShoppingCart_API_Application.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingCart_API_Application.Repository.Classes
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;

        public UserRepository(ApplicationDbContext db,IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user=_db.LocalUsers.FirstOrDefault(s=>s.UserName== username);
            if (user==null)
            {
             return true;
            }
            return false;
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {

            var user=_db.LocalUsers.FirstOrDefault(
             s=>s.UserName==loginRequestDTO.UserName.ToLower() 
             && s.Password==loginRequestDTO.Password
            );

            if (user==null)
            {
                return new LoginResponseDTO()
                {
                    Token="",
                    User=null
                };
            }

            //Եթե օգտատերը գությություն ունի իրականացնում ենք JWT տոկեն
            var tokenHandler=new JwtSecurityTokenHandler();
            var key=Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            LoginResponseDTO loginResponseDTO = new LoginResponseDTO
            {
                Token=tokenHandler.WriteToken(token),
                User=user,
            };

            return loginResponseDTO;
        }
        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            LocalUser user = new LocalUser
            {
                UserName=registrationRequestDTO.UserName,
                Password=registrationRequestDTO.Password,
                Name=registrationRequestDTO.Name,
                Role=registrationRequestDTO.Role,
            };

           await _db.LocalUsers.AddAsync(user);
           await _db.SaveChangesAsync();
           user.Password = "";
           return user;
        }
    }
}
