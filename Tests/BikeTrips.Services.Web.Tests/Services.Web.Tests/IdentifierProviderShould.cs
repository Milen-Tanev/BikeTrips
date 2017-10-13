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
            // Arrange
            var identifierProvider = new IdentifierProvider();

            // Act
            var url = identifierProvider.GetUrlId(id);
            var intId = identifierProvider.GetId(url);

            // Assert
            Assert.AreEqual(id, intId);
        }
    }
}
