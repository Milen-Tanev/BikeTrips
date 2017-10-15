namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.UsersService
{
    using Moq;
    using NUnit.Framework;
    using System.Linq;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;
    using System.Collections.Generic;

    [TestFixture]
    public class GetAllUsersAdmin
    {
        private readonly Mock<IBikeTripsDbRepository<User>> usersRepositoryMock = new Mock<IBikeTripsDbRepository<User>>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        
        [Test]
        public void ReturnExpectedCollection()
        {
            //Arrange
            var service = new UsersService(
                    this.usersRepositoryMock.Object,
                    this.unitOfWorkMock.Object
                );
            
            var expectedCollection = new List<User>();
            for (int i = 0; i < 10; i++)
            {
                expectedCollection.Add(new User() { UserName = i + "username" } );
            }
            this.usersRepositoryMock.Setup(r => r.AdminAll()).Returns(expectedCollection.AsQueryable);
            
            //Act
            var allUsers = service.GetAllUsersAdmin();

            //Assert
            Assert.AreEqual(expectedCollection, allUsers);
        }

        [Test]
        public void ReturnExpectedCollectionSorted()
        {
            //Arrange
            var service = new UsersService(
                    this.usersRepositoryMock.Object,
                    this.unitOfWorkMock.Object
                );


            var expectedCollection = new List<User>();
            for (int i = 10; i > 0; i--)
            {
                expectedCollection.Add(new User() { UserName = i + "username" });
            }
            this.usersRepositoryMock.Setup(r => r.AdminAll()).Returns(expectedCollection.AsQueryable);

            expectedCollection.Sort(delegate (User x, User y)
            {
                return x.UserName.CompareTo(y.UserName);
            });

            //Act
            var allUsers = service.GetAllUsersAdmin();

            //Assert
            Assert.AreEqual(expectedCollection, allUsers);
        }

        [Test]
        public void CallUsersRepositoryMethodAllOnce()
        {
            var usersRepositoryMock = new Mock<IBikeTripsDbRepository<User>>();
            //Arrange
            var service = new UsersService(
                    usersRepositoryMock.Object,
                    this.unitOfWorkMock.Object
                );

            //Act
            var tripsCollection = service.GetAllUsersAdmin();

            //Assert
            usersRepositoryMock.Verify(r => r.AdminAll(),
                Times.Once);
        }
    }
}
