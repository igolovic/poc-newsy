using IdentityModel;
using IdentityServer4.Test;
using System.Security.Claims;

namespace identityserver.IdentityConfiguration
{
    public class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "e95fa28b-1ed1-4a1b-a981-a4e608ca7cda",
                    Username = "jd",
                    Password = "a",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "support@newsy.com"),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.WebSite, "https://news.com"),
                        new Claim(JwtClaimTypes.Name, "John Doe"),
                    }
                },
                new TestUser
                {
                    SubjectId = "e95fa28b-1ed1-4a1b-a981-a4e608ca7cd9",
                    Username = "db",
                    Password = "a",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "db@newsy.com"),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.Name, "Danny Boy")
                    }
                },
            };
        }
    }
}