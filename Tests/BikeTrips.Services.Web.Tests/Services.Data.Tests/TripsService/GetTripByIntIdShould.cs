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
    public class GetTripByIntIdShould
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
            Assert.That(() => service.GetTripById(It.IsAny<int>()),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("Trip"));
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

            var expected = new Mock<Trip>().Object;
            this.tripsRepositoryMock.Setup(r => r.GetById(It.IsAny<int>()))
                .Returns(expected);

            //Act 
            var result = service.GetTripById(It.IsAny<int>());

            //Assert
            Assert.AreSame(expected, result);
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
            service.GetTripById(It.IsAny<int>());

            //Assert
            tripsRepositoryMock.Verify(r => r.GetById(It.IsAny<int>()),
                Times.Once);
        }
    }
}
