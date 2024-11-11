using Microsoft.AspNetCore.Identity;

namespace IBKS.DataAccess.Entities.Identity;

public class Role : IdentityRole<Guid>
{
    public int AccessLevel { get; set; }
}
