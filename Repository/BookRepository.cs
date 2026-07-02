using Microsoft.EntityFrameworkCore;
using LibraryAPI.DataBase;
using LibraryAPI.Models;


namespace LibraryAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void DeleteBookById(int id)
        {
            var bookToDelete = _context.Books.FirstOrDefault(b => b.Id == id);
            if(bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"book with {id} not found");
            }
           
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            var books = _context.Books.FirstOrDefault(b => b.Id == id);
            if (books == null)
            {
                throw new Exception($"book with{id} not found");
            }
            return books;
        }

        public void UpdateBook(int id, Book book )
        {
            var bookToUpdate = _context.Books.FirstOrDefault(b => b.Id == id );
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Author = book.Author;
                bookToUpdate.Year = book.Year;
                _context.SaveChanges();

            }
            else
            {
                throw new Exception($"book with {id} not found");
            }
           

        }
    }
}
