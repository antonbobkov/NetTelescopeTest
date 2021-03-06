﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerByteReader
{
    class ServerByteReader
    {
        public static void Main(string[] args)
        {
            try
            {
                String ip = "127.0.0.1";
                if (args.Length > 0)
                {
                    ip = args[0];
                    Console.WriteLine("Using command line ip address: " + ip);
                }
                else
                {
                    Console.WriteLine("Using default ip address: " + ip);
                }

                IPAddress ipAd = IPAddress.Parse(ip);
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, 8001);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is: " +
                                  myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                while (true) {
                    byte[] b = new byte[100];
                    int k = s.Receive(b);
                    Console.WriteLine($"Recieved {k} bytes:");

                    string hex = BitConverter.ToString(b).Replace("-", " ");
                    Console.WriteLine(hex);
                }

                ///* clean up */
                //s.Close();
                //myList.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
