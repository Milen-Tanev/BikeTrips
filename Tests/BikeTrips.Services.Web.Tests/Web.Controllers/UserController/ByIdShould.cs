namespace BikeTrips.Services.Web.Tests.Web.Controllers.UserController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using BikeTrips.Data.Models;
    using BikeTrips.Web.Controllers;
    using BikeTrips.Web.ViewModels.UserModels;
    using Data.Contracts;

    [TestFixture]
    public class ByIdShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();
        private const string ValidIdString = "idstring";

        [Test]
        public void ShouldRedirectToHomeWhenIdIsNull()
        {
            //Arrange
            var controller = new UserController(this.usersServiceMock.Object, this.mapperMock.Object);

            //Act / Assert
            controller.WithCallTo(x => x.ById(null)).ShouldRedirectToRoute("");
        }

        [Test]
        public void ShouldRedirectToHomeWhenModelIsNull()
        {
            //Arrange
            this.usersServiceMock.Setup(s => s.GetUserById(It.IsAny<string>()))
                .Returns((User)null);
            var controller = new UserController(this.usersServiceMock.Object, this.mapperMock.Object);

            //Act / Assert
            controller.WithCallTo(x => x.ById(ValidIdString)).ShouldRedirectToRoute("");
        }

        [Test]
        public void ShouldRenderByIdView()
        {
            //Arrange
            this.usersServiceMock.Setup(s => s.GetUserById(It.IsAny<string>()))
                .Returns(new User());
            var controller = new UserController(this.usersServiceMock.Object, this.mapperMock.Object);

            //Act / Assert
            controller.WithCallTo(x => x.ById(ValidIdString)).ShouldRenderView("ById");
        }

        [Test]
        public void ShouldRenderWithCorrectModel()
        {
            //Arrange
            this.usersServiceMock.Setup(s => s.GetUserById(It.IsAny<string>()))
                .Returns(new User());
            this.mapperMock.Setup(m => m.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(new UserViewModel());
            var controller = new UserController(this.usersServiceMock.Object, this.mapperMock.Object);

            //Act / Assert
            controller.WithCallTo(c => c.ById(ValidIdString))
                .ShouldRenderView("ById")
                .WithModel<UserViewModel>();
        }

        [Test]
        public void ShouldCallUsersServiceMethodGetUserById()
        {
            //Arrange
            var usersServiceMock = new Mock<IUserService>();

            usersServiceMock.Setup(s => s.GetUserById(It.IsAny<string>()))
                .Returns(new User());
            this.mapperMock.Setup(m => m.Map<UserViewModel>(It.IsAny<User>()))
                .Returns(new UserViewModel());
            var controller = new UserController(usersServiceMock.Object, this.mapperMock.Object);

            //Act
            controller.ById(ValidIdString);

            //Assert
            usersServiceMock.Verify(s => s.GetUserById(It.IsAny<string>()), Times.Once);
        }
    }
}
