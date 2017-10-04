namespace BikeTrips.Services.Data.Contracts
{
    public interface IChatService
    {
        void AddComment(string content, string tripUrl);
    }
}
