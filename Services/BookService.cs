using LibraryAPI.Models;
using LibraryAPI.Repository;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public void AddBook(Book book)
        {
            _bookRepository.AddBook(book);
        }

        public void DeleteBookById(int id)
        {
            _bookRepository.DeleteBookById (id);   
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetbookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public void UpdateBook(int id, Book book)
        {
            _bookRepository.UpdateBook(id,book);
        }
    }
}
