using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeInOcean
{
    class Program
    {
        static void Main(string[] args)
        {
            Ocean myOcean = new Ocean();
      //      myOcean = Cell._o;
            myOcean.initialize ();
            int iternum = 250;
            myOcean.Run ( iternum );
           

            Console.ReadKey();
         
        }
    }
}
