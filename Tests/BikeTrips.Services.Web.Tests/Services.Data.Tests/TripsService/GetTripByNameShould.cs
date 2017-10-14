namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.TripsService
{
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;

    [TestFixture]
    public class GetTripByNameShould
    {
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        [Test]
        public void ReturnCorrectTrip()
        {
            //Arrange
            var service = new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                );

            var tripMock = new Mock<Trip>();
            var expected = tripMock.Object;
            expected.TripName = "name";
            
            this.tripsRepositoryMock.Setup(r => r.All())
                .Returns((new List<Trip>() { tripMock.Object, expected }).AsQueryable);

            //Act
            var result = service.GetTripByName("name");
            
            Assert.AreSame(tripMock.Object, result);
        }

        [Test]
        public void ReturnNullIfTripDoesntExist()
        {
            //Arrange
            var service = new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                );

            var tripMock = new Mock<Trip>();
            var expected = tripMock.Object;
            expected.TripName = "name";

            this.tripsRepositoryMock.Setup(r => r.All())
                .Returns((new List<Trip>() { expected }).AsQueryable);

            //Act
            var result = service.GetTripByName("something");

            Assert.Null(result);
        }

        [Test]
        public void CallTripRepositoryMethodAll()
        {
            var tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
            //Arrange
            var service = new TripsService(
                    tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                );

            var tripMock = new Mock<Trip>();
            var expected = tripMock.Object;
            expected.TripName = "name";

            tripsRepositoryMock.Setup(r => r.All())
                .Returns((new List<Trip>() { tripMock.Object }).AsQueryable);
            
            //Act
            var result = service.GetTripByName("name");

            tripsRepositoryMock.Verify(r => r.All(),
                Times.Once());
        }

    }
}
