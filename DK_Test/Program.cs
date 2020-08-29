using System;
using System.Collections.Generic;

namespace DK_Test
{
    class Program
    {
        const string exitChar = "x";
        const int LineLength = 50;
        const int BatchLineCount = 5;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Infinite Sequence program ");
            Int64 x;
            string Xstr = string.Empty;
            while(true)
            {
                Console.WriteLine("Input x (positive integer) (Enter n to quit): ");
                Xstr = Console.ReadLine().Trim();
                if(Xstr==exitChar)
                    return;
                else
                {
                    if(Int64.TryParse(Xstr,out x))
                        break;
                }
            }

           
            Int64 y;
            string Ystr = string.Empty;
            while (true)
            {
                Console.WriteLine("Input y (positive integer) (Enter n to quit): ");
                Ystr = Console.ReadLine().Trim();
                if (Ystr == exitChar)
                    return;
                else
                {
                    if (Int64.TryParse(Ystr, out y))
                        break;
                }
            }

            GenerateInfiniteSequence(x, y);
        }

        public static void GenerateInfiniteSequence(Int64 x, Int64 y)
        {
            Queue<Int64> objQueue = new Queue<Int64>();
            int maxLimit = 50, maxCounter=0 ;
            int lineLength = 50;

            Int64 item;
            while(maxCounter++ < maxLimit)
            {
                if(objQueue.TryDequeue(out item))
                {
                    objQueue.Enqueue(x);
                }
                else
                {
                    item = x;
                }
                Console.WriteLine(x.ToString() + " ,");

                for(Int64 counter=0; counter< item; counter++)
                {
                    objQueue.Enqueue(y);
                    Console.WriteLine(y.ToString() + " ,");
                }

            }
        }
    }
}
