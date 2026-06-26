using System;
using System.Threading;

class Program
{
    static int counter = 0;
    static object locker = new object();

    static void Main()
    {
        Console.WriteLine("Без lock:");

        counter = 0;
        StartThreads(false);

        Console.WriteLine("\nС lock:");

        counter = 0;
        StartThreads(true);
    }

    static void StartThreads(bool useLock)
    {
        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            if (useLock)
                threads[i] = new Thread(AddWithLock);
            else
                threads[i] = new Thread(AddWithoutLock);

            threads[i].Start();
        }

        foreach (Thread thread in threads)
            thread.Join();

        Console.WriteLine($"Значение счетчика: {counter}");
    }

    static void AddWithoutLock()
    {
        for (int i = 0; i < 100000; i++)
            counter++;
    }

    static void AddWithLock()
    {
        for (int i = 0; i < 100000; i++)
        {
            lock (locker)
            {
                counter++;
            }
        }
    }
}