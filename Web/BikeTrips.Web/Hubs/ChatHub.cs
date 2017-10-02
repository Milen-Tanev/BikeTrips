using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BikeTrips.Web.Hubs
{
    public class ChatHub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}