﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Atm
{
    internal class Methods
    {

       



        internal static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "users.txt"
        );

        internal static void CreateAccount()
        {
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine() ?? "";

            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine() ?? "";

            var users = LoadUsers();

            
            if (users.FirstOrDefault(u => u.AccountNumber == accNum) != null)
            {
                Console.WriteLine("Account number already exists.");
                Console.ReadKey();
                return;
            }

            users.Add(new User { AccountNumber = accNum, Pin = pin, Balance = 0 });
            SaveUsers(users);

            Console.WriteLine("User created successfully.");
            Console.ReadKey();
        }


        internal static void Login()
        {
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine() ?? "";

            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine() ?? "";

            var user = LoadUsers().FirstOrDefault(u => u.AccountNumber == accNum && u.Pin == pin);
            if (user == null)
            {
                Console.WriteLine("Invalid credentials.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"logged in succesfully");
            Console.ReadKey();
            ShowUserMenu(user);
        }

        internal static void ShowUserMenu(User currentUser)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Deposit\n2. Withdraw\n3. Check Balance\n4. Logout");
                Console.Write("Select an option: ");

                try
                {
                    string option = Console.ReadLine();
                    if (option == "1") ChangeBalance(currentUser, "deposit");
                    else if (option == "2") ChangeBalance(currentUser, "withdraw");
                    else if (option == "3") CheckBalance(currentUser);
                    else if (option == "4") return;
                    else
                    {
                        Console.WriteLine("That option does not exist. Please select an option 1-4.");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        internal static void ChangeBalance(User currentUser, string transactionType)
        {
            Console.Write($"Enter {transactionType} amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount, Must be positive");
                Console.ReadKey();
                return;
            }

            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.AccountNumber == currentUser.AccountNumber);
            if (user == null)
            {
                Console.WriteLine("User not found");
                Console.ReadKey();
                return;
            }

            if (transactionType == "withdraw" && user.Balance < amount)
            {
                Console.WriteLine("Insufficient funds");
                Console.ReadKey();
                return;
            }

            if (transactionType == "deposit")
            {
                user.Balance += amount;
            }
            else if (transactionType == "withdraw")
            {
                user.Balance -= amount;
            }

            currentUser.Balance = user.Balance;
            SaveUsers(users);

            Console.WriteLine($"{transactionType} successful. New balance: {user.Balance}$");
            Console.ReadKey();
        }

        internal static void CheckBalance(User user)
        {
            Console.WriteLine($"Current balance: {user.Balance}$");
            Console.ReadKey();
        }

        internal static List<User> LoadUsers()
        {
            var users = new List<User>();

            if (File.Exists(FilePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(FilePath);
                    users = JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();          
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading users: {ex.Message}");
                }
            }

            return users;
        }


        internal static void SaveUsers(List<User> users)
        {
            try
            {                
                using (StreamWriter writer = new StreamWriter(FilePath, false))
                {                    
                    string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                    writer.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users: {ex.Message}");
            }          
        }
    }
}
        