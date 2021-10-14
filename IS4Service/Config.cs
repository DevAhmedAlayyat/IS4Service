using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS4Service
{
    public static class Config
    {
        // For Resource Token
        //public static IEnumerable<IdentityResource> GetIdentityResources() =>
        //    new List<IdentityResource>
        //    {
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //        new IdentityResources.Email()
        //    };
        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "SpaReact",
                    ClientName = "Single Page Application build with ReactJs",
                    ClientSecrets = {new Secret("secret.SpaReact".Sha256())},
                    AllowedScopes = new List<string>
                    {
                        "WNCR.API",
                        // For Resource Token
                        //IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServerConstants.StandardScopes.Profile,
                        //IdentityServerConstants.StandardScopes.Email,
                        //"gateway"
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    //AccessTokenType = AccessTokenType.Reference
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope("WNCR.API", "Ministry of Water Resources and Irrigation API")
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("WNCR.API", "Ministry of Water Resources and Irrigation API")
                {
                    ApiSecrets =
                    {
                        new Secret("secret.API".Sha256())
                    }
                }
            };
    }
}
