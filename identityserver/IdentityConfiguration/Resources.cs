using IdentityServer4.Models;

namespace identityserver.IdentityConfiguration
{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "newsy-api",
                    DisplayName = "Newsy web API",
                    Description = "Allow the application to read and write",
                    Scopes = new List<string> { "newsy-api.read", "newsy-api.write"},
                    ApiSecrets = new List<Secret> {new Secret("verysecretive".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
        }
    }
}