using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2KIT206
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Researcher researcher1 = new Researcher();

            researcher1.DisplayPositions();
            double t = researcher1.Tenure();

            Console.WriteLine("{0:N4}", t);


        }
    }
}
