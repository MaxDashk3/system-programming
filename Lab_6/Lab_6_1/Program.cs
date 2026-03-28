using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"C:\Users\maxda\Documents\KNU\system-programming\Lab_6\TestDir";

        if (Directory.Exists(path))
        {
            Console.WriteLine($"Структура директорії: {path}");
            PrintDirectoryTree(path, 0);
        }
        else
        {
            Console.WriteLine("Директорію не знайдено.");
        }
    }

    static void PrintDirectoryTree(string path, int indentLevel)
    {
        string indent = new string(' ', indentLevel * 4);

        DirectoryInfo dir = new DirectoryInfo(path);
        Console.WriteLine($"{indent}[Folder] {dir.Name}");

        foreach (var file in dir.GetFiles())
        {
            Console.WriteLine($"{indent}    |-- {file.Name}");
        }

        foreach (var subDir in dir.GetDirectories())
        {
            PrintDirectoryTree(subDir.FullName, indentLevel + 1);
        }
    }
}