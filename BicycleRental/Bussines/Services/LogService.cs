using BicycleRental.Bussines.Services.Interfaces;
using BicycleRental.Bussines.Services.Models;
using BicycleRental.Data.Enitities;
using BicycleRental.Data.Repositories.Interfaces;
using BicycleRental.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BicycleRental.Bussines.Services
{
    public class LogService : ILogService
    {
        private readonly IUserRepository _userRepository;

        public LogService(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        public async Task<string> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new Exception($"Wrong mail or you need to sign up");

            var passwordIsCorrect = VerifyHashedPassword(user.PasswordHash, password);

            if (!passwordIsCorrect)
            {
                throw new Exception($"Wrong password or you need to sign up");
            }

            var accessToken = await GenerateToken(email);
            return accessToken;
        }

        public async Task SingnUpAsync(SingUpRequestModel model)
        {
            var isExist = await _userRepository.IsExistAsync(model.Email);

            if (isExist)
            {
                throw new Exception($"User with this email:{model.Email} exist");
            }
            var newGuid = Guid.NewGuid();
            var user = new User
            {
                Id = newGuid,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                FullName = model.FullName,              
                PasswordHash = ToHashPassword(model.Password)
            };
            
            await _userRepository.AddAsync(user);
                            
        }


        private async Task<string> GenerateToken(string email)
        {
            var identity = await GetIdentityAsync(email);

            if (identity == null)
            {
                throw new Exception("Invalid email or password.");
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new Exception("User not found");

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Id.ToString()),

                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }


        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }


        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        private string ToHashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
