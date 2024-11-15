using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

class Server
{
    private static TcpListener listener;
    private static string serverDirectory = @"C:\SharedFolder";

    static void Main(string[] args)
    {
        int port = 8081;
        IPAddress localAddress = IPAddress.Any; //Parse("192.168.0.20")

        listener = new TcpListener(localAddress, port);
        listener.Start();
        Console.WriteLine("Server started on " + localAddress + ":" + port);

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }

    private static void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };

        // Read client name
        string clientName = reader.ReadLine();

        string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
        bool hasWriteExecutePrivileges = CheckPrivileges(clientIP);
        Console.WriteLine($"Client ({clientName}) connected with IP: {clientIP}");
        // Debug log to check if privileges match the IP
        if (clientIP == "192.168.1.166")
        {
            Console.WriteLine("Debug: Privileged client IP detected.");
        }
        else
        {
            Console.WriteLine("Debug: Non-privileged client IP detected.");
        }
        while (true)
        {
            string request = reader.ReadLine();
            if (request == null || request == "EXIT") break;

            Console.WriteLine($"Received from {clientName}: {request}");

            if (request.StartsWith("LIST_FILES"))
            {
                if (Directory.Exists(serverDirectory))
                {
                    string[] files = Directory.GetFiles(serverDirectory);
                    foreach (var file in files)
                    {
                        writer.WriteLine(Path.GetFileName(file)); // Send only the file name
                    }
                    writer.WriteLine("END_OF_LIST");
                }