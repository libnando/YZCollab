using Microsoft.AspNetCore.SignalR;
using YZCollab.Srv.Hubs;

namespace YZCollab.Srv.Services
{
    public class MessageHubService : IMessageHubService
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessageHubService(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task RegisterLogAsync(string message) => 
            await _hubContext.Clients.All.SendAsync("RegisterLog", message);        
    }
}