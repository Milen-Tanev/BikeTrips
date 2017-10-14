namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.CommentsService
{
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    using BikeTrips.Services.Data;
    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;

    [TestFixture]
    public class GetAllAdminShould
    {
        private readonly Mock<IBikeTripsDbRepository<Comment>> commentRepositoryMock = new Mock<IBikeTripsDbRepository<Comment>>();
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        [Test]
        public void ReturnExpectedCollection()
        {
            //Arrange
            var service = new CommentsService
                (this.commentRepositoryMock.Object,
                this.tripRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.identifierProviderMock.Object,
                this.unitOfWorkMock.Object);

            var expectedCollection = new List<Comment>();
            for (int i = 0; i < 10; i++)
            {
                expectedCollection.Add(new Comment() { Author = new User { UserName = i + "username" } });
            }
            this.commentRepositoryMock.Setup(x => x.AdminAll()).Returns(expectedCollection.AsQueryable);

            //Act
            var commentsCollection = service.GetAllAdmin().ToList();

            //Assert
            Assert.AreEqual(expectedCollection, commentsCollection);
        }
    }
}
