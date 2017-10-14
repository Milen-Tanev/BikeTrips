namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.TripsService
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;
    using Common.Constants;

    [TestFixture]
    public class AddTripShould
    {
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        private readonly Trip trip = new Trip() { LocalTimeOffsetMinutes = 60 };
        private readonly Mock<User> userMock = new Mock<User>();

        [Test]
        public void ThrowIfPassedDateIsEarlierThanNow()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var timeInThePast = new DateTime(2010, 10, 10, 10, 10, 10);

            this.userMock.Setup(u => u.AdministeredEvents).Returns(new List<Trip>());

            converterMock.Setup(c => c.Convert(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(timeInThePast);

            //Act / Assert
            Assert.That(() => 
                service.AddTrip(this.trip, It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                Throws.TypeOf<ArgumentException>()
                .With.Message.Contains(ErrorMessageConstants.TripHasPassed));
        }

        [Test]
        public void SetsCorrectStartingTime()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var validTime = new DateTime(2018, 10, 10, 10, 10, 10);

            converterMock.Setup(c => c.Convert(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(validTime);

            usersServiceMock.Setup(x => x.GetCurrentUser())
                .Returns(this.userMock.Object);
            
            //Act
            service.AddTrip(this.trip, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            Assert.AreEqual(validTime, this.trip.StartingTime);
        }

        [Test]
        public void AddTripToUserAdministeredEvents()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var validTime = new DateTime(2018, 10, 10, 10, 10, 10);
            this.userMock.Setup(u => u.AdministeredEvents).Returns(new List<Trip>());

            converterMock.Setup(c => c.Convert(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(validTime);

            usersServiceMock.Setup(x => x.GetCurrentUser())
                .Returns(this.userMock.Object);
            
            //Act
            service.AddTrip(this.trip, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            Assert.IsTrue(this.trip.Creator.AdministeredEvents.Any(x => x == trip));
        }

        [Test]
        public void SetCorrectCreatorToTrip()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var validTime = new DateTime(2018, 10, 10, 10, 10, 10);

            converterMock.Setup(c => c.Convert(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(validTime);
            
            usersServiceMock.Setup(x => x.GetCurrentUser())
                .Returns(this.userMock.Object);

            //Act
            service.AddTrip(this.trip, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            Assert.AreSame(this.userMock.Object, this.trip.Creator);
        }

        [Test]
        public void AddUserToParticipants()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var validTime = new DateTime(2018, 10, 10, 10, 10, 10);

            converterMock.Setup(c => c.Convert(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(validTime);

            usersServiceMock.Setup(x => x.GetCurrentUser())
                .Returns(this.userMock.Object);

            //Act
            service.AddTrip(this.trip, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            Assert.IsTrue(this.trip.Participants.Any(x => x == this.userMock.Object));
        }

        [Test]
        public void CallTripRepositoryAddMethodOnce()
        {
            var tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();

            //Arrange
            var service = new TripsService(
                tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var validTime = new DateTime(2018, 10, 10, 10, 10, 10);

            converterMock.Setup(c => c.Convert(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(validTime);

            usersServiceMock.Setup(x => x.GetCurrentUser())
                .Returns(this.userMock.Object);

            //Act
            service.AddTrip(this.trip, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            tripsRepositoryMock.Verify(x => x.Add(It.IsAny<Trip>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommitMethodOnce()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var validTime = new DateTime(2018, 10, 10, 10, 10, 10);

            converterMock.Setup(c => c.Convert(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(validTime);

            usersServiceMock.Setup(x => x.GetCurrentUser())
                .Returns(this.userMock.Object);

            //Act
            service.AddTrip(this.trip, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }
    }
}
