using System.Diagnostics;
using System.Threading;
namespace Lab1_3
{
    internal class Program
    {
        static List<NumThread> threads = new List<NumThread>();
        static void GenerateThreads(List<ThreadPriority> priorityLevels)
        {
            for (int i = 0; i < priorityLevels.Count; i++)
            {
                NumThread numThread = new NumThread($"Thread {i+1}; Priority: {priorityLevels[i]}");
                numThread.Thrd.Priority = priorityLevels[i];

                threads.Add(numThread);
                numThread.Thrd.Start();
            }

            foreach (var thread in threads)
            {
                thread.Thrd.Join();
            }

            Console.WriteLine("All threads have completed.");

            double totalCount = threads.Sum(t => t.Count);

            Console.WriteLine("Stats:");
            foreach (var thread in threads)
            {
                double cpuTime = (double)thread.Count * 100 / totalCount;
                Console.WriteLine($"{thread.Thrd.Name} - Count: {thread.Count}; {cpuTime:F2}% CPU time");
            }
        }

        static void Main(string[] args)
        {
            GenerateThreads(new List<ThreadPriority> {
                ThreadPriority.Normal,
                ThreadPriority.AboveNormal,
                ThreadPriority.Highest
            });

            //GenerateThreads(new List<ThreadPriority> {
            //    ThreadPriority.Highest,
            //    ThreadPriority.Lowest,
            //    ThreadPriority.AboveNormal,
            //    ThreadPriority.Normal,
            //    ThreadPriority.BelowNormal
            //});

            //GenerateThreads(new List<ThreadPriority> {
            //    ThreadPriority.Lowest,
            //    ThreadPriority.AboveNormal,
            //    ThreadPriority.BelowNormal,
            //    ThreadPriority.Highest
            //});

            //GenerateThreads(new List<ThreadPriority> {
            //    ThreadPriority.Normal,
            //    ThreadPriority.BelowNormal,
            //    ThreadPriority.AboveNormal,
            //    ThreadPriority.Highest,
            //    ThreadPriority.Lowest,
            //    ThreadPriority.Normal
            //});
        }
    }
}
