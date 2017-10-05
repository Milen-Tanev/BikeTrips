namespace BikeTrips.Web.Hubs
{
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Web;

    using Common.Constants;
    using Services.Data.Contracts;
    using Utils;

    public class ChatHub : Hub
    {
        private ICommentsService comments;

        public ChatHub()
        {
        }

        public ChatHub(ICommentsService comments)
        {
            Guard.ThrowIfNull(comments, "Comments service");

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
                throw new Exception(ErrorMessageConstants.UserNotLoggedIn);
            }
            var name = HttpContext.Current.User.Identity.Name;

            this.comments.AddComment(content, urlId);

            Clients.Group(urlId).AddNewMessageToPage(name, content);
        }
    }
}