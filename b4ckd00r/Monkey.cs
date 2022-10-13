using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalZoo
{
    class Monke
    {
        public static string asGet(string param)
        {
            return "GET / HTTP/1.1\n" +
                "Host: microsoft.com\n\n" +
                param;
        }

    }
}
