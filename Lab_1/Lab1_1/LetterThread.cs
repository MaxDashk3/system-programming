namespace Lab1_1
{
    class LetterThread
    {
        public char Letter;
        string thrdName;
        public Thread newThrd;
        public LetterThread(string name)
        {
            Letter = 'A';
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
                Thread.Sleep(300);
                Console.WriteLine("In the thread " + thrdName + ", Letter=" + Letter);
                Letter++;
            } while (Letter < 'Z');
            Console.WriteLine(thrdName + " is completed.");
        }
    }
}