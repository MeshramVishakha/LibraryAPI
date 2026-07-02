using LibraryAPI.Models;

namespace LibraryAPI.Repository
{
    public interface IBookRepository
    {
        public List<Book> GetAll();

        public Book GetBookById(int id);

        public void AddBook(Book book);

        public void DeleteBookById(int id);

        public void UpdateBook(int id, Book book);
    }
}
