using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using BikeTrips.Services.Data.Contracts;

namespace BikeTrips.Web.Hubs
{
    public class ChatHub : Hub
    {
        private IChatService comments;

        public ChatHub()
        {
        }

        public ChatHub(IChatService comments)
        {
            this.comments = comments;
        }

        public void Join(string urlId)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Groups.Add(Context.ConnectionId, urlId);
                }
            }
        }

        public void Send(string content, string urlId)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new Exception("Please, log in in order to comment");
            }
            var name = HttpContext.Current.User.Identity.Name;

            this.comments.AddComment(content, urlId);

            Clients.Group(urlId).AddNewMessageToPage(name, content);
        }
    }
}