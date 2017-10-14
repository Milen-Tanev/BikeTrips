namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.TripsService
{
    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;
    using Moq;
    using NUnit.Framework;
    using BikeTrips.Services.Data;

    [TestFixture]
    public class ConstructorShould
    {
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        [Test]
        public void CreateInstanceOfTripsServiceWithoutParameters()
        {
            // Arrange & Act
            var service = new TripsService();

            // Assert
            Assert.IsInstanceOf<ICommentsService>(service);
        }

        [Test]
        public void CreateInstanceOfCommentsServiceWithtParameters()
        {
            // Act

        }

        [Test]
        public void ThrowIfCommentsRepositoryIsNull()
        {

        }
    }
}
