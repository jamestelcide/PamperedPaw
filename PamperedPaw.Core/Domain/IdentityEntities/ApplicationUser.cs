using Microsoft.AspNetCore.Identity;

namespace PamperedPaw.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
