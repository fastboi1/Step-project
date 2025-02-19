using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTN
{
    internal class Methods
    {
        internal static void PlayGame()
        {
            int attempts = SelectDifficulty();
            int targetNumber = new Random().Next(1, 101);

            Console.WriteLine("I have picked a number between 1 and 100. Try to guess it!");

            for (int i = 1; i <= attempts; i++)
            {
                int guess = GetUserGuess(i, attempts);

                if (guess == targetNumber)
                {
                    Console.WriteLine("Congratulations! You guessed the number in {0} attempts!", i);
                    return;
                }
                else if (guess < targetNumber)
                {
                    Console.WriteLine("Too low!");
                }
                else
                {
                    Console.WriteLine("Too high!");
                }
            }

            Console.WriteLine("Out of attempts! The correct number was {0}.", targetNumber);
        }

        internal static int SelectDifficulty()
        {
            Console.WriteLine("Welcome to Guess the Number!");
            Console.WriteLine("Choose a difficulty:");
            Console.WriteLine("1. Easy (10 attempts)");
            Console.WriteLine("2. Medium (7 attempts)");
            Console.WriteLine("3. Hard (5 attempts)");

            while (true)
            {
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1") return 10;
                else if (choice == "2") return 7;
                else if (choice == "3") return 5;
                else Console.WriteLine("Invalid choice Please enter 1, 2, or 3.");
            }
        }

        internal static int GetUserGuess(int attempt, int maxAttempts)
        {
            while (true)
            {
                Console.Write($"Attempt {attempt}/{maxAttempts}: Enter your guess: ");
                if (int.TryParse(Console.ReadLine(), out int guess))
                    return guess;
                else
                    Console.WriteLine("Invalid input! Please enter a number.");
            }
        }
    }
}
