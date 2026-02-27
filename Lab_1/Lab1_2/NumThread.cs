namespace Lab1_2
{
    class NumThread
    {
        public int Count;
        string thrdName;
        int target;
        int delay;

        public NumThread(string name, int target, int delay)
        {
            Count = 0;
            thrdName = name;
            this.target = target;
            this.delay = delay;
        }

        public void Run()
        {
            Console.WriteLine(thrdName + " starting.");
            do
            {
                // Призупинення даного потоку
                Thread.Sleep(delay);
                Console.WriteLine("In the thread " + thrdName + ", Count=" + Count);
                Count++;
            } while (Count < target);
            Console.WriteLine(thrdName + " is completed.");
        }
    }
}