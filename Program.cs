using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace TcpListener
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var server = new System.Net.Sockets.TcpListener(IPAddress.Parse("0.0.0.0"), 49025);
            //TcpListener server = new TcpListener(IPAddress.Parse("0.0.0.0"), 66);
            server.Start();
            Console.WriteLine("Started.");
            while (true)
            {
                var cw = new ClientWorking(server.AcceptTcpClient());
                new Thread(new ThreadStart(cw.DoSomethingWithClient)).Start();
            }
            server.Stop();
        }
    }

    class ClientWorking
    {
        private readonly Stream _clientStream;
        private TcpClient _client;

        public ClientWorking(TcpClient client)
        {
            Console.WriteLine("Inside ClientWorking");
            this._client = client;
            _clientStream = client.GetStream();
            
        }

        public void DoSomethingWithClient()
        {
            //var sr = new StreamReader(_clientStream);
            //var s = new stre
            //var sr = new StreamReader(sw.BaseStream);
            //Console.WriteLine(sw.ToString());
            //sw.WriteLine("Hi. This is x2 TCP/IP easy-to-use server");
            //sw.Flush();
            while (_client.Connected)
            {
                try
                {
                    byte[] data = new byte[1024];
                    if (_clientStream.CanRead )
                    {
                        Console.WriteLine("Reading stream");

                        int bytesRead = _clientStream.Read(data, 0, (int)data.Length);
                            string hex = BitConverter.ToString(data);
                            Console.WriteLine(hex.Replace("-00", ""));
                        _client.Client.Send(data, bytesRead,SocketFlags.None);


                    }
                    //string data;
                   
                   
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    //sw.Close();
                }
    
            }
            
          
        }
    }
}