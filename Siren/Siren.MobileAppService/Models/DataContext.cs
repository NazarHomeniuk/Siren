using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Models
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<UserTrack> UserTracks { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

    }
}
