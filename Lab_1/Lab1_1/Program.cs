namespace Lab1_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            LetterThread letters = new LetterThread("Letter thread");
            NumThread numbers = new NumThread("Number thread");

            do
            {
                Console.Write(".");
                Thread.Sleep(100);
            } while (letters.newThrd.IsAlive || numbers.newThrd.IsAlive);

            Console.ReadLine();
        }
    }
}
