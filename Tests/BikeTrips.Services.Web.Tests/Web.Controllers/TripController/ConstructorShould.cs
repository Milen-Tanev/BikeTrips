namespace BikeTrips.Services.Web.Tests.Web.Controllers.TripController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using System;

    using BikeTrips.Web.Controllers;
    using Data.Contracts;
    using Contracts;

    [TestFixture]
    public class ConstructorShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ITripsService> tripsServiceMock = new Mock<ITripsService>();
        private readonly Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        [Test]
        public void CreateInstanceOfTripControllerWithoutParameters()
        {
            //Act
            var controller = new TripController();

            //Assert
            Assert.IsInstanceOf<TripController>(controller);
        }

        [Test]
        public void CreateInstanceOfTripControllerWithParameters()
        {
            //Act
            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object
                );

            //Assert
            Assert.IsInstanceOf<TripController>(controller);
        }

        [Test]
        public void ThrowArgumentNullExceptionIfUsersServiceIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new TripController(
                null,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object
                ),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Users"));
        }

        [Test]
        public void ThrowArgumentNullExceptionIfTripsServiceIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new TripController(
                this.usersServiceMock.Object,
                null,
                this.cacheServiceMock.Object,
                this.mapperMock.Object
                ),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Trips"));
        }

        [Test]
        public void ThrowArgumentNullExceptionIfCacheServiceIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                null,
                this.mapperMock.Object
                ),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("HTTP cache service"));
        }

        [Test]
        public void ThrowArgumentNullExceptionIfIMapperIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                null
                ),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Mapper"));
        }

        [Test]
        public void NotThrowWithValidParameters()
        {
            //Act / Assert
            Assert.DoesNotThrow(() => new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object
                ));
        }
    }
}
