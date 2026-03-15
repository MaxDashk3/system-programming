using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] sizes = { 100000, 1000000, 10000000 };

        Console.WriteLine(new string('-', 78));
        Console.WriteLine($"| {"Type",-7} | {"Size",-10} | {"Operation",-18} | {"For, ms",-10} | {"Parallel, ms",-12} |");
        Console.WriteLine(new string('-', 78));

        foreach (var size in sizes) RunExperiment<int>("int", size);
        Console.WriteLine(new string('-', 78));
        foreach (var size in sizes) RunExperiment<double>("double", size);

        Console.WriteLine(new string('-', 78));
    }
    static void RunExperiment<T>(string type, int size)
    {
        T[] num_list = new T[size];
        for (int i = 0; i < size; i++)
            num_list[i] = (T)Convert.ChangeType(i, typeof(T));

        Calculate(type, num_list, "x = x / 10", (i) => {
            var x = Convert.ToDouble(i);
            return x / 10;
        });

        Calculate(type, num_list, "x = x / PI", (i) => {
            var x = Convert.ToDouble(i);
            return x / Math.PI;
        });

        Calculate(type, num_list, "x = e^x / x^PI", (i) => {
            var x = Convert.ToDouble(i);
            return Math.Exp(x) / Math.Pow(x, Math.PI);
        });

        Calculate(type, num_list, "x = e^PIx / x^PI", (i) => {
            var x = Convert.ToDouble(i);
            return Math.Exp(Math.PI * x) / Math.Pow(x, Math.PI);
        });
    }

    static void Calculate<T>(string type, T[] num_list, string label, Func<T, double> operation)
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        foreach (var i in num_list)
        {
            operation(i);
        }
        sw.Stop();
        long forTime = sw.ElapsedMilliseconds;

        sw.Restart();
        Parallel.ForEach(num_list, i =>
        {
            operation(i);
        });
        sw.Stop();
        long parallelTime = sw.ElapsedMilliseconds;

        Console.WriteLine($"| {type,-7} | {num_list.Length,-10} | {label,-18} | {forTime,-10} | {parallelTime,-12} |");
    }
}