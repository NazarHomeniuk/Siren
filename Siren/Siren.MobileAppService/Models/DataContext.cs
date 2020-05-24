using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Siren.Contracts.Models.Chat;
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
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationUser> ConversationUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ConversationUser>().HasKey(cu => new { cu.UserId, cu.ConversationId });
        }
    }
}
