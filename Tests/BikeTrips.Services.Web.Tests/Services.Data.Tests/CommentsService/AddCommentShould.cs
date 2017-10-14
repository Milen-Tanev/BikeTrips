namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.CommentsService
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;

    [TestFixture]
    public class AddCommentShould
    {
        private const string ValidString = "ValidString";

        private readonly Mock<IBikeTripsDbRepository<Comment>> commentRepositoryMock = new Mock<IBikeTripsDbRepository<Comment>>();
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        [TestCase(null, ValidString, "Comment content")]
        [TestCase(ValidString, null, "Subject url")]
        public void ThrowIfParamsAreNull(string content, string url, string message)
        {
            // Arrange
            var service = new CommentsService();
            
            //Act / Assert
            Assert.That(() => service.AddComment(content, url),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains(message));
        }
        
        [Test]
        public void ThrowIfTripsRepositoryGetByIdReturnsNull()
        {
            //Arrange
            var service = new CommentsService
                (this.commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                this.unitOfWorkMock.Object);

            var mockUser = new Mock<User>();

            this.tripRepositoryMock.Setup(t => t.GetById(It.IsAny<int>())).Returns((Trip)null);
            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns(mockUser.Object);

            //Act / Assert
            Assert.That(() => service.AddComment(ValidString, ValidString),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Trip"));
        }

        [Test]
        public void ThrowIfUserServiceGetCurrentUserReturnsNull()
        {
            //Arrange
            var service = new CommentsService
                (this.commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                this.unitOfWorkMock.Object);

            var mockTrip = new Mock<Trip>();

            this.tripRepositoryMock.Setup(t => t.GetById(It.IsAny<int>())).Returns(mockTrip.Object);
            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns((User)null);

            //Act / Assert
            Assert.That(() => service.AddComment(ValidString, ValidString),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Author"));
        }

        [Test]
        public void AddToTripsComment()
        {
            //Arrange
            var service = new CommentsService
                (this.commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                this.unitOfWorkMock.Object);

            var mockTrip = new Mock<Trip>();
            mockTrip.Setup(t => t.Comments).Returns(new List<Comment>());

            this.tripRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockTrip.Object);

            var mockUser = new Mock<User>();
            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns(mockUser.Object);
            
            //Act
            service.AddComment(ValidString, ValidString);

            //Assert
            Assert.AreEqual(mockTrip.Object.Comments.Count, 1);
        }

        [Test]
        public void CallCommentsRepositoryAddMethodOnce()
        {
            //Arrange
            var commentRepositoryMock = new Mock<IBikeTripsDbRepository<Comment>>();

            var service = new CommentsService
                (commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                this.unitOfWorkMock.Object);

            var mockTrip = new Mock<Trip>();
            mockTrip.Setup(t => t.Comments).Returns(new List<Comment>());

            var mockUser = new Mock<User>();
            this.tripRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockTrip.Object);
            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns(mockUser.Object);

            //Act
            service.AddComment(ValidString, ValidString);

            //Assert
            commentRepositoryMock.Verify(c => c.Add(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommitMethodOnce()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var service = new CommentsService
                (commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                unitOfWorkMock.Object);

            var mockTrip = new Mock<Trip>();
            mockTrip.Setup(t => t.Comments).Returns(new List<Comment>());

            var mockUser = new Mock<User>();
            this.tripRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockTrip.Object);
            this.usersServiceMock.Setup(x => x.GetCurrentUser()).Returns(mockUser.Object);

            //Act
            service.AddComment(ValidString, ValidString);

            //Assert
            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
