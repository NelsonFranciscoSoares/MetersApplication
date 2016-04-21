using System;
using System.Net.Sockets;
using MetersApplication.Core;
using NS_Socket = System.Net.Sockets;

namespace MetersApplication.Socket
{
    public class SocketTcpIp : IExchangeDataComponent
    {
        private NS_Socket.Socket Socket { get; set; }

        public SocketTcpIp()
        {
            this.Socket = new NS_Socket.Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {
            this.Socket.Connect(ip, port);
        }

        public void Disconnect()
        {
            this.Socket.Disconnect(false);
        }

        public void Send(byte[] frame)
        {
            this.Socket.Send(frame);
        }

        public int Receive(byte[] frame)
        {
            return this.Socket.Receive(frame);
        }

        public void Dispose()
        {
            this.Socket.Dispose();
        }
    }
}
