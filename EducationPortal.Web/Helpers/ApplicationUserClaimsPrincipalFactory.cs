using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using EducationPortal.Data.Entities;

namespace EducationPortal.Web.Helpers;

public class ApplicationUserClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole<Guid>>
{
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        if (!string.IsNullOrEmpty(user.FirstName))
            identity.AddClaim(new Claim("FirstName", user.FirstName));

        if (!string.IsNullOrEmpty(user.LastName))
            identity.AddClaim(new Claim("LastName", user.LastName));

        if (!string.IsNullOrEmpty(user.Theme))
            identity.AddClaim(new Claim("Theme", user.Theme));

        return identity;
    }
}
