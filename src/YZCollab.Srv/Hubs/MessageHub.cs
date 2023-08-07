using Microsoft.AspNetCore.SignalR;

namespace YZCollab.Srv.Hubs
{
    public class MessageHub : Hub
    {

        public MessageHub() {            
        }

        private string? GetQueryParam(string key) => Context.GetHttpContext()?.Request?.Query[key];

        public override Task OnConnectedAsync()
        {
            var name = GetQueryParam("user");
            return Clients.All.SendAsync("RegisterUser", $"Usuário {name} acabou de entrar.");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var name = GetQueryParam("user");
            return Clients.All.SendAsync("RegisterUser", $"Usuário {name} saiu.");
        }

        public Task RegisterLog(string message)
        {
            return Clients.All.SendAsync("RegisterLog", $"{message}");
        }

        //public Task SendToConnection(string connectionId, string name, string message)
        //{
        //    return Clients.Client(connectionId).SendAsync("Send", $"Private message from {name}: {message}");
        //}

        //public Task SendToGroup(string groupName, string name, string message)
        //{
        //    return Clients.Group(groupName).SendAsync("Send", $"{name}@{groupName}: {message}");
        //}

        //public async Task JoinGroup(string groupName, string name)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //    await Clients.Group(groupName).SendAsync("Send", $"{name} joined {groupName}");
        //}

        //public async Task LeaveGroup(string groupName, string name)
        //{
        //    await Clients.Group(groupName).SendAsync("Send", $"{name} left {groupName}");

        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //}

        //public Task Echo(string name, string message)
        //{
        //    return Clients.Caller.SendAsync("Send", $"{name}: {message}");
        //}

    }
}
