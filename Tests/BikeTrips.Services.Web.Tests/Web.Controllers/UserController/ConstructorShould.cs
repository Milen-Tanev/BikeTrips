namespace BikeTrips.Services.Web.Tests.Web.Controllers.AccountController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using System;

    using BikeTrips.Web.Controllers;
    using Data.Contracts;

    [TestFixture]
    public class ConstructorShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        [Test]
        public void CreateInstanceOfUserController()
        {
            //Act
            var controller = new UserController(usersServiceMock.Object, mapperMock.Object);

            //Assert
            Assert.IsInstanceOf<UserController>(controller);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenUsersServiceIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new UserController(null, this.mapperMock.Object),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Users"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenIMapperIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new UserController(this.usersServiceMock.Object, null),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Mapper"));
        }

        [Test]
        public void NotThrowWithValidParameters()
        {
            //Act / Assert
            Assert.DoesNotThrow(() => new UserController(this.usersServiceMock.Object, this.mapperMock.Object));
        }
    }
}
