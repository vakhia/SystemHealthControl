using IdentityServer4.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources =>
        new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<Client> GetClients =>
        new[]
        {
            new Client
            {
                ClientId = "killerwhale-client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret-killerwhale".Sha512()) },
                AllowedScopes = { "EntityFrameworkAPI.write", "EntityFrameworkAPI.read" }
            },
            new Client
            {
                ClientId = "machine-to-machine-client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret-killerwhale".Sha512()) },
                AllowedScopes = { "EntityFrameworkAPI.write", "EntityFrameworkAPI.read" }
            },
        };
    
    public static IEnumerable<ApiScope> GetApiScopes =>
        new[] { new ApiScope("EntityFrameworkAPI.read"), new ApiScope("EntityFrameworkAPI.write"), };

    public static IEnumerable<ApiResource> GetApiResources =>
        new[]
        {
            new ApiResource("EntityFrameworkAPI")
            {
                Scopes = new List<string> { "EntityFrameworkAPI.read", "EntityFrameworkAPI.write" },
                ApiSecrets = new List<Secret> { new Secret("secret-killerwhale".Sha512()) },
                UserClaims = new List<string> { "role" }
            }
        };
}

