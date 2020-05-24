using Siren.Contracts.Models.Identity;

namespace Siren.Contracts.Models.Chat
{
    public class ConversationUser
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
