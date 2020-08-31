using System;
using System.Collections.Generic;
using System.Text;

namespace DK_Test
{
//Developed By: Deepak Mohanty
//Date: 29-Aug-2020
//Assignment problem statement
// Write a generic and parameterised function that is able to generate the following(infinite) pattern
//2,3,3,2,3,3,3,2,3,3,3,2,3,3,2,3,3,3,2,3,3,3,2,3,3,3,2,3,3,2,…..
//    2	3	 3	 2	3	3	3	2
//This is not a random pattern ..
//Hint to understand how the pattern has ben created.If you count the number of 3s sandwitchd between 2s, you will notice that  it creates the same pattern

    public class Program
    {
        const string exitChar = "N";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Infinite Sequence program ");
            Int64 x;
            string Xstr = string.Empty;
            while (true)
            {
                Console.WriteLine("Input x (positive integer) (Enter N to quit): ");
                Xstr = Console.ReadLine().Trim().ToUpper();
                if (Xstr == exitChar)
                    return;
                else
                {
                    if (Int64.TryParse(Xstr, out x))
                        break;
                }
            }


            Int64 y;
            string Ystr = string.Empty;
            while (true)
            {
                Console.WriteLine("Input y (positive integer) (Enter N to quit): ");
                Ystr = Console.ReadLine().Trim().ToUpper();
                if (Ystr == exitChar)
                    return;
                else
                {
                    if (Int64.TryParse(Ystr, out y))
                        break;
                }
            }

            SequenceGeneration objSequenceGeneration = new SequenceGeneration();
            objSequenceGeneration.GenerateInfiniteSequence(x, y);
        }


    }

    public class SequenceGeneration
    {
        Queue<Int64> objQueue = new Queue<Int64>();
        StringBuilder objStrbuilder = new StringBuilder();
        const string exitChar = "N";
        const int LineLength = 10;
        const int BatchLineCount = 5;

        public void GenerateInfiniteSequence(Int64 x, Int64 y)
        {
            try
            {
                Console.WriteLine("Sequence will be generated for X: " + x.ToString() + ", Y: " + y.ToString());
                
                Int64 item;
                string inputCode = "Y";
                while (inputCode != exitChar)
                {
                    for (int lineCounter = 0; lineCounter < BatchLineCount; lineCounter++)
                    {
                        objStrbuilder.Clear();
                        for (int charCounter = 0; charCounter < LineLength; charCounter++)
                        {

                            if (objQueue.TryDequeue(out item))
                            {
                                objQueue.Enqueue(x);
                            }
                            else
                            {
                                item = x;
                            }
                            objStrbuilder.Append(x.ToString() + ", ");

                            for (Int64 counter = 0; counter < item; counter++)
                            {
                                objQueue.Enqueue(y);
                                objStrbuilder.Append(y.ToString() + ", ");
                            }


                        }
                        Console.WriteLine(objStrbuilder.ToString());
                    }

                    Console.WriteLine("To continue (Press Y)/ To exit (Press N)");
                    inputCode = Console.ReadLine().Trim().ToUpper();
                }
            }
            catch(Exception ex)
            {
                Console.Write("Error: " + ex.Message);
            }
        }

    }
}
