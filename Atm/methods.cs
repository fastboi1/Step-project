﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm
{
    internal class Methods
    {

        internal static readonly string FilePath = Path.Combine(
         Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
         "users.txt"
     );

    
        internal static void CreateUser()
        {
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine() ?? "";

            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine() ?? "";

            var users = ReadUsers();
            if (users.Any(u => u.AccountNumber == accNum))
                throw new Exception("Account number already exists");
            

            users.Add(new User { AccountNumber = accNum, Pin = pin, Balance = 0 });
            SaveUsers(users);

            Console.WriteLine("User created successfully");
            Console.ReadKey();
        }

       
        internal static void Login()
        {
            Console.Write("Enter account number: ");
            string accNum = Console.ReadLine() ?? "";

            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine() ?? "";

            var user = ReadUsers().FirstOrDefault(u => u.AccountNumber == accNum && u.Pin == pin)
                ?? throw new Exception("Invalid credentials");

            Console.WriteLine($"Welcome, {user.AccountNumber}");
            Console.ReadKey();
            ShowUserMenu(user);
        }


        internal static void ShowUserMenu(User currentUser)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[1] Deposit\n[2] Withdraw\n[3] Check Balance\n[4] Logout");
                Console.Write("Select an option: ");

                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1": ChangeBalance(currentUser, TransactionType.Deposit); break;
                        case "2": ChangeBalance(currentUser, TransactionType.Withdraw); break;
                        case "3": CheckBalance(currentUser); break;
                        case "4": return;
                        default:
                            Console.WriteLine("That option does not exist. Please select an option 1-4.");
                            Console.ReadKey(); 
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ReadKey();
                    
                }
            }
        }

      
        internal static void ChangeBalance(User currentUser, TransactionType transactionType)
        {
            Console.Write($"Enter {transactionType.ToString().ToLower()} amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Error: Invalid amount. Must be positive");
                Console.ReadKey();
            }
               
           

            var users = ReadUsers();
            var user = users.FirstOrDefault(u => u.AccountNumber == currentUser.AccountNumber)
                ?? throw new Exception("User not found");

            if (transactionType == TransactionType.Withdraw && user.Balance < amount)
            {
                Console.WriteLine("error: Insufficient funds");
                Console.ReadKey();
            }
                
           

            
             if(transactionType == TransactionType.Deposit)
             {
                _ = user.Balance + amount;
            }
            else
             {
                _ = user.Balance - amount;
             }; ;


                currentUser.Balance = user.Balance; 
            SaveUsers(users);
            Console.WriteLine($"{transactionType} successful. New balance: {user.Balance:C}");
            Console.ReadKey();
           
        }

        
        internal static void CheckBalance(User user)
        {
            Console.WriteLine($"Current balance: {user.Balance:C}");
            Console.ReadKey();
            
        }

        
        #region Helper Methods and Enums
        internal enum TransactionType { Deposit, Withdraw }

       
        internal static List<User> ReadUsers()
        {
            var users = new List<User>();
            if (File.Exists(FilePath))
            {
                foreach (var line in File.ReadAllLines(FilePath))
                {
                    var parts = line.Split(',');
                    if (parts.Length != 3) throw new Exception("Corrupted data file");
                    users.Add(new User
                    {
                        AccountNumber = parts[0],
                        Pin = parts[1],
                        Balance = decimal.Parse(parts[2])
                    });
                }
            }
            return users;
        }

      
        internal static void SaveUsers(List<User> users)
        {
            File.WriteAllLines(FilePath, users.Select(u => $"{u.AccountNumber},{u.Pin},{u.Balance}"));
        }

       
       
        #endregion
    }
}
