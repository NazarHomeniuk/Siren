using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DataContext dataContext;

        public TrackRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Track> GetAll()
        {
            return dataContext.Tracks;
        }

        public async Task<Track> Get(int id)
        {
            return await dataContext.Tracks.FindAsync(id);
        }

        public async Task<Track> Create(Track track)
        {
            var result = await dataContext.Tracks.AddAsync(track);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Track> Update(Track track)
        {
            var result = dataContext.Tracks.Update(track);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var track = await dataContext.Tracks.FindAsync(id);
            dataContext.Tracks.Remove(track);
            await dataContext.SaveChangesAsync();
        }
    }
}
