using System;

namespace Basico
{
    class Program
    {
        static void Main(string[] args)
        {

            bool basicoProceed = true;

            while (basicoProceed)
            {
                Console.Clear();
                Console.WriteLine("You're using 'ACIO LATTE Basico'");
                Console.WriteLine("Your simple integer calculator!");

                int firstNumber = getInteger("Insert the first integer: ");
                char operation = getOperator();
                int secondNumber = getInteger("Insert the second integer: ");

                int result = Calculate(firstNumber, secondNumber, operation);
                Console.WriteLine($"The result of {firstNumber} {operation} {secondNumber} = {result}");

                basicoProceed = AskToContinue();
            }

            Console.WriteLine("Thanks for using 'ACIO LATTE Basico'");
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

        static char getOperator()
        {

            char operation;
            do
            {
                Console.Write("(+, -, *, /): ");
                operation = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            while ("+-*/".IndexOf(operation) < 0);
            return operation;

        }

        static int Calculate(int firstNumber, int secondNumber, char operation)
        {

            return operation switch
            {
                '+' => firstNumber + secondNumber,
                '-' => firstNumber - secondNumber,
                '*' => firstNumber * secondNumber,
                '/' => secondNumber != 0 ? firstNumber / secondNumber : throw new DivideByZeroException("Error: Division by zero!")
            };

        }

        static bool AskToContinue()
        {
            Console.Write("Do you want use it again? (yes/no): ");
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
            Console.WriteLine("Your latte is ready! Enjoy! 🥛");
        }
    }
}