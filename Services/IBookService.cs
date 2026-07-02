using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IBookService
    {
        public List<Book> GetAllBooks();

        public Book GetbookById(int id);

        public void AddBook(Book book);

        public void DeleteBookById(int id);

        public void UpdateBook(int id,Book book);


    }
}
