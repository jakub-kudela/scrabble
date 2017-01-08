using System;

namespace ScrabbleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Settings.welcomeMessage);
            Console.WriteLine(Settings.portRequestMessage);

            int port;
            while (!Int32.TryParse(Console.ReadLine(), out port))
	        {
                Console.WriteLine(Settings.portRequestMessage);
	        }

            Server server;
            if (port == 0)
            {
                server = new Server();
            }
            else
            {
                server = new Server(port);
            }

            string command;
            while (true)
            {
                command = Console.ReadLine();
                if (command == Settings.startCommandKeyword)
                {
                    server.Start();
                }
                if (command == Settings.beginCommandKeyword)
                {
                    server.BeginGame();
                }
                else if (command == Settings.stopCommandKeyword)
                {
                    server.Stop();
                    return;
                }
            }
        }
    }
}
