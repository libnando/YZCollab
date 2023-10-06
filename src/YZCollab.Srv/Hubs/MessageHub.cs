using Microsoft.AspNetCore.SignalR;

namespace YZCollab.Srv.Hubs
{
    public class MessageHub : Hub
    {

        private string? GetUserName() => Context.GetHttpContext()?.Request?.Query["user"];

        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("RegisterUser", $"Usuário {GetUserName()} acabou de entrar.");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return Clients.All.SendAsync("RegisterUser", $"Usuário {GetUserName()} saiu.");
        }

        public Task RegisterLog(string message)
        {
            return Clients.All.SendAsync("RegisterLog", $"{message}");
        }
    }
}
