




using Atm;


        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            try
            {
                string choice = Console.ReadLine();
                if (choice == "1") Methods.CreateAccount();
                else if (choice == "2") Methods.Login();
                else if (choice == "3") Environment.Exit(0);
                else Console.WriteLine("Invalid selection. Please choose 1-3.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
        }
  
