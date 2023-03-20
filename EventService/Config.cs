using IdentityServer4;
using IdentityServer4.Models;

namespace Features;

/// <summary>
/// Конфиг клиентов для IS4
/// </summary>
public class Config
{
    /// <summary>
    /// Получение клиента
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Client> GetClients()
    {
        //return new List<Client>
        //{
        //    new Client
        //    {
        //        ClientId = "client",
        //        AllowedGrantTypes = GrantTypes.ClientCredentials,
        //        ClientSecrets =
        //        {
        //            new Secret("secret".Sha256())
        //        },
        //        AllowedScopes = {"api1"}
        //    }
        //};

        return new List<Client>
        {
            new Client
            {
                ClientId = "spaWeb",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("hardtoguess".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "myapi.access"
                },
                AllowOfflineAccess = true, // this to allow SPA,
                AlwaysSendClientClaims = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                // this will generate reference tokens instead of access tokens
                AccessTokenType = AccessTokenType.Reference
            }
        };
    }

    /// <summary>
    /// Список API Resourse
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<ApiResource> GetApiResources()
    {
        //return new List<ApiResource>
        //{
        //    new ApiResource("api1", "My Api")
        //};
        return new List<ApiResource>
        {
            new ApiResource("myapi", "My API")
            {
                Scopes = new List<string>()
                {
                    "myapi.access"
                },
                ApiSecrets = { new Secret("hardtoguess".Sha256()) }
            }
        };
    }

    /// <summary>
    /// Получение ApiScope
    /// </summary>
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
            new ApiScope(name: "myapi.access",   displayName: "Access API Backend")
        };
    }

    /// <summary>
    /// Получение Identity
    /// </summary>
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }

}