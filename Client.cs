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
                    Console.WriteLine("5: SEND_MESSAGE");
                    Console.WriteLine("6: EXIT");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": // LIST_FILES
                            writer.WriteLine("LIST_FILES");
                            Console.WriteLine("Files on server:");
                            string response;
                            while ((response = reader.ReadLine()) != "END_OF_LIST")
                            {
                                Console.WriteLine(response);
                            }
                            break;

                        case "2": // READ_FILE
                            Console.Write("Enter the file name to read: ");
                            string fileNameToRead = Console.ReadLine();
                            writer.WriteLine("READ_FILE " + fileNameToRead);

                            string fileContent = reader.ReadLine();
                            Console.WriteLine("File content: " + fileContent);
                            break;

                        case "3": // WRITE_FILE
                            Console.Write("Enter the file name to write: ");
                            string fileNameToWrite = Console.ReadLine();
                            Console.Write("Enter the content to write: ");
                            string contentToWrite = Console.ReadLine();
                            writer.WriteLine($"WRITE_FILE {fileNameToWrite} {contentToWrite}");

                            string writeResponse = reader.ReadLine();
                            Console.WriteLine("Server response: " + writeResponse);
                            break;

                        case "4": // EXECUTE
                            Console.Write("Enter the command to execute: ");
                            string command = Console.ReadLine();
                            writer.WriteLine($"EXECUTE {command}");

                            Console.WriteLine("Command output:");
                            string commandOutput;
                            while ((commandOutput = reader.ReadLine()) != null && commandOutput != "END_OF_COMMAND")
                            {
                                Console.WriteLine(commandOutput);
                            }
                            break;

                        case "5": // SEND_MESSAGE
                            Console.Write("Enter the message to send to the server: ");
                            string message = Console.ReadLine();
                            writer.WriteLine($"MESSAGE {message}");

                            string serverReply = reader.ReadLine();
                            Console.WriteLine("Server reply: " + serverReply);
                            break;

                        case "6": // EXIT
                            Console.WriteLine("Exiting client...");
                            running = false;
                            writer.WriteLine("EXIT");
                            break;

