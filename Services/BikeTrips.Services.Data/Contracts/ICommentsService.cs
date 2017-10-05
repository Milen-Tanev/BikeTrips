namespace BikeTrips.Services.Data.Contracts
{
    public interface ICommentsService
    {
        void AddComment(string content, string tripUrl);
    }
}
