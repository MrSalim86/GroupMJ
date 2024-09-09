using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClintApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Connect to the server at the specified IP and port
                TcpClient client = new TcpClient("4.tcp.eu.ngrok.io", 12215); // Replace SERVER_IP_ADDRESS with the actual server IP
                Console.WriteLine("Connected to server");

                // Get the network stream
                NetworkStream stream = client.GetStream();

                // Read message from the console
                Console.Write("Enter message to send: ");
                string message = Console.ReadLine();

                // Send the message to the server
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: " + message);

                // Receive the mirrored message from the server
                data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string responseData = Encoding.ASCII.GetString(data, 0, bytesRead);
                Console.WriteLine("Received: " + responseData);

                // Close everything
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
