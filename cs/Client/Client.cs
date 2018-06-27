﻿
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;


public class Client
{

    public static void Main(string[] args)
    {

        try
        {
            String ip = "192.168.0.114";
            if (args.Length > 0)
            {
                ip = args[0];
                Console.WriteLine("Using command line ip address: " + ip);
            }
            else
            {
                Console.WriteLine("Using default ip address: " + ip);
            }

            TcpClient tcpclnt = new TcpClient();
            Console.WriteLine("Connecting....." + ip);

            tcpclnt.Connect(ip, 8001);
            // use the ipaddress as in the server program

            Console.WriteLine("Connected");
            Console.Write("Enter the string to be transmitted : ");

            String str = Console.ReadLine();
            Stream stm = tcpclnt.GetStream();

            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(str);
            Console.WriteLine("Transmitting.....");

            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);

            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(bb[i]));

            tcpclnt.Close();
        }

        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
        }
    }

}