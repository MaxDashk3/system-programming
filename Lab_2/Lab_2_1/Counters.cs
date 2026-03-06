using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_1
{
    class Counters
    {
        public static void NumberCounter()
        {
            Console.WriteLine("Number counter started");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Console.WriteLine($"Number counter at {i}");
            }
        }
        public static void LetterCounter()
        {
            Console.WriteLine("Letter counter started");
            for (char i = 'A'; i < 'J'; i++)
            {
                Thread.Sleep(200);
                Console.WriteLine($"Letter counter at "+i);
            }
        }
    }
}
