using System.Collections.Generic;
using System.Threading.Tasks;
using Siren.Contracts.Models.Identity;

namespace Siren.MobileAppService.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        Task<User> GetWithAdditionalInfo(string id);
    }
}