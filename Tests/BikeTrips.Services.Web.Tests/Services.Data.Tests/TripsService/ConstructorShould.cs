namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.TripsService
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
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        [Test]
        public void CreateInstanceOfTripsServiceWithoutParameters()
        {
            //Arrange / Act
            var service = new TripsService();

            //Assert
            Assert.IsInstanceOf<ITripsService>(service);
        }

        [Test]
        public void CreateInstanceOfCommentsServiceWithtParameters()
        {
            //Arange / Act
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            //Assert
            Assert.IsInstanceOf<ITripsService>(service);
        }

        [Test]
        public void ThrowIfTripsRepositoryIsNull()
        {
            //Arrange / Act / Assert
            Assert.That(() => new TripsService(
                    null,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                ),
                Throws.InstanceOf<ArgumentNullException>()
                .With.Message.Contain("Trips"));
        }

        [Test]
        public void ThrowIfUsersServiceIsNull()
        {
            //Arrange / Act / Assert
            Assert.That(() => new TripsService(
                    this.tripsRepositoryMock.Object,
                    null,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                ),
                Throws.InstanceOf<ArgumentNullException>()
                .With.Message.Contain("Users"));
        }

        [Test]
        public void ThrowIfCommentsServiceIsNull()
        {
            //Arrange / Act / Assert
            Assert.That(() => new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    null,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                ),
                Throws.InstanceOf<ArgumentNullException>()
                .With.Message.Contain("Comments"));
        }

        [Test]
        public void ThrowIfUnitOfWorkIsNull()
        {
            //Arrange / Act / Assert
            Assert.That(() => new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    null,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                ),
                Throws.InstanceOf<ArgumentNullException>()
                .With.Message.Contain("Unit of work"));
        }

        [Test]
        public void ThrowIfDateTimeConverterIsNull()
        {
            //Arrange / Act / Assert
            Assert.That(() => new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    null,
                    this.identifierProviderMock.Object
                ),
                Throws.InstanceOf<ArgumentNullException>()
                .With.Message.Contain("Converter"));
        }

        [Test]
        public void ThrowIfIdentifierProviderIsNull()
        {
            //Arrange / Act / Assert
            Assert.That(() => new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    null
                ),
                Throws.InstanceOf<ArgumentNullException>()
                .With.Message.Contain("Identifier provider"));
        }

        [Test]
        public void NotThrowWithValidParameters()
        {
            //Act / Assert
            Assert.DoesNotThrow(() => new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                ));
        }
    }
}
