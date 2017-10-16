namespace BikeTrips.Services.Web.Tests.Web.Controllers.TripController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using System;
    using TestStack.FluentMVCTesting;

    using BikeTrips.Data.Models;
    using BikeTrips.Web.Controllers;
    using Common.Constants;
    using Data.Contracts;
    using Contracts;
    using BikeTrips.Web.ViewModels.TripModels;

    [TestFixture]
    public class CretePostShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ITripsService> tripsServiceMock = new Mock<ITripsService>();
        private readonly Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        private const string ValidString = "validstring";

        [Test]
        public void SetModelDescriptionIfNull()
        {
            //Arrange
            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;

            //Act
            controller.Create(viewModelMock);

            //
            Assert.AreEqual(viewModelMock.Description, UiMessageConstants.NoTripDescription);
        }

        [Test]
        public void SetModelStateToErrorWithCorrectTypeWhenTripNameAlreadyExistsInComingTrips()
        {
            //Arrange
            var tripMock = new Mock<Trip>().Object;
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns(tripMock);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);
            
            var viewModelMock = new Mock<CreateTripViewModel>().Object;

            //Act
            controller.Create(viewModelMock);

            //Assert
            Assert.IsTrue(controller.ModelState.Values.Contains(controller.ModelState["TripName"]));
        }

        [Test]
        public void SetModelStateToErrorWithCorrectMessageWhenTripNameAlreadyExistsInComingTrips()
        {
            //Arrange
            var tripMock = new Mock<Trip>().Object;
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns(tripMock);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            var expected = ErrorMessageConstants.TripNameAlreadyExists;

            //Act
            controller.Create(viewModelMock);

            //Assert
            var errorMessage = controller.ModelState["TripName"].Errors[0].ErrorMessage;
            Assert.AreEqual(expected, errorMessage);
        }

        [Test]
        public void RedirectCorrectlyWhenTripNameAlreadyExistsInComingTrips()
        {
            //Arrange
            var tripMock = new Mock<Trip>().Object;
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns(tripMock);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            
            //Act / Assert
            controller.WithCallTo(c => c.Create(viewModelMock))
                .ShouldRenderView("Create");
        }

        [Test]
        public void RedirectWithCorrectModelWhenTripNameAlreadyExistsInComingTrips()
        {
            //Arrange
            var tripMock = new Mock<Trip>().Object;
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns(tripMock);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;

            //Act / Assert
            controller.WithCallTo(c => c.Create(viewModelMock))
                .ShouldRenderView("Create")
                .WithModel<CreateTripViewModel>();
        }

        [Test]
        public void SetModelStateToErrorWithCorrectTypeWhenTripIsSetupInThePast()
        {
            //Arrange
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            viewModelMock.LocalTimeOffsetMinutes = 100;
            viewModelMock.TripDate = new DateTime(2010, 10, 10, 10, 10, 10);
            
            //Act
            controller.Create(viewModelMock);

            //Assert
            Assert.IsTrue(controller.ModelState.Values.Contains(controller.ModelState["TripDate"]));
        }

        [Test]
        public void SetModelStateToErrorWithCorrectMessageWhenTripIsSetupInThePast()
        {
            //Arrange
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            viewModelMock.LocalTimeOffsetMinutes = 100;
            viewModelMock.TripDate = new DateTime(2010, 10, 10, 10, 10, 10);
            var expected = ErrorMessageConstants.TripDateInThePast;
            
            //Act
            controller.Create(viewModelMock);

            //Assert
            var errorMessage = controller.ModelState["TripDate"].Errors[0].ErrorMessage;
            Assert.AreEqual(expected, errorMessage);
        }

        [Test]
        public void RedirectCorrectlyWhenTripDateIsInThePast()
        {
            //Arrange
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            viewModelMock.LocalTimeOffsetMinutes = 100;
            viewModelMock.TripDate = new DateTime(2010, 10, 10, 10, 10, 10);

            //Act / Assert
            controller.WithCallTo(c => c.Create(viewModelMock))
                .ShouldRenderView("Create");
        }

        [Test]
        public void RedirectWithCorrectModelWhenTripDateIsInThePast()
        {
            //Arrange
            this.tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            viewModelMock.LocalTimeOffsetMinutes = 100;
            viewModelMock.TripDate = new DateTime(2010, 10, 10, 10, 10, 10);

            //Act / Assert
            controller.WithCallTo(c => c.Create(viewModelMock))
                .ShouldRenderView("Create")
                .WithModel<CreateTripViewModel>();
        }

        [Test]
        public void CallTripsServiceAddTripMethodOnce()
        {
            //Arrange
            var tripsServiceMock = new Mock<ITripsService>();
            tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            viewModelMock.LocalTimeOffsetMinutes = 100;
            viewModelMock.TripDate = new DateTime(2020, 10, 10, 10, 10, 10);

            var tripMock = new Mock<Trip>().Object;
            mapperMock.Setup(m => m.Map<Trip>(viewModelMock)).Returns(tripMock);

            var fullTripViewModelMock = new Mock<FullTripViewModel>();
            
            //Act
            controller.Create(viewModelMock);

            //Assert
            tripsServiceMock.Verify(s => s.AddTrip(It.IsAny<Trip>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public void CallCaceServiceRemoveMethodOnce()
        {
            //Arrange
            var cacheServiceMock = new Mock<ICacheService>();
            tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            viewModelMock.LocalTimeOffsetMinutes = 100;
            viewModelMock.TripDate = new DateTime(2020, 10, 10, 10, 10, 10);

            var tripMock = new Mock<Trip>().Object;
            mapperMock.Setup(m => m.Map<Trip>(viewModelMock)).Returns(tripMock);

            var fullTripViewModelMock = new Mock<FullTripViewModel>();
            mapperMock.Setup(m => m.Map<FullTripViewModel>(It.IsAny<Trip>())).Returns(fullTripViewModelMock.Object);
            
            //Act
            controller.Create(viewModelMock);

            //Assert
            cacheServiceMock.Verify(c => c.Remove(It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void RedirectToCorrectAction()
        {
            //Arrange
            tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            var viewModelMock = new Mock<CreateTripViewModel>().Object;
            viewModelMock.LocalTimeOffsetMinutes = 100;
            viewModelMock.TripDate = new DateTime(2020, 10, 10, 10, 10, 10);

            var tripMock = new Mock<Trip>().Object;
            mapperMock.Setup(m => m.Map<Trip>(viewModelMock)).Returns(tripMock);

            var fullTripViewModelMock = new Mock<FullTripViewModel>();
            mapperMock.Setup(m => m.Map<FullTripViewModel>(It.IsAny<Trip>())).Returns(fullTripViewModelMock.Object);

            //Act / Assert
            controller.WithCallTo(c => c.Create(viewModelMock))
                .ShouldRedirectTo<string>(c => c.ById);
        }

        [Test]
        public void RedirectToCreateViewIfModelStateNotValid()
        {
            //Arrange
            tripsServiceMock.Setup(s => s.GetTripByName(It.IsAny<string>()))
                .Returns((Trip)null);

            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            controller.ModelState.AddModelError(ValidString, new Exception());

            //Act / Assert
            controller.WithCallTo(c => c.Create(It.IsAny<CreateTripViewModel>()))
                .ShouldRenderView("Create");
        }
    }
}
