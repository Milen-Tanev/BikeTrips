namespace BikeTrips.Services.Web.Tests
{
    using NUnit.Framework;

    using Web;

    [TestFixture]
    public class IdentifierProviderShould
    {
        [TestCase(1)]
        [TestCase(1234)]
        [TestCase(int.MaxValue / 2)]
        [TestCase(int.MaxValue)]
        public void ReturnTheSameIdAfterEncodingAndDecoding(int id)
        {
            var identifierProvider = new IdentifierProvider();

            var url = identifierProvider.GetUrlId(id);
            var intId = identifierProvider.GetId(url);

            Assert.AreEqual(id, intId);
        }
    }
}
