namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.UsersService
{
    using Moq;
    using NUnit.Framework;
    using System;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;

    [TestFixture]
    public class ConstructorShould
    {
        private readonly Mock<IBikeTripsDbRepository<User>> usersRepositoryMock = new Mock<IBikeTripsDbRepository<User>>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        [Test]
        public void CreateInstanceOfUsersServiceWithoutParameters()
        {
            //Act
            var service = new UsersService();

            //Assert
            Assert.IsInstanceOf<UsersService>(service);
        }

        [Test]
        public void CreateInstanceOfUsersServiceWIthParameters()
        {
            //Act
            var service = new UsersService(this.usersRepositoryMock.Object, this.unitOfWorkMock.Object);

            //Assert
            Assert.IsInstanceOf<UsersService>(service);
        }

        [Test]
        public void ThrowIfUsersRepositoryIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new UsersService(null, this.unitOfWorkMock.Object),
                Throws.TypeOf<ArgumentNullException>().With.Message.Contains("Users"));
        }

        [Test]
        public void ThrowIfUnitOfWorkIsNullWithCorrectMessage()
        {
            //Act / Assert
            Assert.That(() => new UsersService(this.usersRepositoryMock.Object, null),
                Throws.TypeOf<ArgumentNullException>().With.Message.Contains("Unit of work"));
        }

        [Test]
        public void NotThrowWIthCorrectParameters()
        {
            //Act / Assert
            Assert.DoesNotThrow(() => new UsersService(this.usersRepositoryMock.Object, this.unitOfWorkMock.Object));
        }
    }
}
