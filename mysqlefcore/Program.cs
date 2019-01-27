using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace mysqlefcore
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertData(); // If again run this program this line make to command line!
            PrintData();
        }

        private static void InsertData(){
            using(var context = new LibraryContext()){
                //Create the Database if not exist
                context.Database.EnsureCreated();

                //Add a publisher
                
                var publisher = new Publisher{
                    Name = "Iletisim"
                };

                context.Publishers.Add(publisher);

                //Adds some books
                context.Books.Add(new Book
                {
                ISBN = "978-0544003415",
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                Language = "English",
                Pages = 1216,
                Publisher = publisher
                });
                context.Books.Add(new Book
                {
                ISBN = "978-0547247762",
                Title = "The Sealed Letter",
                Author = "Emma Donoghue",
                Language = "English",
                Pages = 416,
                Publisher = publisher
                });

                // Saves changes
                context.SaveChanges();
                    }
        }
        private static void PrintData(){
            using (var context = new LibraryContext()){

                var books = context.Books
                    .Include(p => p.Publisher);
                foreach(var book in books)
                {
                var data = new StringBuilder();
                data.AppendLine($"ISBN: {book.ISBN}");
                data.AppendLine($"Title: {book.Title}");
                data.AppendLine($"Publisher: {book.Publisher.Name}");
                Console.WriteLine(data.ToString());
                }

            }
        }
    }
}
