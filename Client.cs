using System;
using System.IO;
using System.Net.Sockets;

class Client
{
    static void Main(string[] args)
    {
        string serverIp = "10.180.35.232"; 
        int port = 8082;
        try
        {
            using (TcpClient client = new TcpClient(serverIp, port))
            using (NetworkStream stream = client.GetStream())
            using (StreamWriter writer = new StreamWriter(stream) { AutoFlush = true })
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.Write("Enter your name: ");
                string clientName = Console.ReadLine();

                // Send the client name to the server
                writer.WriteLine(clientName);
                Console.WriteLine("Connected to server!");

                bool running = true;
                while (running)
                {
                    Console.WriteLine("\nChoose an operation:");
                    Console.WriteLine("1: LIST_FILES");
                    Console.WriteLine("2: READ_FILE");
                    Console.WriteLine("3: WRITE_FILE");
                    Console.WriteLine("4: EXECUTE");
                    Console.WriteLine("5: EXIT");
                    string choice = Console.ReadLine();