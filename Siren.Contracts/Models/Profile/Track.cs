namespace Siren.Contracts.Models.Profile
{
    public class Track
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public byte[] Data { get; set; }
    }
}
