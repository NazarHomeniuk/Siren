using Microsoft.AspNetCore.Identity;
using Siren.Contracts.Models.Profile;

namespace Siren.Contracts.Models.Identity
{
    public class User : IdentityUser
    {
        public int? TrackId { get; set; }
        public Track Track { get; set; }
    }
}
