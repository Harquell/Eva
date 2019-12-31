using System.IO;

namespace EVA.Protocol.Interfaces
{
    public interface IDataWriter
    {
        byte[] Data { get; }
        int Position { get; }

        void WriteShort(short value);

        void WriteInt(int value);

        void WriteLong(long value);

        void WriteUShort(ushort value);

        void WriteUInt(uint value);

        void WriteULong(ulong value);

        void WriteByte(byte value);

        void WriteSByte(sbyte value);

        void WriteFloat(float value);

        void WriteBoolean(bool value);

        void WriteChar(char value);

        void WriteDouble(double value);

        void WriteSingle(float value);

        void WriteString(string value);

        void WriteBytes(byte[] data);

        void Clear();

        void Seek(int offset, SeekOrigin seekOrigin);
    }
}