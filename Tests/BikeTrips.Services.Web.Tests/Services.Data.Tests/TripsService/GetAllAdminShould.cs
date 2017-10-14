namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.TripsService
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using BikeTrips.Services.Data.Contracts;
    using Contracts;

    [TestFixture]
    public class GetAllAdminShould
    {
        private readonly Mock<IBikeTripsDbRepository<Trip>> tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ICommentsService> commentsServiceMock = new Mock<ICommentsService>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IDateTimeConverter> converterMock = new Mock<IDateTimeConverter>();
        private readonly Mock<IIdentifierProvider> identifierProviderMock = new Mock<IIdentifierProvider>();

        [Test]
        public void ReturnExpectedCollection()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var expectedCollection = new List<Trip>();

            for (int i = 0; i < 10; i++)
            {
                expectedCollection.Add(new Trip() { StartingTime = new DateTime(2018, 12, 10, 11, 11, i) });
            }
            this.tripsRepositoryMock.Setup(x => x.AdminAll()).Returns(expectedCollection.AsQueryable);

            //Act
            var tripsCollection = service.GetAllAdmin();

            //Assert
            Assert.AreEqual(expectedCollection, tripsCollection);
        }

        [Test]
        public void ReturnExpectedCollectionSorted()
        {
            //Arrange
            var service = new TripsService(
                this.tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            var expectedCollection = new List<Trip>();
            for (int i = 10; i > 0; i--)
            {
                expectedCollection.Add(new Trip() { StartingTime = new DateTime(2018, 12, 10, 11, 11, i) });
            }
            this.tripsRepositoryMock.Setup(x => x.AdminAll()).Returns(expectedCollection.AsQueryable);
            expectedCollection.Sort(delegate (Trip x, Trip y)
            {
                return x.StartingTime.CompareTo(y.StartingTime);
            });

            //Act
            var tripsCollection = service.GetAllAdmin();

            //Assert
            Assert.AreEqual(expectedCollection, tripsCollection);
        }

        [Test]
        public void CallTripsRepositoryMethodAllOnce()
        {
            var tripsRepositoryMock = new Mock<IBikeTripsDbRepository<Trip>>();
            //Arrange
            var service = new TripsService(
                tripsRepositoryMock.Object,
                this.usersServiceMock.Object,
                this.commentsServiceMock.Object,
                this.unitOfWorkMock.Object,
                this.converterMock.Object,
                this.identifierProviderMock.Object);

            //Act
            var tripsCollection = service.GetAllAdmin();

            //Assert
            tripsRepositoryMock.Verify(r => r.AdminAll(),
                Times.Once);
        }

    }
}
