using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCPServerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a TCP/IP socket
            TcpListener server = new TcpListener(IPAddress.Any, 12345);
            server.Start();
            Console.WriteLine("Server started, waiting for connection...");

            while (true)
            {
                // Accept an incoming client connection
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                // Get the network stream
                NetworkStream stream = client.GetStream();

                // Buffer to store the incoming data
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // Convert data bytes to string
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + data);

                // Mirror the message back to the client
                byte[] msg = Encoding.ASCII.GetBytes(data);
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: " + data);

                // Close the connection
                client.Close();
            }
        }


    }
}
