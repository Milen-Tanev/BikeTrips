namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.CommentsService
{
    using Moq;
    using NUnit.Framework;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;
    using System.Collections.Generic;
    using System;

    [TestFixture]
    class DeleteAllCommentsShould
    {
        private readonly Mock<IBikeTripsDbRepository<Comment>> commentRepositoryMock = new Mock<IBikeTripsDbRepository<Comment>>();
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        [Test]
        public void ThrowIfParameterIsNull()
        {
            //Arrange
            var service = new CommentsService
                (this.commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                this.unitOfWorkMock.Object);

            //Act / Assert
            Assert.That(() => service.DeleteAllComments((ICollection<Comment>)null),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Comments"));
        }

        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        public void ShouldCallCommentRepositoryDeleteMethodCorrectTimesCount(int collectionLength)
        {
            var commentRepositoryMock = new Mock<IBikeTripsDbRepository<Comment>>();
            //Arrange
            var service = new CommentsService
                (commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                this.unitOfWorkMock.Object);

            var commentsCollection = new List<Comment>();

            for (int i = 0; i < collectionLength; i++)
            {
                commentsCollection.Add(new Comment());
            }
            //Act
            service.DeleteAllComments(commentsCollection);

            //Assert
            commentRepositoryMock.Verify(x => x.Delete(It.IsAny<Comment>()), Times.Exactly(collectionLength));
        }
    }
}
