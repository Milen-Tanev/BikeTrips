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

    [TestFixture]
    public class AddParticipantToShould
    {
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        [Test]
        public void ThrowIfUserIsNullWithCorrectMessage()
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

            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns((User)null);

            //Act / Assert
            Assert.That(() => service.AddParticipantTo(It.IsAny<Trip>()),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("User"));
        }

        [Test]
        public void AddCorrectParticipantToTrip()
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

            var mockTrip = new Mock<Trip>();
            var mockUser = new Mock<User>();

            mockTrip.Setup(t => t.Participants).Returns(new List<User>());
            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns(mockUser.Object);

            //Act
            service.AddParticipantTo(mockTrip.Object);

            //Assert
            Assert.IsTrue(mockTrip.Object.Participants.Any(u => u == mockUser.Object));
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
                    this.identifierProviderMock.Object
                );

            var mockTrip = new Mock<Trip>();
            var mockUser = new Mock<User>();

            mockTrip.Setup(t => t.Participants).Returns(new List<User>());
            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns(mockUser.Object);

            //Act
            service.AddParticipantTo(mockTrip.Object);

            //Assert
            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
