using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Await
{
    public class Task_asynchron
    {
        public static void Main(string[] args)
        {
            Calculate();
        }

        private static void Calculate()
        {
            Stopwatch stopAsync = Stopwatch.StartNew();

            var task1 = Task.Run(() =>
            {
                return Calculate1();
            });
            var task2 = Task.Run(() =>
            {
                return Calculate2();
            });

            Task.WaitAll(task1, task2);

            var awaiter1 = task1.GetAwaiter();
            var awaiter2 = task2.GetAwaiter();

            var result1 = awaiter1.GetResult();
            var result2 = awaiter2.GetResult();

            Calculate3(result1, result2);

            stopAsync.Stop();
            Console.WriteLine("..... " + stopAsync);
        }

        static int Calculate1()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("C1 -> " + i);
            }

            return 100;
        }

        static int Calculate2()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("\tC2 -> " + i);
            }

            return 200;
        }

        static int Calculate3(int result1, int result2)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("\t\tC3 -> " + i);
            }

            return result1 + result2;
        }
    }
}