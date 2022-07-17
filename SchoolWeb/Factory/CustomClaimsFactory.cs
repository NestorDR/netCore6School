using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SchoolWeb.Models;

namespace SchoolWeb.Factory
{
    public class CustomClaimsFactory : UserClaimsPrincipalFactory<User>
    {
        // Custom class has to implement the UserClaimsPrincipalFactory<TUser> class and to send a userManager and optionsAccessor objects to it.
        // Now, you have to override the GenerateClaimsAsync method in this class to add custom claims:
        public CustomClaimsFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            // A Claim is a piece of information about the user. It is consists of a Claim type and an optional value.
            // Visit: https://www.tektutorialshub.com/asp-net-core/authentication-in-asp-net-core/#claims-claimsidentity-claimsprincipal
            //        https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-6.0
            var identity = await base.GenerateClaimsAsync(user);

            // Update the "Name" claim type with a more friendly value.
            identity.RemoveClaim(identity.FindFirst(ClaimTypes.Name));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FullName));

            // Add custom claims.
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

            var roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}
