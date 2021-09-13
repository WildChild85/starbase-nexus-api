using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Hubs
{
    public class ViewsHub : Hub
    {
        public static Dictionary<string, DateTimeOffset> _views = new Dictionary<string, DateTimeOffset>();

        public async Task NotifyWatching()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            List<string> toRemove = new List<string>();
            foreach (KeyValuePair<string, DateTimeOffset> view in _views)
            {
                var age = now.Subtract(view.Value);
                if (age.TotalMinutes > 5)
                {
                    toRemove.Add(view.Key);
                }
            }
            foreach(string key in toRemove)
            {
                _views.Remove(key);
            }

            //string ip = Context.Connection.RemoteIpAddress.ToString();
            string ip = Context.GetHttpContext().Connection.RemoteIpAddress.ToString();
            if (_views.Keys.Contains(ip))
            {
                _views[ip] = now;
            }
            else
            {
                _views.Add(ip, now);
            }

            await Clients.All.SendAsync("viewCountUpdate", _views.Count());
        }
    }
}
