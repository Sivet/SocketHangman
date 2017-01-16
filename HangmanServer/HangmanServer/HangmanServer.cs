using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangmanServer
{
    class HangmanServer
    {
        IPAddress ip;
        Socket client;

        int port;
        private volatile bool stop = false;
        private HangmanRepo repo = new HangmanRepo();

        public HangmanServer(string ip, int port)
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
        }

        internal void Run()
        {
            //skaber og starter en listener
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            while (!stop)
            {
                //venter på at der kommer en client
                //når der gør acceptere den
                client = listener.AcceptSocket();

                //starter en thread til den nye client og sender acceptet fra listeneren med som en socket
                ClientHandler ch = new ClientHandler(client, repo.GetRandomWord());
                new Thread(ch.Run).Start();
            }
        }
    }
}
