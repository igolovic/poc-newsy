using IdentityServer4.Models;

namespace identityserver.IdentityConfiguration
{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
            new ApiScope("newsy-api.read", "Read access"),
            new ApiScope("newsy-api.write", "Write access"),
        };
        }
    }
}
