using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Siren.Contracts.Models.Chat;
using Siren.Contracts.Models.Profile;

namespace Siren.Contracts.Models.Identity
{
    public class User : IdentityUser
    {
        public int? TrackId { get; set; }
        public Track Track { get; set; }
        public bool IsPositionEnabled { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public List<ConversationUser> Conversations { get; set; }
    }
}
