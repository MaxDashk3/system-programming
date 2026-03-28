using System;
using System.IO;

class Program
{
    static void Main()
    {
        // виправлення відображення укр. тексту
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;


        Console.Write("Введіть шлях до папки у якій проводитиметься пошук: ");
        string startPath = Console.ReadLine();

        Console.Write("Введіть назву файлу для пошуку: ");
        string fileNameToFind = Console.ReadLine();

        if (Directory.Exists(startPath))
        {
            Console.WriteLine("\nРезультати пошуку:");
            SearchFile(startPath, fileNameToFind);
            Console.WriteLine("\nПошук завершено.");
        }
        else
        {
            Console.WriteLine("Початкову директорію не знайдено.");
        }
    }

    static void SearchFile(string currentPath, string targetFileName)
    {
        try
        {
            string[] files = Directory.GetFiles(currentPath);
            foreach (string file in files)
            {
                if (Path.GetFileName(file).Contains(targetFileName,StringComparison.OrdinalIgnoreCase))
                {
                    FileInfo info = new FileInfo(file);
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine($"Знайдено: {info.Name}");
                    Console.WriteLine($"Повний шлях: {info.FullName}");
                    Console.WriteLine($"Розмір: {info.Length} байт");
                    Console.WriteLine($"Дата створення: {info.CreationTime}");
                }
            }

            string[] directories = Directory.GetDirectories(currentPath);
            foreach (string dir in directories)
            {
                SearchFile(dir, targetFileName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при доступі до {currentPath}: {ex.Message}");
        }
    }
}