using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLines
{
    class Program
    {
        static void RunCommand(string cmd) {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + cmd;
            process.StartInfo = startInfo;
            process.Start();
        }

        static void Main(string[] args)
        {
            if (args[0] == "hon")
                RunCommand("netsh wlan start hostednetwork");
            else if (args[0] == "hoff")
                RunCommand("netsh wlan stop hostednetwork");
            else if (args[0] == "won")
                RunCommand("netsh interface set interface \"Wi-Fi\" enabled");
            else if (args[0] == "woff")
                RunCommand("netsh interface set interface \"Wi-Fi\" disabled");
            else
                System.Console.WriteLine("Unknown command " + args[0] + "\n");
        }
    }
}
