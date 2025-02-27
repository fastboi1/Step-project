using Library.models;
using System.Text.Json;


namespace Library
{
    internal class BookManager
    {
        private readonly string filePath;
        private List<Book> books = new List<Book>();
        private int idCounter = 1;

        public BookManager()
        {
            books = new List<Book>();
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "books.txt");
            LoadBooksFromJson();

            if (books.Count > 0)
                idCounter = books.Max(b => b.ID) + 1;

        }

      

        public void AddBook(string title, Author author, DateTime year)
        {
            books.Add(new Book(idCounter++, title, author, year));
            SaveBooksToJson();
            Console.WriteLine("Book added successfully \n");
        }

        public void ViewBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        public void SearchBook(string title)
        {
            var foundBooks = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            if (foundBooks.Count == 0)
            {
                Console.WriteLine("No books found with that title.");
            }
            else
            {
                Console.WriteLine("Found Books:");
                foreach (var book in foundBooks)
                {
                    Console.WriteLine(book);
                }
            }
        }

        public void DeleteBook(int bookID)
        {
            var bookToRemove = books.FirstOrDefault(b => b.ID == bookID);
            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
                SaveBooksToJson();
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        private void LoadBooksFromJson()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Book file not found, creating a new one.");
                books = new List<Book>();
                return;
            }

            try
            {
                string jsonString = File.ReadAllText(filePath);
                books = JsonSerializer.Deserialize<List<Book>>(jsonString) ?? new List<Book>();
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"error loading books: {ex.Message}");
                books = new List<Book>();
            }
        }

        private void SaveBooksToJson()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false)) 
                {
                    string jsonString = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                    writer.Write(jsonString);
                }
            }         
            catch (Exception ex)
            {
                Console.WriteLine($"error saving books: {ex.Message}");
            }
        }




    }
}
