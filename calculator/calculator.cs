using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class Calculator
    {

        public static List<double> GetNumbers()
        {
            
             List<double> numbers = new List<double>();
            Console.WriteLine("type '=' to finish");
            while (true)
            {
                Console.Write("Enter number: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "=") break;
                if (double.TryParse(input, out double number))
                    numbers.Add(number);
                else
                    Console.WriteLine("Invalid input! Please enter a valid number.");
            }
            return numbers;
        }

        public static double PerformOperation(int operation, List<double> numbers)
        {
            double result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                switch (operation)
                {
                    case 1: result += numbers[i]; break;
                    case 2: result -= numbers[i]; break;
                    case 3: result *= numbers[i]; break;
                    case 4:
                        if (numbers[i] == 0)
                        {
                            Console.WriteLine("Cannot divide by zero!");
                            return result;
                        }
                        result /= numbers[i];
                        break;
                }
            }
            return result;
        }
    }
}
