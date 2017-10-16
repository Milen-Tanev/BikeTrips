namespace BikeTrips.Services.Web.Tests.Web.Controllers.TripController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using BikeTrips.Data.Models;
    using BikeTrips.Web.Controllers;
    using BikeTrips.Web.ViewModels.TripModels;
    using Data.Contracts;
    using Contracts;

    [TestFixture]
    public class JoinTripShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ITripsService> tripsServiceMock = new Mock<ITripsService>();
        private readonly Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        [Test]
        public void CallTripsServiceMethodGetByIdOnce()
        {
            //Arrange
            var tripsServiceMock = new Mock<ITripsService>();

            var controller = new TripController(
                this.usersServiceMock.Object,
                tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act
            controller.JoinTrip(It.IsAny<int>());

            //Assert
            tripsServiceMock.Verify(s => s.GetTripById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void CallTripsServiceMethodAddParticipantToOnce()
        {
            //Arrange
            var tripsServiceMock = new Mock<ITripsService>();

            var controller = new TripController(
                this.usersServiceMock.Object,
                tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act
            controller.JoinTrip(It.IsAny<int>());

            //Assert
            tripsServiceMock.Verify(s => s.AddParticipantTo(It.IsAny<Trip>()), Times.Once);
        }

        [Test]
        public void CallCacheServiceMethodRemoveOnce()
        {
            //Arrange
            var cacheServiceMock = new Mock<ICacheService>();

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act
            controller.JoinTrip(It.IsAny<int>());

            //Assert
            cacheServiceMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void RenderCorrectPartialView()
        {
            //Arrange
            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);
            
            //Act / Assert
            controller.WithCallTo(c => c.JoinTrip(It.IsAny<int>()))
                .ShouldRenderPartialView("_ButtonsPartial");
        }
        
        [Test]
        public void RenderCoPartialViewWithCorrectViewModel()
        {
            //Arrange
            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<FullTripViewModel>().Object;
            this.mapperMock.Setup(m => m.Map<FullTripViewModel>(It.IsAny<Trip>()))
                .Returns(viewModelMock);

            //Act / Assert
            controller.WithCallTo(c => c.JoinTrip(It.IsAny<int>()))
                .ShouldRenderPartialView("_ButtonsPartial")
                .WithModel<FullTripViewModel>();
        }
    }
}
