using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HangmanServer server = new HangmanServer("127.0.0.1", 11000);
            server.Run();
        }
    }
}
