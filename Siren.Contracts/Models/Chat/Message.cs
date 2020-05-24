using System;
using Siren.Contracts.Models.Identity;

namespace Siren.Contracts.Models.Chat
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public User SentBy { get; set; }
        public string SentById { get; set; }
        public bool IsReceived { get; set; }
        public Conversation Conversation { get; set; }
        public int ConversationId { get; set; }
    }
}
