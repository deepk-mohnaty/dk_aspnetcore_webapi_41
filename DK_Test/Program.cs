using System;
using System.Collections;
using System.Collections.Generic;

namespace DK_Test
{
   public class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
                Console.WriteLine("Welcome to infinite searies program.(enter x to exit)");

                Console.WriteLine("Enter X(positive integer): ");
                string Xstr = Console.ReadLine().Trim();
                long x;
                while (!Int64.TryParse(Xstr, out x))
                {
                    Console.WriteLine("Enter X(positive integer): ");
                    Xstr = Console.ReadLine().Trim();
                    if (Xstr == "x") { return; }
                }

                Console.WriteLine("Enter Y(positive integer): ");
                string Ystr = Console.ReadLine().Trim();
                long y;
                while (!Int64.TryParse(Ystr, out y))
                {
                    Console.WriteLine("Enter Y(positive integer): ");
                    Ystr = Console.ReadLine().Trim();

                    if (Ystr == "x") { return; }
                }
                CreateInfiniteSequence(x, y);

            //}
            //catch(Exception ex)
            //{
            //    string message = ex.Message;
            //    Console.Write("Error thrown:"+message);
            //}

        }


        public static void CreateInfiniteSequence(Int64 x, Int64 y)
        {
            Queue<Int64> objQueue = new Queue<Int64>();
            //objQueue.Enqueue(x);
            Int64 item = 0;
            int countLimit = 100;
            int limitCounter = 0;
            while (true && limitCounter++ <countLimit)
            {
                if (objQueue.TryDequeue(out item))
                {
                    objQueue.Enqueue(x);
                }
                else
                {
                    item = x;
                }
                Console.WriteLine(x.ToString() + " ,");
                for (Int64 counter=0; counter< item; counter++)
                {
                    objQueue.Enqueue(y);
                    Console.WriteLine(y.ToString() + " ,");
                }
            }

        }
    }
}
