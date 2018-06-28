using System;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;

namespace StellariumReader
{
    class StellariumReader
    {
        public static void Main(string[] args)
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

            while (true)
            {
                byte[] data = new byte[100];
                int sz = s.Receive(data);
                if (sz == 0)
                {
                    Console.WriteLine("Connection closed");
                    break;
                }
                Console.WriteLine($"Recieved {sz} bytes:");

                string hex = BitConverter.ToString(data, 0, sz).Replace("-", " ");
                Console.WriteLine(hex);

                Trace.Assert(BitConverter.IsLittleEndian == true);


                Int16 len = BitConverter.ToInt16(data, 0);
                Int16 type = BitConverter.ToInt16(data, 2);
                Int64 time = BitConverter.ToInt64(data, 4);
                UInt32 ra = BitConverter.ToUInt32(data, 12);
                Int32 dec = BitConverter.ToInt32(data, 16);

                Console.WriteLine($"len:{len} type:{type} time:{time} ra:{ra} dec:{dec}");

                Trace.Assert(type == 0);
                Trace.Assert(len == sz);
            }

            /* clean up */
            s.Close();
            myList.Stop();
        }
    }
}
