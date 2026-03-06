namespace Lab_2_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task numberCounter = new Task(Counters.NumberCounter);
            Task letterCounter = new Task(Counters.LetterCounter);

            numberCounter.Start();
            letterCounter.Start();

            Task.WaitAll(numberCounter, letterCounter);
            Console.WriteLine("All tasks are completed");
        }
    }
}
