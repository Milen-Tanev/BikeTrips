using BikeTrips.Services.Web;
using Common.Constants;
using NUnit.Framework;

namespace BikeTrips.Services.Web.Tests
{
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
