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

        public void Send(string message/*, string room*/)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new Exception("Please, log in in order to comment");
            }
            var name = HttpContext.Current.User.Identity.Name;
            //this.chat.Add(message, "");
            Clients.All.AddNewMessageToPage(name, message);
        }
    }
}