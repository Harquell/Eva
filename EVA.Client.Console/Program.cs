using EVA.Client.Managers;
using EVA.Client.Network;
using EVA.Common.Attributes;
using EVA.Common.Utils;
using EVA.Protocol.Messages;
using EVA.Protocol.Messages.Common;
using EVA.Protocol.Utils;
using System;
using System.Threading.Tasks;

namespace EVA.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageManager.Instance.Init();
            TcpClient chaussette = new TcpClient("127.0.0.1", 443);
            chaussette.Init();
            chaussette.Start();
            string username = string.Empty;

            while(true)
            {
                string text = System.Console.ReadLine();
                if (text == string.Empty)
                    continue;   
                if(text == "exit")
                {
                    chaussette.Stop();
                    Environment.Exit(0);
                }
                else
                {
                    if(text.Split(' ')[0] == "username" && text.Split(' ').Length >= 2)
                    {
                        username = text.Split(' ')[1];
                    }
                    else
                    {
                        Task.Run(() =>
                        {

                            while (true)
                            {
                                ChatMessage msg = new ChatMessage
                                {
                                    Auteur = username,
                                    Message = text,
                                    SentAt = DateTime.Now
                                };
                                chaussette.SendMessage(msg);
                            }
                        });
                    }
                }
            }
        }

        [MessageHandler(3)]
        public static void HandleChatMessage(MessageBase msg)
        {
            ChatMessage cmsg = (ChatMessage)msg;
            System.Console.WriteLine($"[{cmsg.SentAt}] {cmsg.Auteur} => {cmsg.Message}");
        }
    }
}
