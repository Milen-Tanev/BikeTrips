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
    public class GetTripByStringIdShould
    {
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        [Test]
        public void ThrowIfTripIsNull()
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

            this.tripsRepositoryMock.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns((Trip)null);

            //Act / Assert
            Assert.That(() => service.GetTripById(It.IsAny<string>()),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Trip"));
        }

        [Test]
        public void CallIdentifierProviderGetIdMethodOnce()
        {
            var identifierProviderMock = new Mock<IIdentifierProvider>();

            //Arrange
            var service = new TripsService(
                    this.tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    identifierProviderMock.Object
                );

            var tripMock = new Mock<Trip>();
            this.tripsRepositoryMock.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(tripMock.Object);

            //Act
            service.GetTripById(It.IsAny<string>());

            //Assert
            identifierProviderMock.Verify(p => p.GetId(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ReturnCorrectTrip()
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

            var expectedTrip = new Mock<Trip>().Object;
            this.tripsRepositoryMock.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(expectedTrip);

            //Act
            var resultTrip = service.GetTripById(It.IsAny<string>());

            //Assert
            Assert.AreSame(expectedTrip, resultTrip);
        }

        [Test]
        public void TripsRepositoryGetByIdMethodIsCalledOnce()
        {
            var tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();

            //Arrange
            var service = new TripsService(
                    tripsRepositoryMock.Object,
                    this.usersServiceMock.Object,
                    this.commentsServiceMock.Object,
                    this.unitOfWorkMock.Object,
                    this.converterMock.Object,
                    this.identifierProviderMock.Object
                );

            var tripMock = new Mock<Trip>();
            tripsRepositoryMock.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(tripMock.Object);

            //Act 
            service.GetTripById(It.IsAny<string>());

            //Assert
            tripsRepositoryMock.Verify(r => r.GetById(It.IsAny<int>()),
                Times.Once);
        }
    }
}
