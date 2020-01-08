using EVA.Protocol.Interfaces;
using System;
using System.IO;
using System.Text;

namespace EVA.Protocol.Utils
{
    public class BigEndianWriter : IDataWriter, IDisposable
    {
        private BinaryWriter _writer;
        private Stream _stream => _writer?.BaseStream;

        public byte[] Data
        {
            get
            {
                try
                {
                    var stream = _stream as MemoryStream;
                    var array = stream.ToArray();
                    return array;
                }
                catch (Exception)
                {
                    //TODO: log exception
                    return new byte[0];
                }
            }
        }

        public int Position => Convert.ToInt32(_stream.Position);

        public BigEndianWriter()
        {
            Clear();
        }

        public void WriteShort(short value)
            => _writer.Write(value);

        public void WriteInt(int value)
            => _writer.Write(value);

        public void WriteLong(long value)
            => _writer.Write(value);

        public void WriteUShort(ushort value)
            => _writer.Write(value);

        public void WriteUInt(uint value)
            => _writer.Write(value);

        public void WriteULong(ulong value)
            => WriteBytes(BitConverter.GetBytes(value));

        public void WriteByte(byte value)
            => _writer.Write(value);

        public void WriteSByte(sbyte value)
            => _writer.Write(value);

        public void WriteFloat(float value)
            => _writer.Write(value);

        public void WriteBoolean(bool value)
            => _writer.Write(value);

        public void WriteChar(char value)
            => _writer.Write(value);

        public void WriteDouble(double value)
            => _writer.Write(value);

        public void WriteSingle(float value)
            => _writer.Write(value);

        public void WriteString(string value)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            _writer.Write(buffer.Length);
            _writer.Write(buffer);
        }

        public void WriteBytes(byte[] data)
        {
            _writer.Write(data.Length);
            _writer.Write(data);
        }

        public void Clear()
            => _writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);

        public void Seek(int offset, SeekOrigin seekOrigin)
            => _stream.Seek(offset, seekOrigin);

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _writer.Flush();
                    _writer.Dispose();
                    _writer = null;
                }
                disposedValue = true;
            }
        }

        ~BigEndianWriter()
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