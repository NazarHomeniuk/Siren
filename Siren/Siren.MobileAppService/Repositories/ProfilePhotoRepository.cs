using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Models;

namespace Siren.MobileAppService.Repositories
{
    public class ProfilePhotoRepository : IProfilePhotoRepository
    {
        private readonly DataContext dataContext;

        public ProfilePhotoRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<ProfilePhoto> GetAll()
        {
            return dataContext.ProfilePhotos;
        }

        public async Task<ProfilePhoto> Get(int id)
        {
            return await dataContext.ProfilePhotos.FindAsync(id);
        }

        public async Task<ProfilePhoto> GetForUser(string userId)
        {
            return await dataContext.ProfilePhotos.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<ProfilePhoto> Create(ProfilePhoto profilePhoto)
        {
            var result = await dataContext.ProfilePhotos.AddAsync(profilePhoto);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ProfilePhoto> Update(ProfilePhoto profilePhoto)
        {
            var result = dataContext.ProfilePhotos.Update(profilePhoto);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var profilePhoto = await dataContext.ProfilePhotos.FindAsync(id);
            dataContext.ProfilePhotos.Remove(profilePhoto);
            await dataContext.SaveChangesAsync();
        }
    }
}
