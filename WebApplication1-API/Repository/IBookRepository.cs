using DataAccessLayers;
namespace WebApplication1_API.Repository 
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBook(int Id);
        Task<Book> AddNewBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<Book> DeleteBook(int Id);
        Task<IEnumerable<Book>> Search(string BookName);
    }
}
