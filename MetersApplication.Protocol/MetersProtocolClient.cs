using System;
using MetersApplication.Core;
using MetersApplication.ProtocolBase;
using MetersApplication.ProtocolBase.Constants;
using MetersApplication.ProtocolBase.Exceptions;
using MetersApplication.ProtocolBase.Factory;
using MetersApplication.Utils;

namespace MetersApplication.Protocol
{
    public class MetersProtocolClient : IMetersOperation
    {
        private string Ip { get; set; }
        private int Port { get; set; }
        private IExchangeDataComponent NetworkService { get; set; }
        private ProtocolValidator Validator { get; set; }

        private const int BUFFER_DIMENSION = 100;

        public MetersProtocolClient(IExchangeDataComponent networkService, string ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
            this.NetworkService = networkService;
        }

        public void Connect()
        {
            //Create socket connection to exchange data between client and server
            this.NetworkService.Connect(this.Ip, this.Port);

            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_CONNECT);
            this.NetworkService.Send(frame);

            //Create frame response
            var buffer = new byte[BUFFER_DIMENSION];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            try
            {
                this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_CONNECT);
            }
            catch (OversizedException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (InvalidFormatException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (ErrorException)
            {
                this.NetworkService.Send(frame);
            }
            catch (ChecksumErrorException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
        }

        public void Disconnect()
        {
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_DISCONNECT);
            this.NetworkService.Send(frame);

            //Create buffer to save received data
            var buffer = new byte[BUFFER_DIMENSION];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            try
            {
                this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_DISCONNECT);
            }
            catch (OversizedException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (InvalidFormatException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (ErrorException)
            {
                this.NetworkService.Send(frame);
            }
            catch (ChecksumErrorException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }

            //Switch off socket connection 
            this.NetworkService.Disconnect();
        }

        public string ReadSerialNumber()
        {
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_SERIAL_NUMBER);
            this.NetworkService.Send(frame);

            //Create buffer to save received data
            var buffer = new byte[BUFFER_DIMENSION];
            var sizeReceived = this.NetworkService.Receive(buffer);
            var dataSize = ConvertUtils.ConvertByteInHexadecimalToInt(buffer[2]);

            //Validate response
            try
            {
                this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_SERIAL_NUMBER);
            }
            catch (OversizedException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (InvalidFormatException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (ErrorException)
            {
                this.NetworkService.Send(frame);
            }
            catch (ChecksumErrorException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }

            return ConvertUtils.ConvertByteArrayInHexadecimalToText(buffer, dataSize);
        }

        public DateTime ReadDateTime()
        {
            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_DATE_AND_TIME);
            this.NetworkService.Send(frame);

            //Create buffer to save received data
            var buffer = new byte[BUFFER_DIMENSION];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            try
            {
                this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_DATE_AND_TIME);
            }
            catch (OversizedException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (InvalidFormatException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (ErrorException)
            {
                this.NetworkService.Send(frame);
            }
            catch (ChecksumErrorException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }

            return ConvertUtils.ConvertByteArrayInHexadecimalToDateTime(buffer);
        }

        public double ReadEnergyValue(DateTime dateTime)
        {
            //Convert DateTime in Hex (byte[])
            var dateTimeInHexadecimal = ConvertUtils.ConvertDateTimeToByteArrayInHexadecimal(dateTime);

            //Create frame request
            var frame = FrameFactory.Create(MetersOperationsConstants.REQUEST_READ_ENERGY_VALUE, dateTimeInHexadecimal);
            this.NetworkService.Send(frame);

            //Create buffer to save received data
            var buffer = new byte[BUFFER_DIMENSION];
            var sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            try
            {
                this.Validator.ValidateFrame(buffer, sizeReceived, MetersOperationsConstants.RESPONSE_READ_ENERGY_VALUE);
            }
            catch (OversizedException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (InvalidFormatException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }
            catch (ErrorException)
            {
                this.NetworkService.Send(frame);
            }
            catch (ChecksumErrorException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
            }

            return ConvertUtils.ConvertByteArrayInHexadecimalToDouble(buffer);
        }

        public void Dispose()
        {
            this.NetworkService.Dispose();
        }
    }
}
