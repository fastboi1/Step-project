


using Library.models;
using Library;
using System.Diagnostics.Metrics;

BookManager bookManager = new BookManager();

while (true)
{
    Console.Clear();
    Console.WriteLine("Book Management System");
    Console.WriteLine("1. Add Book");
    Console.WriteLine("2. View Books");
    Console.WriteLine("3. Search Book by Title");
    Console.WriteLine("4. Delete Book by ID");
    Console.WriteLine("5. Exit");
    Console.Write("\nEnter your choice: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddBookMenu(bookManager);
            break;
        case "2":
            bookManager.ViewBooks();
            break;
        case "3":
            SearchBookMenu(bookManager);
            break;
        case "4":
            DeleteBookMenu(bookManager);
            break;
        case "5":
            Console.WriteLine("Exiting...");
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}
    

    static void AddBookMenu(BookManager bookManager)
{
    Console.Clear();
    Console.WriteLine("Add a New Book");

    Console.Write("Enter Title: ");
    string title = Console.ReadLine();

    
    Console.Write("Enter Author's First Name: ");
    string firstName = Console.ReadLine();

    
    Console.Write("Enter Author's Last Name: ");
    string lastName = Console.ReadLine();

   
    int age;
    while (true)
    {
        Console.Write("Enter Author's Age: ");
        string ageInput = Console.ReadLine();
        if (int.TryParse(ageInput, out age))
            break;
        else
            Console.WriteLine("Please enter a valid number for age.");
    }

    
    DateTime releaseDate;
    while (true)
    {
        Console.Write("Enter Release Year (yyyy mm dd): ");
        string dateInput = Console.ReadLine();
        if (DateTime.TryParse(dateInput, out releaseDate))
            break;
        else
            Console.WriteLine("Please enter the date correctly");
    }

    Author author = new Author(firstName, lastName, age);
    bookManager.AddBook(title, author, releaseDate);
}

static void SearchBookMenu(BookManager bookManager)
{
    Console.Clear();
    Console.WriteLine("Search for a Book");
    Console.Write("Enter Title to Search: ");
    string title = Console.ReadLine();
    bookManager.SearchBook(title);
}

static void DeleteBookMenu(BookManager bookManager)
{
    Console.Clear();
    Console.WriteLine("Delete a Book");

    
    int bookID;
    while (true)
    {
        Console.Write("Enter Book ID to Delete: ");
        string idInput = Console.ReadLine();
        if (int.TryParse(idInput, out bookID))
            break;
        else
            Console.WriteLine("Invalid input. Please enter a valid number for Book ID.");
    }

    bookManager.DeleteBook(bookID);
}