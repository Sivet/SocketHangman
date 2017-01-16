using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HangmanServer
{
    class ClientHandler
    {
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        
        private Socket client;
        private char[] word;

        public ClientHandler(Socket client, string word)
        {
            this.client = client;
            this.word = word.ToCharArray();
        }

        internal void Run()
        {
            //skaber en stream mellem clienten og serveren
            stream = new NetworkStream(client);
            //tilføjer en reader til den stream
            reader = new StreamReader(stream);
            //tilføjer en writer til den stream
            writer = new StreamWriter(stream);


            DoChat();

            writer.Close();
            reader.Close();
            stream.Close();
            client.Close();
        }
        private void DoChat()
        {
            try
            {
                writer.WriteLine("Ready, you word is " + word.Length + " long");
                writer.Flush();

                //kører metoden i et while loop så længe metoden returner true
                while (Commands());
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        private bool Commands()
        {
            //sætter det input jeg får fra clienter til en string
            char input = recieveFromClient();
            
            
            //tjekker inputtet op imod det valgte ord. Og sender svar tilbage til klienten
            BroadCast(CheckWord(input));

            return true;
        }
        private char recieveFromClient()
        {
            try
            {
                string temp = reader.ReadLine();
                if (temp.Length == 1)
                {
                    return temp.ToCharArray()[0];
                }
                return ' ';
            }
            catch
            {
                return ' ';
            }
        }
        private List<int> CheckWord(char input)
        {
            List<int> correctSpots = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == input)
                {
                    correctSpots.Add(i);
                }
            }
            return correctSpots;
        }
        private void BroadCast(List<int> corrects)
        {
            string temp = "The word contains your letter on the spots number: ";
            foreach (int item in corrects)
            {
                int place = item + 1;
                temp += place.ToString() + ", ";
            }
            writer.WriteLine(temp);
            writer.Flush();

        }
    }
}
