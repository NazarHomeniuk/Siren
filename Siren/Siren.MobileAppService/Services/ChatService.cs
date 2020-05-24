using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siren.Contracts.Models.Chat;
using Siren.Contracts.Models.Identity;
using Siren.MobileAppService.Extensions;
using Siren.MobileAppService.Interfaces.Repositories;
using Siren.MobileAppService.Interfaces.Services;

namespace Siren.MobileAppService.Services
{
    public class ChatService : IChatService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IConversationRepository conversationRepository;
        private readonly IConversationUserRepository conversationUserRepository;
        private readonly IUserRepository userRepository;

        public ChatService(IMessageRepository messageRepository, IConversationRepository conversationRepository,
            IConversationUserRepository conversationUserRepository, IUserRepository userRepository)
        {
            this.messageRepository = messageRepository;
            this.conversationRepository = conversationRepository;
            this.conversationUserRepository = conversationUserRepository;
            this.userRepository = userRepository;
        }

        public async Task<Message> AddMessage(Message message)
        {
            var result = await messageRepository.Create(message);
            return result;
        }

        public IEnumerable<Conversation> GetConversationsForUser(string userId)
        {
            var conversations = conversationRepository.GetAll();
            return conversations.Where(c => c.Participants.Exists(p => p.UserId == userId));
        }

        public async Task<Conversation> GetConversation(int id)
        {
            return await conversationRepository.Get(id);
        }

        public async Task<Conversation> StartConversation(List<string> userIds)
        {
            userIds.Sort();
            var conversionHashCode = string.Join(string.Empty, userIds).GetHashCodeSimple();
            var conversation = await conversationRepository.GetByHashCode(conversionHashCode);
            var users = userRepository.GetAll().Where(u => userIds.Contains(u.Id)).ToList();
            if (conversation != null) return conversation;
            conversation = new Conversation
            {
                HashCode = conversionHashCode,
                Messages = new List<Message>()
            };

            conversation = await conversationRepository.Create(conversation);
            var conversationUsers = new List<ConversationUser>(users.Select(u => new ConversationUser
            {
                ConversationId = conversation.Id, UserId = u.Id
            }));

            await conversationUserRepository.CreateRange(conversationUsers);
            return conversation;
        }
    }
}
