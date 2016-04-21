using System;
using MetersApplication.Core;
using MetersApplication.ProtocolBase;
using MetersApplication.ProtocolBase.Constants;
using MetersApplication.ProtocolBase.Exceptions;
using MetersApplication.ProtocolBase.Factory;

namespace MetersApplication.Protocol
{
    public abstract class BaseProtocolClient : IDisposable
    {
        private string Ip { get; set; }
        private int Port { get; set; }
        protected IExchangeDataComponent NetworkService { get; set; }
        protected ProtocolValidator Validator { get; set; }

        protected const int BUFFER_DIMENSION = 100;

        protected BaseProtocolClient(IExchangeDataComponent networkComponent, string ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
            this.NetworkService = networkComponent;
            this.NetworkService.Connect(this.Ip, this.Port);
        }

        //I try to generalize this method but i have not time for it
        protected byte[] TransferData(byte operation)
        {
            //Create frame request
            var frame = FrameFactory.Create(operation);
            this.NetworkService.Send(frame);

            //Create buffer to save received data
            var buffer = new byte[BUFFER_DIMENSION];
            int sizeReceived = this.NetworkService.Receive(buffer);

            //Validate response
            try
            {
                this.Validator.ValidateFrame(buffer, sizeReceived, operation);
            }
            catch (OversizedException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
                return null;
            }
            catch (InvalidFormatException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
                return null;
            }
            catch (ErrorException)
            {
                this.NetworkService.Send(frame);
                return null;
            }
            catch (ChecksumErrorException)
            {
                frame = FrameFactory.Create(MetersOperationsConstants.ERROR);
                this.NetworkService.Send(frame);
                return null;
            }

            return buffer;
        }

        public void Dispose()
        {
            this.NetworkService.Dispose();
        }
    }
}
