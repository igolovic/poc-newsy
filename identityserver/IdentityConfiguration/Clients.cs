using IdentityServer4.Models;
using IdentityServer4;

namespace identityserver.IdentityConfiguration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "newsy-mobile-client",
                    ClientName = "Newsy web API client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("verysecretive".Sha256())},
                    AllowedScopes = new List<string> { "newsy-api.read", "newsy-api.write" }
                },
                new Client
                {
                    ClientId = "newsy-editor-viewer-application",
                    ClientName = "Newsy editor-viewer application client",
                    ClientSecrets = new List<Secret> {new Secret("verysecretive".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> {"https://localhost:44346/signin-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "newsy-api.read",
                        "newsy-api.write"
                    },

                    RequirePkce = true,
                    AllowPlainTextPkce = false
                }
            };
        }
    }
}
