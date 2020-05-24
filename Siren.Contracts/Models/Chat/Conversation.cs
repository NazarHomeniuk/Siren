using System.Collections.Generic;

namespace Siren.Contracts.Models.Chat
{
    public class Conversation
    {
        public int Id { get; set; }
        public int HashCode { get; set; }
        public List<ConversationUser> Participants { get; set; }
        public List<Message> Messages { get; set; }
    }
}
