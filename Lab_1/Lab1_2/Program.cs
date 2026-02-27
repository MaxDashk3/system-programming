namespace Lab1_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            NumThread numbers1 = new NumThread("Thread 1", 40, 80);
            NumThread numbers2 = new NumThread("Thread 2", 50, 70);
            BgThread bg = new BgThread();

            var bgThread = new Thread(bg.Run);
            bgThread.IsBackground = true;
            var numThread1 = new Thread(numbers1.Run);
            var numThread2 = new Thread(numbers2.Run);

            bgThread.Start();
            numThread1.Start();
            numThread2.Start();
        }
    }
}
