using EVA.Common.Interfaces;
using EVA.Protocol.Messages;
using EVA.Protocol.Utils;
using System;
using System.Net.Sockets;
using static EVA.Protocol.Constants;

namespace EVA.Server.Common.Network
{
    public class TcpClient : IInitializable, IDisposable
    {
        private readonly Socket _socket;

        private byte[] _buffer;

        public TcpClient(Socket socket)
        {
            _socket = socket;
        }

        public void Init()
        {
            BeginReceive();
        }

        private void BeginReceive()
        {
            _buffer = new byte[BUFFER_SIZE];
            _socket.BeginReceive(_buffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(ReceiveCallback), this);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            int size = _socket.EndReceive(result);
            byte[] buffer = new byte[size];
            Array.Copy(_buffer, buffer, size);

            using BigEndianReader reader = new BigEndianReader(buffer);
            int packetId = reader.ReadUShort();
            //TODO: Generate message object using packetId
            //TODO: Handle packet

            BeginReceive();
        }

        public void SendMessage(MessageBase message)
        {
            using BigEndianWriter writer = new BigEndianWriter();
            message.Serialize(writer);
            var buffer = writer.Data;
            _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendMessageCallBack), _socket);
        }

        private void SendMessageCallBack(IAsyncResult result)
        {
            try
            {
                Socket socket = (Socket)result.AsyncState;
                socket.EndSend(result);
            }
            catch (Exception)
            {
                _socket.Dispose();
            }
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

                _buffer = null;

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