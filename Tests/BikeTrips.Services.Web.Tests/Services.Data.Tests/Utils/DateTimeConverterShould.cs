namespace BikeTrips.Services.Web.Tests.Services.Data.Tests.Utils
{
    using NUnit.Framework;
    using System;
    using System.Globalization;

    [TestFixture]
    class DateTimeConverterShould
    {
        [Test]
        public void ConvertDateTimeCorrectly()
        {
            //Arrange
            var expectedResult = new DateTime(2017, 11, 18, 10, 10, 10);
            var date = new DateTime(2017, 11, 18);
            var time = new DateTime(2019, 01, 01, 10, 10, 10);
            var converter = new DateTimeConverter();

            //Act
            var actualResult = converter.Convert(date, time);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
