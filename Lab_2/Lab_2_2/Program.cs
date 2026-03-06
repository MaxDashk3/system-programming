using System;
using System.Threading.Tasks;

namespace Lab_2_2
{
    class Program
    {
        static void NumberCounter()
        {
            Console.WriteLine($"Counter N{Task.CurrentId} started");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(200);
                Console.WriteLine($"Counter N{Task.CurrentId} at {i}");
            }
        }
        static void Main(string[] args) {
            Task tsk1 = new Task(NumberCounter);
            Task tsk2 = new Task(NumberCounter);
            Task tsk3 = new Task(NumberCounter);

            tsk1.Start();
            tsk2.Start();
            tsk3.Start();

            Console.WriteLine($"Id of task tsk1 is {tsk1.Id}");
            Console.WriteLine($"Id of task tsk2 is {tsk2.Id}");
            Console.WriteLine($"Id of task tsk3 is {tsk3.Id}");

            Task.WaitAll(tsk1, tsk2, tsk3);
            Console.WriteLine("All tasks are completed");
        }
    }
}
