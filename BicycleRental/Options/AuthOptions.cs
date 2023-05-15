using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BicycleRental.Options
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer";
        public const string AUDIENCE = "MyAuthClient";
        const string KEY = "this is my custom Secret key for authentication";
        public const int LIFETIME = 15;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
