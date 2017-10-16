namespace BikeTrips.Services.Web.Tests.Web.Controllers.TripController
{
    using AutoMapper;
    using Moq;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    using BikeTrips.Web.Controllers;
    using Data.Contracts;
    using Contracts;

    [TestFixture]
    public class CreateGetShould
    {
        private readonly Mock<IUserService> usersServiceMock = new Mock<IUserService>();
        private readonly Mock<ITripsService> tripsServiceMock = new Mock<ITripsService>();
        private readonly Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        [Test]
        public void RenderCorrectView()
        {
            //Arrange
            var controller = new TripController(
                this.usersServiceMock.Object,
                this.tripsServiceMock.Object,
                this.cacheServiceMock.Object,
                this.mapperMock.Object);

            //Act / Assert
            controller.WithCallTo(c => c.Create())
                .ShouldRenderView("Create");
        }
    }
}
