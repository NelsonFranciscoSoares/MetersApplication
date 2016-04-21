using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetersApplication.Utils.UnitTests
{
    [TestClass]
    public class TestMethods
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var value = 1.014;

            //Act
            var valueRounded = FormatUtils.Round(value);

            //Assert
            Assert.AreEqual(1.01, valueRounded);
        }

        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            var value = 1.015;

            //Act
            var valueRounded = FormatUtils.Round(value);

            //Assert
            Assert.AreEqual(1.02, valueRounded);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            var value = 1.025;

            //Act
            var valueRounded = FormatUtils.Round(value);

            //Assert
            Assert.AreEqual(1.02, valueRounded);
        }

        [TestMethod]
        public void TestMethod4()
        {
            //Arrange
            var value = 1.018;

            //Act
            var valueRounded = FormatUtils.Round(value);

            //Assert
            Assert.AreEqual(1.02, valueRounded);
        }

        [TestMethod]
        public void TestMethod5()
        {
            //Arrange
            var value = 1.016;

            //Act
            var valueRounded = FormatUtils.Round(value);

            //Assert
            Assert.AreEqual(1.02, valueRounded);
        }
    }
}
