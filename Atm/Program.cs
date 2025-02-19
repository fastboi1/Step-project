using Atm;
   
while (true)
{
    Console.Clear();
    Console.WriteLine("[1] Create User");
    Console.WriteLine("[2] Login");
    Console.WriteLine("[3] Exit");
    Console.Write("Select an option: ");

    try
    {
        switch (Console.ReadLine())
        {
        case "1": Methods.CreateUser(); break;
        case "2": Methods.Login(); break;
        case "3": Environment.Exit(0); break;
        default: throw new Exception("Invalid option");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.ReadKey();
    }
}
        
