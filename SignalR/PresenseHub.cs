using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalR
{
    [Authorize]
    public class PresenseHub : Hub
    {
        private readonly PresenceTracker _tracker;
        public PresenseHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var isOnline = await _tracker.UserConnected(Context.User.Getusername(), Context.ConnectionId);
            if(isOnline)
             await Clients.Others.SendAsync("UserIsOnline", Context.User.Getusername());

            var currentUsers = await _tracker.GetOnlineUsers();
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUsers);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var isOffline = await _tracker.UserDisconnected(Context.User.Getusername(), Context.ConnectionId);
            if(isOffline)
            await Clients.Others.SendAsync("UserIsOffline", Context.User.Getusername());

            
            await base.OnDisconnectedAsync(exception);
        }

    }
}