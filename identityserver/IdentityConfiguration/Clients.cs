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
                    ClientName = "Newsy editor-viewer application client",
                    ClientId = "newsy-editor-viewer-application",
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:8080",
                        "https://localhost:8080/callback.html",
                        "https://localhost:8080/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:8080/",
                        "https://localhost:8080"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:8080"
                    },
                    AllowedScopes = new List<string> { "newsy-api.read", "newsy-api.write" },

                    ClientSecrets = new List<Secret> {new Secret("verysecretive".Sha256())},
                }
            };
        }
    }
}
