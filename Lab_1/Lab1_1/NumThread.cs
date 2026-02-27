namespace Lab1_1
{
    class NumThread
    {
        public int Count;
        string thrdName;
        public Thread newThrd;

        public NumThread(string name)
        {
            Count = 0;
            thrdName = name;
            newThrd = new Thread(this.Run);
            newThrd.Name = name;
            //Почати виконання потоку
            newThrd.Start();
        }

        public void Run()
        {
            Console.WriteLine(thrdName + " starting.");
            do
            {
                // Призупинення даного потоку
                Thread.Sleep(200);
                Console.WriteLine("In the thread " + thrdName + ", Count=" + Count);
                Count++;
            } while (Count < 40);
            Console.WriteLine(thrdName + " is completed.");
        }
    }
}