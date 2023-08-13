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
