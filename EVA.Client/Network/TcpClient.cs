using EVA.Client.Managers;
using EVA.Common.Interfaces;
using EVA.Common.Utils;
using EVA.Protocol.Interfaces;
using EVA.Protocol.Messages;
using EVA.Protocol.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using static EVA.Protocol.Constants;

namespace EVA.Client.Network
{
    public class TcpClient : IInitializable, IStartable, IDisposable
    {
        private readonly EndPoint _serverEndpoint;
        public EndPoint EndPoint => _serverEndpoint;
        private Socket _socket;
        private byte[] _buffer;

        public TcpClient(string ip, int port)
        {
            IPAddress.TryParse(ip, out IPAddress address);
            _serverEndpoint = new IPEndPoint(address, port);
        }

        public void Init()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            _socket.Connect(_serverEndpoint);
            BeginReceive();
        }

        public void SendMessage(MessageBase message)
        {
            try
            {
                using IDataWriter writer = new BigEndianWriter();
                message.Serialize(writer);
                byte[] buffer = writer.Data;
                _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendMessageCallback), _socket);

                Logger.Debug(string.Format("Sending {0} message", message.GetType()));
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void SendMessageCallback(IAsyncResult result)
        {
            try
            {
                _socket.EndSend(result);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Stop();
            }
        }

        private void BeginReceive()
        {
            _buffer = new byte[BUFFER_SIZE];
            _socket.BeginReceive(_buffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), this);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int size = _socket.EndReceive(result);
                byte[] buffer = new byte[size];
                Array.Copy(_buffer, buffer, size);

                MessageManager.Instance.HandleMessage(buffer, this);

                BeginReceive();
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        public void Stop()
        {
            
            _socket.Disconnect(true);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _socket.Dispose();
                }

                disposedValue = true;
            }
        }

        ~TcpClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}