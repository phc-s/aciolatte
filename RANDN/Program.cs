using System;

namespace BASIC
{
    class Program
    {
        static void Main(string[] args)
        {
            bool basicoProceed = true;
            while (basicoProceed)
            {
                Console.Clear();
                Console.WriteLine("You're using now 'ACIO LATTE Random'!");
                Console.WriteLine("Your simple integer randomizer!");

                int minNumber = getInteger("Insert the minimum integer to use: ");
                int maxNumber = getInteger("Insert the maximum integer to use: ");
                int result = new Random().Next(minNumber, maxNumber+1);
                Console.WriteLine($"The result is: {result}");

                basicoProceed = AskToContinue();
            }
            LatteFillingAnimation();
        }
        static int getInteger(string message)
        {
            int number;
            do
            {
            Console.WriteLine(message);
            } 
            while (!int.TryParse(Console.ReadLine(), out number));
            return number;
        }

        static bool AskToContinue()
        {
            Console.WriteLine("Do you want randomize again?");
            Console.WriteLine("insert 'yes' to proceed.");
            Console.WriteLine("Anything else is a 'no'.");
            return Console.ReadLine()?.ToLower() == "yes";
        }
        static void LatteFillingAnimation()
        {
            Console.Clear();
            Console.WriteLine("Filling your latte...");
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, 2);
                Console.Write("[");
                Thread.Sleep(200);

                for (int j = 0; j < i; j++)
                {
                    Console.Write("$"); 
                }
                
                for (int j = i; j < 10; j++)
                {
                    Console.Write("-");
                }

                Console.WriteLine("]");
                Thread.Sleep(500);
            }
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Your latte is ready! Enjoy!  🥛");
            Console.WriteLine("Thanks for Using ACIO LATTE! 🥛");
        }
    }
}