namespace BikeTrips.Services.Web.Tests.Services.Data.Tests
{
    using Moq;
    using NUnit.Framework;
    using System;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;

    [TestFixture]
    public class CommentsServiceAddCommentShould
    {
        private const string ValidString = "ValidString";

        private readonly IBikeTripsDbRepository<Comment> commentRepositoryMock = new Mock<IBikeTripsDbRepository<Comment>>().Object;
        private readonly IBikeTripsDbRepository<Trip> tripRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>().Object;
        private readonly IUserService usersServiceMock = new Mock<IUserService>().Object;
        private readonly IIdentifierProvider identifierProviderMock = new Mock<IIdentifierProvider>().Object;
        private readonly IUnitOfWork unitOfWorkMock = new Mock<IUnitOfWork>().Object;

        [TestCase(null, ValidString, "Comment content")]
        [TestCase(ValidString, null, "Subject url")]
        public void ThrowIfParamsAreNull(string content, string url, string message)
        {
            // Arrange
            var service = new CommentsService();

            //Act / Assert
            Assert.That(() => service.AddComment(content, url), Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains(message));
        }
    }
}
