﻿namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.TripsService
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using BikeTrips.Services.Data.Contracts;
    using Common.Constants;
    using Contracts;

    [TestFixture]
    public class DeleteTripShould
    {
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        [Test]
        public void ThrowIfCurrentUserDifferentThanTripCreator()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var firstUser = new Mock<User>();
            var secondUser = new Mock<User>();
            var trip = new Mock<Trip>();

            this.usersServiceMock.Setup(s => s.GetCurrentUser())
                .Returns(firstUser.Object);
            trip.Setup(t => t.Creator)
                .Returns(secondUser.Object);

            //Act / Assert
            Assert.That(() =>
                service.DeleteTrip(trip.Object),
                Throws.TypeOf<UnauthorizedAccessException>()
                .With.Message.Contains(ErrorMessageConstants.NotCreator));
        }

        [Test]
        public void CallCommentsServiceDeleteAllMethodOnceIfCommentsExist()
        {
            var commentsServiceMock = new Mock<ICommentsService>();
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var userMock = new Mock<User>();
            var tripMock = new Mock<Trip>();
            var commentMock = new Mock<Comment>();

            this.usersServiceMock.Setup(s => s.GetCurrentUser())
                .Returns(userMock.Object);
            tripMock.Setup(t => t.Creator)
                .Returns(userMock.Object);
            tripMock.Setup(t => t.Comments)
                .Returns(new List<Comment>() { commentMock.Object });

            //Act
            service.DeleteTrip(tripMock.Object);

            //Assert
            commentsServiceMock.Verify(x => x.DeleteAllComments(tripMock.Object.Comments), Times.Once);
        }

        [Test]
        public void FlagsDeletedTrueOfTrip()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var userMock = new Mock<User>();
            var tripMock = new Mock<Trip>();

            this.usersServiceMock.Setup(s => s.GetCurrentUser())
                .Returns(userMock.Object);
            tripMock.Setup(t => t.Creator)
                .Returns(userMock.Object);
            tripMock.Setup(t => t.Comments)
                .Returns(new List<Comment>());

            Assert.IsFalse(tripMock.Object.IsDeleted);

            //Act
            service.DeleteTrip(tripMock.Object);

            //Assert
            Assert.IsTrue(tripMock.Object.IsDeleted);
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

            var userMock = new Mock<User>();
            var tripMock = new Mock<Trip>();

            this.usersServiceMock.Setup(s => s.GetCurrentUser())
                .Returns(userMock.Object);
            tripMock.Setup(t => t.Creator)
                .Returns(userMock.Object);
            tripMock.Setup(t => t.Comments)
                .Returns(new List<Comment>());

            //Act
            service.DeleteTrip(tripMock.Object);

            //Assert
            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
