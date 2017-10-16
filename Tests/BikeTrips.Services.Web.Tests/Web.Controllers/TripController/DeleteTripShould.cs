namespace BikeTrips.Services.Web.Tests.Web.Controllers.TripController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using BikeTrips.Data.Models;
    using BikeTrips.Web.Controllers;
    using BikeTrips.Web.ViewModels.UserModels;
    using Contracts;
    using Data.Contracts;

    [TestFixture]
    public class DeleteTripShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ITripsService> tripsServiceMock = new Mock<ITripsService>();
        private readonly Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        [Test]
        public void CallTripsServiceMethodGetTripByIdOnce()
        {
            //Arrange
            var tripsServiceMock = new Mock<ITripsService>();
            var controller = new TripController(
                this.usersServiceMock.Object,
                tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act
            controller.DeleteTrip(It.IsAny<int>());

            //Assert
            tripsServiceMock.Verify(s => s.GetTripById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallTripsServiceMethodDeleteTripOnce()
        {
            //Arrange
            var tripsServiceMock = new Mock<ITripsService>();
            var controller = new TripController(
                this.usersServiceMock.Object,
                tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act
            controller.DeleteTrip(It.IsAny<int>());

            //Assert
            tripsServiceMock.Verify(s => s.DeleteTrip(It.IsAny<Trip>()), Times.Once);
        }
    }
}
