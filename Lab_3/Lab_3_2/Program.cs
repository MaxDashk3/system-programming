using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] sizes = { 100000, 1000000, 10000000 };

        Console.WriteLine(new string('-', 90));
        Console.WriteLine($"| {"Type",-7} | {"Size",-10} | {"Operation",-18} | {"For, ms",-10} | {"Parallel, ms",-12} | {"Parr. Status",-12} |");
        Console.WriteLine(new string('-', 90));

        foreach (var size in sizes) RunExperiment<int>("int", size);
        Console.WriteLine(new string('-', 90));
        foreach (var size in sizes) RunExperiment<double>("double", size);

        Console.WriteLine(new string('-', 90));
    }
    static void RunExperiment<T>(string type, int size)
    {
        T[] num_list = new T[size];
        for (int i = 0; i < size; i++)
            num_list[i] = (T)Convert.ChangeType(i, typeof(T));

        Calculate(type, size, "x = x / 10", (i) => {
            var x = Convert.ToDouble(num_list[i]);
            return x / 10;
        });

        Calculate(type, size, "x = x / PI", (i) => {
            var x = Convert.ToDouble(num_list[i]);
            return x / Math.PI;
        });

        Calculate(type, size, "x = e^x / x^PI", (i) => {
            var x = Convert.ToDouble(num_list[i]);
            return Math.Exp(x) / Math.Pow(x, Math.PI);
        });

        Calculate(type, size, "x = e^PIx / x^PI", (i) => {
            var x = Convert.ToDouble(num_list[i]);
            return Math.Exp(Math.PI * i) / Math.Pow(i, Math.PI);
        });
    }

    static void Calculate(string type, int size, string label, Func<int, double> operation)
    {
        // число та окіл
        const double target = 500.0;    
        const double epsilon = 0.001;

        Stopwatch sw = new Stopwatch();

        sw.Start();
        for (int i = 0; i < size; i++)
        {
            operation(i);
        }
        sw.Stop();
        long forTime = sw.ElapsedMilliseconds;

        sw.Restart();
        var result = Parallel.For(0, size, (i, state) =>
        {
            if (Math.Abs(operation(i) - target) < epsilon)
            {
                state.Stop();
            }
        });

        string status = result.IsCompleted? "Completed": "Interrupted";

        sw.Stop();
        long parallelTime = sw.ElapsedMilliseconds;

        Console.WriteLine($"| {type,-7} | {size,-10} | {label,-18} | {forTime,-10} | {parallelTime,-12} | {status, -12} |");
    }
}