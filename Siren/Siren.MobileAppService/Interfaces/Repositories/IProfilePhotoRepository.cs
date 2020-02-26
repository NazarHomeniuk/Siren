using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface IProfilePhotoRepository
    {
        IEnumerable<ProfilePhoto> GetAll();
        Task<ProfilePhoto> Get(int id);
        Task<ProfilePhoto> GetForUser(string userId);
        Task<ProfilePhoto> Create(ProfilePhoto profilePhoto);
        Task<ProfilePhoto> Update(ProfilePhoto profilePhoto);
        Task Delete(int id);
    }
}