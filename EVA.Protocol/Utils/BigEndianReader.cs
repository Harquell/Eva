using EVA.Protocol.Interfaces;
using System;
using System.IO;
using System.Text;

namespace EVA.Protocol.Utils
{
    public class BigEndianReader : IDataReader, IDisposable
    {
        public BigEndianReader(byte[] buffer)
        {
            _reader = new BinaryReader(new MemoryStream(buffer));
        }

        private BinaryReader _reader;
        private Stream _stream => _reader?.BaseStream;

        public int Position => Convert.ToInt32(_stream.Position);

        public int BytesAvailable => Convert.ToInt32(_stream.Length - _stream.Position);

        public short ReadShort()
            => _reader.ReadInt16();

        public int ReadInt()
            => _reader.ReadInt32();

        public long ReadLong()
            => _reader.ReadInt64();

        public ushort ReadUShort()
            => _reader.ReadUInt16();

        public uint ReadUInt()
            => _reader.ReadUInt32();

        public ulong ReadULong()
            => _reader.ReadUInt64();

        public byte ReadByte()
            => _reader.ReadByte();

        public sbyte ReadSByte()
            => (sbyte)_reader.ReadByte();

        public byte[] ReadBytes()
        {
            int size = _reader.ReadInt32();
            return _reader.ReadBytes(size);
        }

        public bool ReadBoolean()
            => _reader.ReadBoolean();

        public char ReadChar()
            => _reader.ReadChar();

        public double ReadDouble()
            => _reader.ReadDouble();

        public float ReadFloat()
            => _reader.ReadSingle();

        public string ReadString()
        {
            int size = _reader.ReadInt32();
            byte[] buffer = _reader.ReadBytes(size);
            return Encoding.UTF8.GetString(buffer);
        }

        public void Seek(int offset, SeekOrigin seekOrigin)
            => _stream.Seek(offset, seekOrigin);

        public void SkipBytes(int n)
            => _reader.ReadBytes(n);

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _reader.Dispose();
                    _reader = null;
                }

                disposedValue = true;
            }
        }

        ~BigEndianReader()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
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