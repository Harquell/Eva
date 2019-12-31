using System.IO;

namespace EVA.Protocol.Interfaces
{
    public interface IDataReader
    {
        int Position { get; }

        int BytesAvailable { get; }

        short ReadShort();

        int ReadInt();

        long ReadLong();

        ushort ReadUShort();

        uint ReadUInt();

        ulong ReadULong();

        byte ReadByte();

        sbyte ReadSByte();

        byte[] ReadBytes();

        bool ReadBoolean();

        char ReadChar();

        double ReadDouble();

        float ReadFloat();

        string ReadString();

        void Seek(int offset, SeekOrigin seekOrigin);

        void SkipBytes(int n);
    }
}