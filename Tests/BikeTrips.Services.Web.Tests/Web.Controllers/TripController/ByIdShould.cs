namespace BikeTrips.Services.Web.Tests.Web.Controllers.TripController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using BikeTrips.Web.Controllers;
    using BikeTrips.Data.Models;
    using Data.Contracts;
    using Contracts;

    [TestFixture]
    public class ByIdShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ITripsService> tripsServiceMock = new Mock<ITripsService>();
        private readonly Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        private const string ValidIdString = "idstring";

        [Test]
        public void ShouldRedirectToHomeWhenIdIsNull()
        {
            //Arrange
            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act / Assert
            controller.WithCallTo(x => x.ById(null)).ShouldRedirectToRoute("");
        }

        [Test]
        public void ShouldRedirectToHomeWhenModelIsNull()
        {
            //Arrange
            this.tripsServiceMock.Setup(s => s.GetTripById(It.IsAny<int>()))
                .Returns((Trip)null);
            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act / Assert
            controller.WithCallTo(x => x.ById(ValidIdString)).ShouldRedirectToRoute("");
        }
        [Test]
        public void ShouldCallTripsServiceMethodGetUserById()
        {
            //Arrange
            var tripsServiceMock = new Mock<ITripsService>();
            tripsServiceMock.Setup(s => s.GetTripById(It.IsAny<int>()))
                .Returns(new Trip());

            var controller = new TripController(
                this.usersServiceMock.Object,
                tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);
           
            //Act
            controller.ById(ValidIdString);

            //Assert
            tripsServiceMock.Verify(s => s.GetTripById(It.IsAny<string>()), Times.Once);
        }
    }
}
