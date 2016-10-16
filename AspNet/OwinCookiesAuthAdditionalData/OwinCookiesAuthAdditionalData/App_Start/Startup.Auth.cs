using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace OwinCookiesAuthAdditionalData
{
    public class CustomClaimTypes
    {
        public const string Token = "Token";
    }

    public partial class Startup
    {
        private static List<string> tokens = new List<string>();

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            //            app.CreatePerOwinContext(ApplicationDbContext.Create);
            //            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseExternalSignInCookie();
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = OnValidateIdentity
                }
            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "<ClientId>",
                ClientSecret = "<ClientSecret>"
            });
        }

        private async Task OnValidateIdentity(CookieValidateIdentityContext cookieValidateIdentityContext)
        {
            var enumerable = cookieValidateIdentityContext.Identity.Claims.Where(val => val.Type == CustomClaimTypes.Token).Select(val => val.Value).SingleOrDefault();
            if (!tokens.Contains(enumerable ?? string.Empty))
                cookieValidateIdentityContext.ReplaceIdentity(WindowsIdentity.GetAnonymous());

            await Task.FromResult(0);
        }

        public class CustomSignInManager
        {
            private readonly IAuthenticationManager authenticationManager;

            public CustomSignInManager(IAuthenticationManager authenticationManager)
            {
                this.authenticationManager = authenticationManager;
            }

            public async Task<SignInStatus> SignIn(string user, string password)
            {
                var guid = Guid.NewGuid().ToString();
                tokens.Add(guid);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user),
                    new Claim(ClaimTypes.Name, user),
                    new Claim(CustomClaimTypes.Token, guid)
                };

                var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                this.authenticationManager.SignIn(new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IssuedUtc = DateTimeOffset.Now,
                    ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(2))
                }, id);

                return await Task.FromResult(SignInStatus.Success);
            }
        }

        /*
        public class CustomAuthenticationSessionStore : IAuthenticationSessionStore
        {

            public Task<string> StoreAsync(AuthenticationTicket ticket)
            {
                var value = ticket.Identity.Claims.Where(val => val.Type == CustomClaimTypes.Token).Select(val => val.Value).SingleOrDefault();
                return Task.FromResult(value);
            }

            public Task RenewAsync(string key, AuthenticationTicket ticket)
            {
                var sessionKey = GetSessionKey(ticket);
                sessionKeyDict.Remove(sessionKey);
                return StoreAsync(ticket);
            }

            public Task<AuthenticationTicket> RetrieveAsync(string key)
            {
                return Task.FromResult(new AuthenticationTicket(new ClaimsIdentity(), new AuthenticationProperties()));
            }

            public Task RemoveAsync(string key)
            {
                return Task.FromResult(true);
            }

            private static string GetSessionKey(AuthenticationTicket ticket)
            {
                var sessionKey = ticket.Identity.Claims.Single().Value;
                return sessionKey;
            }
        }
        */
    }


}