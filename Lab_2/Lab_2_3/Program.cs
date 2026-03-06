namespace Lab_2_3
{
    class Program
    {
        static int SumIt(object v)
        {
            int x = (int)v;
            int sum = 0;
            for (; x > 0; x--)
                sum += x;
            return sum;
        }
        static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            var sumTask = new Task<int>(SumIt, Convert.ToInt32(input));
            var continuation = sumTask.ContinueWith(task =>
            {
                Console.WriteLine("Result: " + task.Result);
            });

            sumTask.Start();
        }
    }
}