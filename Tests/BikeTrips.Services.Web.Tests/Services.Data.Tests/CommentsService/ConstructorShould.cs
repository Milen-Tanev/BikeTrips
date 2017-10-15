namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.CommentsService
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
    public class ConstructorShould
    {
        private readonly IBikeTripsDbRepository<Comment> commentRepositoryMock = new Mock<IBikeTripsDbRepository<Comment>>().Object;
        private readonly IBikeTripsDbRepository<Trip> tripRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>().Object;
        private readonly IUserService usersServiceMock = new Mock<IUserService>().Object;
        private readonly IIdentifierProvider identifierProviderMock = new Mock<IIdentifierProvider>().Object;
        private readonly IUnitOfWork unitOfWorkMock = new Mock<IUnitOfWork>().Object;

        [Test]
        public void CreateInstanceOfCommentsServiceWithoutParameters()
        {
            //Act
            var service = new CommentsService();

            //Assert
            Assert.IsInstanceOf<CommentsService>(service);
        }

        [Test]
        public void CreateInstanceOfCommentsServiceWithtParameters()
        {
            //Act
            var service = new CommentsService
                (this.commentRepositoryMock,
                this.tripRepositoryMock,
                this.usersServiceMock,
                this.identifierProviderMock,
                this.unitOfWorkMock);

            //Assert
            Assert.IsInstanceOf<CommentsService>(service);
        }

        [Test]
        public void ThrowIfCommentsRepositoryIsNull()
        {
            //Act / Assert
            Assert.That(() => new CommentsService
                (null,
                this.tripRepositoryMock,
                this.usersServiceMock,
                this.identifierProviderMock,
                this.unitOfWorkMock), Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Comments"));
        }

        [Test]
        public void ThrowIfTripsRepositoryIsNull()
        {
            //Act / Assert
            Assert.That(() => new CommentsService
                (this.commentRepositoryMock,
                null,
                this.usersServiceMock,
                this.identifierProviderMock,
                this.unitOfWorkMock), Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Trips"));
        }

        [Test]
        public void ThrowIfUsersServiceIsNull()
        {
            //Act / Assert
            Assert.That(() => new CommentsService
                (this.commentRepositoryMock,
                this.tripRepositoryMock,
                null,
                this.identifierProviderMock,
                this.unitOfWorkMock), Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Users"));
        }

        [Test]
        public void ThrowIfIdentifierProviderIsNull()
        {
            //Act / Assert
            Assert.That(() => new CommentsService
                (this.commentRepositoryMock,
                this.tripRepositoryMock,
                this.usersServiceMock,
                null,
                this.unitOfWorkMock), Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Identifier provider"));
        }

        [Test]
        public void ThrowIfUnitOfWorkIsNull()
        {
            //Act / Assert
            Assert.That(() => new CommentsService
                (this.commentRepositoryMock,
                this.tripRepositoryMock,
                this.usersServiceMock,
                this.identifierProviderMock,
                null), Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Unit of work"));
        }

        [Test]
        public void NotThrowWithValidParameters()
        {
            //Act / Assert
            Assert.DoesNotThrow(() => new CommentsService
                (this.commentRepositoryMock,
                this.tripRepositoryMock,
                this.usersServiceMock,
                this.identifierProviderMock,
                this.unitOfWorkMock));
        }
    }
}
