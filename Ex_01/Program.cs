using System;
using System.Collections.Generic;

namespace Ex_01
{
    class Program
    {
        static string[] _names;
        static void Main(string[] args)
        {
            if (!Initalize())
            {
                Console.WriteLine("Ошибка. Не удалось загрузить файл с названиями");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите имя пользователя: ");
            string writedStr = Console.ReadLine();
            Console.WriteLine();
            string result = SearchName(writedStr);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static string SearchName(string writedStr)
        {
            foreach (string name in _names)
            {
                int result = GetLevenshteinDistance(writedStr, name);
                if (result == 0)
                {
                    return $"Привет, {writedStr}";
                }
                else if (result == 1)
                {
                    Console.WriteLine($"Вы имели в виду {name}? Y/N");
                    while (true)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.Y)
                        {
                            return $"Привет, {name}";
                        }
                        if (key == ConsoleKey.N)
                        {
                            break;
                        }
                    }
                    continue;
                }
                else
                {
                    return "Ваше имя не найдено.";
                }
            }

            return "Ваше имя не найдено.";
        }

        private static bool Initalize()
        {
            try
            {
                _names = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
                if (_names == null || _names.Length == 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public static int GetLevenshteinDistance(string writedStr, string strFromStore)
        {
            if (string.IsNullOrEmpty(writedStr) || string.IsNullOrWhiteSpace(writedStr))
            {
                return -1;
            }

            if (writedStr == strFromStore)
            {
                return 0;
            }

            int l1 = writedStr.Length;
            int l2 = strFromStore.Length;
            int track, t;
            int[,] dist = new int[l2 + 1, l1 + 1];

            for (int i = 0; i <= l1; i++)
            {
                dist[0, i] = i;
            }

            for (int i = 0; i <= l2; i++)
            {
                dist[i, 0] = i;
            }

            for (int j = 1; j <= l1; j++)
            {
                for (int i = 1; i <= l2; i++)
                {
                    if (writedStr[j - 1] == strFromStore[i - 1])
                    {
                        track = 0;
                    }
                    else
                    {
                        track = 1;
                    }
                    t = dist[i - 1, j] + 1 < dist[i, j - 1] + 1 ? dist[i - 1, j] + 1 : dist[i, j - 1] + 1;

                    dist[i, j] = t < dist[i - 1, j - 1] + track ? t : dist[i - 1, j - 1] + track;
                }
            }

            return dist[l2, l1];
        }
    }
}
