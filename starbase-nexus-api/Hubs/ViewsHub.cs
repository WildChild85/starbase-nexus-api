using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Hubs
{
    public class ViewsHub : Hub
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();

        public async Task NotifyWatching()
        {
            await Clients.All.SendAsync("viewCountUpdate", ConnectedIds.Count());
        }
        public override Task OnConnectedAsync()
        {
            ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
