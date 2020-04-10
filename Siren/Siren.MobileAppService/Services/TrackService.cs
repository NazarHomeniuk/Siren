using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Siren.Contracts.Models.Identity;
using Siren.Contracts.Models.Profile;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository trackRepository;
        private readonly UserManager<User> userManager;

        public TrackService(ITrackRepository trackRepository, UserManager<User> userManager)
        {
            this.trackRepository = trackRepository;
            this.userManager = userManager;
        }

        public async Task<Track> Play(int id, User user)
        {
            var track = await Get(id);
            //if (track != null)
            //{
            //    user.TrackId = track.Id;
            //    await userManager.UpdateAsync(user);
            //}

            return track;
        }

        public async Task<Track> Get(int id)
        {
            return await trackRepository.Get(id);
        }

        public IEnumerable<Track> GetAll()
        {
            return trackRepository.GetAll();
        }

        public IEnumerable<int> GetAllIds()
        {
            return trackRepository.GetAll().Select(t => t.Id);
        }

        public async Task Upload(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            byte[] buffer;
            var file = TagLib.File.Create(path);
            using (var fileStream = file.FileAbstraction.ReadStream)
            using (var stream = new MemoryStream())
            {
                await fileStream.CopyToAsync(stream);
                buffer = stream.ToArray();
            }

            var track = new Track
            {
                Artist = file.Tag.FirstPerformer,
                Title = file.Tag.Title,
                Data = buffer
            };
            await trackRepository.Create(track);
        }
    }
}
