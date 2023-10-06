using Microsoft.AspNetCore.SignalR;

namespace YZCollab.Srv.Hubs
{
    public class MessageHub : Hub
    {

        private string? GetUserName() => $"{Context.GetHttpContext()?.Request?.Query["user"]} ({Context.ConnectionId})";

        public override Task OnConnectedAsync()
        {
            return Clients.All.SendAsync("RegisterUser", $"<i>{GetUserName()}</i> <strong>entrou.</strong>");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return Clients.All.SendAsync("RegisterUser", $"<i>{GetUserName()}</i> <strong>saiu.</strong>.");
        }

        public Task RegisterLog(string message)
        {
            return Clients.All.SendAsync("RegisterLog", $"{message}");
        }
    }
}
