using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository trackRepository;

        public TrackService(ITrackRepository trackRepository)
        {
            this.trackRepository = trackRepository;
        }

        public async Task<Track> Get(int id)
        {
            return await trackRepository.Get(id);
        }

        public IEnumerable<int> GetAllIds()
        {
            return trackRepository.GetAll().Select(t => t.Id);
        }

        public async Task Upload(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            byte[] buffer;
            var file = new FileInfo(path);
            using (var fileStream = file.Open(FileMode.Open))
            using (var stream = new MemoryStream())
            {
                await fileStream.CopyToAsync(stream);
                buffer = stream.ToArray();
            }

            var track = new Track
            {
                Data = buffer
            };
            await trackRepository.Create(track);
        }
    }
}
