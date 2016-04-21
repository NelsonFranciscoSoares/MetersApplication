using MetersApplication.Core;
using MetersApplication.ProtocolBase;
using MetersApplication.ProtocolBase.Constants;
using MetersApplication.ProtocolBase.Exceptions;
using MetersApplication.ProtocolBase.Factory;
using MetersApplication.SocketTest;
using MetersApplication.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetersApplication.Protocol.UnitTests
{
    [TestClass]
    public class ProtocolTest
    {
        private IExchangeDataComponent NetworkService { get; set; }
        private ProtocolValidator Validator { get; set; }

        [TestInitialize]
        public void Init()
        {
            this.Validator = new ProtocolValidator();            
        }

        [TestMethod]
        public void ConnectNoError()
        {
            //Arrange
            this.NetworkService = new MySocketTestConnectNoError();
            
            //Act
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_CONNECT);
            this.NetworkService.Send(frame);

            //Create frame response
            var buffer = new byte[100];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_CONNECT);

            //Assert
            Assert.AreEqual(MetersOperationsConstants.RESPONSE_CONNECT, buffer[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ChecksumErrorException))]
        public void ConnectWithChecksumError()
        {
            //Arrange
            this.NetworkService = new MySocketTestConnectChecksumError();

            //Act
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_CONNECT);
            this.NetworkService.Send(frame);

            //Create frame response
            var buffer = new byte[100];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_CONNECT);

            //Assert
            Assert.AreEqual(MetersOperationsConstants.RESPONSE_CONNECT, buffer[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ErrorException))]
        public void ConnectWithError()
        {
            //Arrange
            this.NetworkService = new MySocketTestConnectError();

            //Act
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_CONNECT);
            this.NetworkService.Send(frame);

            //Create frame response
            var buffer = new byte[100];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_CONNECT);

            //Assert
            Assert.AreEqual(MetersOperationsConstants.ERROR, buffer[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(OversizedException))]
        public void ConnectWithOversizedError()
        {
            //Arrange
            this.NetworkService = new MySocketTestConnectOversizedError();

            //Act
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_CONNECT);
            this.NetworkService.Send(frame);

            //Create frame response
            var buffer = new byte[100];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_CONNECT);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFormatException))]
        public void ConnectWithFormatInvalidError()
        {
            //Arrange
            this.NetworkService = new MySocketTestConnectFormatError();

            //Act
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_CONNECT);
            this.NetworkService.Send(frame);

            //Create frame response
            var buffer = new byte[100];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_CONNECT);
        }

        [TestMethod]
        public void GetSerialNumberWithSuccess()
        {
            //Arrange
            this.NetworkService = new MySocketTestGetSerialNumber();

            //Act
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_SERIAL_NUMBER);
            this.NetworkService.Send(frame);

            //Create frame response
            var buffer = new byte[100];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_SERIAL_NUMBER);

            var serialNumber = ConvertUtils.ConvertByteArrayInHexadecimalToText(buffer, sizeReceived);

            //Assert
            Assert.AreEqual("SN-100-200", serialNumber);
        }

    }
}
