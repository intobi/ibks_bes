using Microsoft.AspNetCore.Identity;

namespace IBKS.DataAccess.Entities.Identity;

public class User : IdentityUser<Guid>
{
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
}
