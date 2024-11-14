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