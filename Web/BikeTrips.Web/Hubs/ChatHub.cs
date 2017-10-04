using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BikeTrips.Services.Data.Contracts;

namespace BikeTrips.Web.Hubs
{
    public class ChatHub : Hub
    {
        private IChatService chat;

        public ChatHub()
        {
        }

        public ChatHub(IChatService chat)
        {
            this.chat = chat;
        }

        public void Join(string urlId)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new Exception("Please, log in in order to comment");
            }

            Groups.Add(Context.ConnectionId, urlId);
        }

        public void Send(string content, string urlId)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new Exception("Please, log in in order to comment");
            }
            var name = HttpContext.Current.User.Identity.Name;

            //this.chat.AddComment(content, urlId);

            Clients.Group(urlId).AddNewMessageToPage(name, content);
        }
    }
}