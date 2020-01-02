using EVA.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace EVA.Server.Common.Network
{
    public class TcpServer : IInitializable, IStartable, IDisposable
    {
        private Socket _socket;
        private IPAddress _iPAddress;
        private readonly List<TcpClient> _clients;
        private readonly int _port;

        public bool IsRunning { get; private set; }

        public TcpServer(string ipAddress, int port)
        {
            IPAddress.TryParse(ipAddress, out _iPAddress);
            _port = port;
            _clients = new List<TcpClient>();
        }

        public void Init()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(_iPAddress, _port));
        }

        public void Start()
        {
            _socket.Listen(0);
            BeginAccept();
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
            _socket.Close();
        }

        private void BeginAccept()
        {
            _socket.BeginAccept(new AsyncCallback(AcceptCallBack), _socket);
        }

        private void AcceptCallBack(IAsyncResult result)
        {
            Socket socket = _socket.EndAccept(result);

            TcpClient client = new TcpClient(socket);
            _clients.Add(client);
            client.Init();

            BeginAccept();
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            IsRunning = false;
            if (!disposedValue)
            {
                if (disposing)
                {
                    _socket.Dispose();
                }

                _iPAddress = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TcpServer()
        {
            Dispose(false);
        }

        #endregion IDisposable Support
    }
}