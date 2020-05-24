using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Siren.Contracts.Models.Chat;
using Siren.Contracts.Services;

namespace Siren.Services
{
    public class ChatService : IChatService
    {
        private readonly IHttpService httpService;

        public ChatService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<Conversation> GetConversation(int id)
        {
            var result = await httpService.GetAsync($"Chat/GetConversation?id={id}", App.Token);
            var conversation = JsonConvert.DeserializeObject<Conversation>(await result.Content.ReadAsStringAsync());
            return conversation;
        }

        public async Task<IEnumerable<Conversation>> GetConversations()
        {
            var result = await httpService.GetAsync("Chat/GetConversations", App.Token);
            var conversations =
                JsonConvert.DeserializeObject<IEnumerable<Conversation>>(await result.Content.ReadAsStringAsync());
            return conversations;
        }

        public async Task<Conversation> StartConversation(string userId)
        {
            var result = await httpService.PostAsync($"Chat/StartConversation?userId={userId}",
                new StringContent(userId), App.Token);
            var conversation = JsonConvert.DeserializeObject<Conversation>(await result.Content.ReadAsStringAsync());
            return conversation;
        }

        public async Task SendMessage(Message message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var stringContent = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            var response = await httpService.PostAsync("Chat/SendMassage", stringContent, App.Token);
            var result = JsonConvert.DeserializeObject<Message>(await response.Content.ReadAsStringAsync());
        }
    }
}
