

using project;

bool running = true;
while (running)
{
    Console.Clear();
    Console.WriteLine("Choose an operation:");
    Console.WriteLine("1. +");
    Console.WriteLine("2. -");
    Console.WriteLine("3. *");
    Console.WriteLine("4. /");
    Console.WriteLine("5.  Exit");

    Console.Write("Enter your choice(1-5): ");
    string choice = Console.ReadLine();

    if (choice == "5")
    {
        Console.WriteLine("Exiting");
        running = false;
        continue;
    }

    if (!int.TryParse(choice, out int operation) || operation < 1 || operation > 4)
    {
        Console.WriteLine("Invalid choice Press any key to retry");
        Console.ReadKey();
        continue;
    }

    List<double> numbers = Calculator.GetNumbers();
    double result = Calculator.PerformOperation(operation, numbers);

    Console.WriteLine($"Result: {result}");
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
}



