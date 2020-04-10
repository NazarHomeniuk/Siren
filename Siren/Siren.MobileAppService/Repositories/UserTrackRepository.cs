using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class UserTrackRepository : IUserTrackRepository
    {
        private readonly DataContext dataContext;

        public UserTrackRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<UserTrack> GetAll()
        {
            return dataContext.UserTracks.Include(t => t.Track);
        }

        public async Task<UserTrack> Get(int id)
        {
            return await dataContext.UserTracks.FindAsync(id);
        }

        public async Task<UserTrack> Create(UserTrack userTrack)
        {
            var result = await dataContext.UserTracks.AddAsync(userTrack);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserTrack> Update(UserTrack userTrack)
        {
            var result = dataContext.UserTracks.Update(userTrack);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var userTrack = await dataContext.UserTracks.FindAsync(id);
            dataContext.UserTracks.Remove(userTrack);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteAll(IEnumerable<UserTrack> userTracks)
        {
            dataContext.UserTracks.RemoveRange(userTracks);
            await dataContext.SaveChangesAsync();
        }
    }
}