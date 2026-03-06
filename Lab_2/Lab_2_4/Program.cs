namespace Lab_2_4
{
    internal class Program
    {
        static void CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++) result *= i;
            Console.WriteLine($"Method 1 (Factorial {n}): {result}");
        }

        static void CalculateSum(int n)
        {
            int sum = Enumerable.Range(1, n).Sum();
            Console.WriteLine($"Method 2 (Sum of numbers from 1 to {n}): {sum}");
        }

        static void ShowMessageWithPause(string message)
        {
            Thread.Sleep(300);
            Console.WriteLine($"Method 3: {message}");
        }
        static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Parallel.Invoke(
                () => CalculateFactorial(n),
                () => CalculateSum(n),
                () => ShowMessageWithPause("Hi from method 3!")
            );
            Console.WriteLine("All tasks are completed");
        }
    }
}
