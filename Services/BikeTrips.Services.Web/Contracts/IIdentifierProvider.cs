namespace BikeTrips.Services.Web.Contracts
{
    public interface IIdentifierProvider
    {
        int GetId(string urlId);

        string GetUrlId(int id);
    }
}
