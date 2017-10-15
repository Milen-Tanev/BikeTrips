namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.UsersService
{
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Linq;

    using BikeTrips.Data.Common.Contracts;
    using BikeTrips.Data.Models;
    using BikeTrips.Services.Data;

    [TestFixture]
    public class GetUserByIdShould
    {
        private readonly Mock<IBikeTripsDbRepository<User>> usersRepositoryMock = new Mock<IBikeTripsDbRepository<User>>();
        private readonly Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        [Test]
        public void ThrowIfUserIsNullWithCorrectMessage()
        {
            //Arrange
            var service = new UsersService(
                this.usersRepositoryMock.Object,
                this.unitOfWorkMock.Object
            );

            this.usersRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns((User)null);

            //Act / Assert
            Assert.That(() => service.GetUserById(It.IsAny<string>()),
                Throws.TypeOf<ArgumentNullException>()
                .With.Message.Contains("User"));
        }

        [Test]
        public void ReturnCorrectUser()
        {
            //Arrange
            var service = new UsersService(
                this.usersRepositoryMock.Object,
                this.unitOfWorkMock.Object
            );

            var expected = new Mock<User>().Object;

            this.usersRepositoryMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(expected);

            //Act
            var result = service.GetUserById(It.IsAny<string>());

            //Assert
            Assert.AreSame(expected, result);
        }
    }
}
