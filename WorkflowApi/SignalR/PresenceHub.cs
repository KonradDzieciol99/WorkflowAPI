using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using WorkflowApi.Data;
using WorkflowApi.Models;

namespace WorkflowApi.SignalR
{
    [Authorize]
    public class PresenceHub :Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDistributedCache _cache;

        public PresenceHub(ApplicationDbContext dbContext, IDistributedCache cache)
        {
            this._dbContext = dbContext;
            this._cache = cache;
        }

        public override async Task OnConnectedAsync()
        {
            //Context.UserIdentifier
            //var SignalRConnection = await _dbContext.SignalRConnections.FindAsync(Context.User.FindFirst(ClaimTypes.Name)?.Value);
            //if (SignalRConnection!=null)
            //{
            //    return;
            //}//temporary
            //var httpContext = Context.GetHttpContext();
            //var otherUser = httpContext.Request.Query["user"].ToString();

            //var connection = new SignalRConnection()
            //{
            //    UserId= int.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
            //    UserName= Context.User.FindFirst(ClaimTypes.Name)?.Value,
            //    SignalRConnectionId=Context.ConnectionId
            //};

            //await _dbContext.SignalRConnections.AddAsync(connection);
            //try
            //{
            //    await _dbContext.SaveChangesAsync();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw e;
            //}
            _cache.
            await Clients.Others.SendAsync("UserIsOnline", Context.User.FindFirst(ClaimTypes.Name)?.Value);
            
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //var SignalRConnection = await _dbContext.SignalRConnections.FindAsync(Context.User.FindFirst(ClaimTypes.Name)?.Value);
            //if (SignalRConnection == null) { return; }

            //try
            //{
            //    _dbContext.SignalRConnections.Remove(SignalRConnection);
            //    await _dbContext.SaveChangesAsync();
            //}
            //catch (Exception E)
            //{
            //    Console.WriteLine(E);
            //    throw E;
            //}


            await Clients.Others.SendAsync("UserIsOffline", Context.User.FindFirst(ClaimTypes.Name)?.Value);
            //Groups.
            await base.OnDisconnectedAsync(exception);
        }

    }
}
