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
                }else
                    {
                    writer.WriteLine("Directory not found.");
                    }
            }
            else if (request.StartsWith("READ_FILE"))
            {
                string filePath = Path.Combine(serverDirectory, request.Split(' ')[1]);
                if (File.Exists(filePath))
                {
                    writer.WriteLine(File.ReadAllText(filePath));
                }
                else
                {
                    writer.WriteLine("File not found.");
                }
            }
            else if (request.StartsWith("WRITE_FILE") && hasWriteExecutePrivileges)
            {
                string[] parts = request.Split(new[] { ' ' }, 3);
                string fileName = parts[1];
                string fileContent = parts[2];

                string filePath = Path.Combine(serverDirectory, fileName);

                try
                {
                    File.AppendAllText(filePath, fileContent + Environment.NewLine);
                    writer.WriteLine("Content was written to the file successfully.");
                }
                catch (Exception ex)
                {
                    writer.WriteLine("Failed to write to file: " + ex.Message);
                }
            }
            else if (request.StartsWith("EXECUTE") && hasWriteExecutePrivileges)
            {
                string command = request.Split(' ')[1];

                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(processInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    writer.WriteLine(output);
                }
            }
            else
            {
                writer.WriteLine("Permission denied or invalid command.");
            }
        }
        Console.WriteLine($"Client ({clientName}) disconnected.");
        client.Close();
    }

    private static bool CheckPrivileges(string clientIP)
    {
        return clientIP == "172.20.10.2";
    }
}
