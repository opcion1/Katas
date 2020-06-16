using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz();
            Console.ReadLine();
        }

        public static void FizzBuzz()
        {
            for (int i = 1; i < 101; i++)
            {
                Console.WriteLine(NumToFizzBuzz(i));
            }
        }

        public static string NumToFizzBuzz(int num)
        {
            bool isMod3 = num % 3 == 0;
            bool isMod5 = num % 5 == 0;

            return
                isMod3 && isMod5
                    ? "FizzBuzz"
                    : isMod3
                        ? "Fizz"
                        : isMod5
                            ? "Buzz"
                            : num.ToString();
        }
    }
}
