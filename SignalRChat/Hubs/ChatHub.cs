using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            SendMonitoringData("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public Task Send(string message)
        {
            return this.AllExceptMe().SendAsync("Send", $"{Context.ConnectionId}: {message}");
        }

        private Task SendMonitoringData(string eventType, string connectionId)
        {
            return this.AllExceptMe().SendAsync("Joined", $"{ Context.ConnectionId}: Has joined the conversation");
        }
    }

    public static class HubExtensions
    {
        public static IClientProxy AllExceptMe(this Hub hub)
        {
            IReadOnlyList<string> me = new List<string> { hub.Context.ConnectionId };
            return hub.Clients.AllExcept(me);
        }
    }
}
