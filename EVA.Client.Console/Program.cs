using EVA.Protocol.Messages.Common;
using EVA.Protocol.Utils;
using System;
using System.Net.Sockets;

namespace EVA.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket chausette = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            chausette.Connect("127.0.0.1", 443);

            while(System.Console.ReadLine() != string.Empty)
            {
                using BigEndianWriter writer = new BigEndianWriter();
                var pingMsg = new PingMessage()
                {
                    PingTime = DateTime.Now
                };
                pingMsg.Serialize(writer);
                var buffer = writer.Data;
                chausette.Send(buffer, buffer.Length, SocketFlags.None);
            }
        }
    }
}
