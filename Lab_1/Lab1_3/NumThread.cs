namespace Lab1_3
{
    internal class NumThread
    {
        public int Count;
        public Thread Thrd;
        static bool stop = false;

        public NumThread(string name)
        {
            Count = 0;
            Thrd = new Thread(Run);
            Thrd.Name = name;
        }

        void Run()
        {
            Console.WriteLine("Thread " + Thrd.Name + " is beginning.");
            do
            {
                Count++;

            } while (stop == false && Count < 1e8);
            stop = true;
            Console.WriteLine("Thread " + Thrd.Name + " is completed.");
        }
    }
}
