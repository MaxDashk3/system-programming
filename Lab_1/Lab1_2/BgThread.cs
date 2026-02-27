namespace Lab1_2
{
    class BgThread
    {
        public void Run()
        {
            while (true)
            {
                Console.WriteLine(".");
                Thread.Sleep(10);
            }
        }
    }
}
