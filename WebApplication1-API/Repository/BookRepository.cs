using DataAccessLayers;
using Microsoft.EntityFrameworkCore;
using WebApplication1_API.DataContext;

namespace WebApplication1_API.Repository
{
 
    public class BookRepository:IBookRepository
    {
        public readonly Application_DBContext _context;
        public List<Book> books;
        public BookRepository(Application_DBContext context)
        {
            _context = context;
        }

        //getting All Books 
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        //Getting book By Specfic ID
        public async Task<Book> GetBook(int Id)
        {
            return await _context.Books.Where(a => a.Id == Id).FirstOrDefaultAsync();

        }

        //Create New Book Record
        public async Task<Book> AddNewBook(Book book)
        {
            var data = await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        //Delete Book By An ID
        public async Task<Book> DeleteBook(int Id)

        {
            var data = await _context.Books.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (data != null)
            {
                _context.Books.Remove(data);
                _context.SaveChangesAsync();
                return data;
            }
            return null;
        }

        //Update Book By ID
        public async Task<Book> UpdateBook(Book book)
        {
            var data = await _context.Books.FirstOrDefaultAsync(a => a.Id == book.Id);
            if (data != null)
            {
                data.Bookname = book.Bookname;
                data.Author = book.Author;
                data.Catagory = book.Catagory;
                data.publishDate = book.publishDate;
                await _context.SaveChangesAsync();
                return data;
            }
            return null;
        }

        //Search  Book  by Author Name & BookName
        public async Task<IEnumerable<Book>> Search(string BookName)
        {
            IQueryable<Book> query = _context.Books;
            if (!string.IsNullOrEmpty(BookName) )
            {
                query = query.Where(a => a.Bookname.Contains(BookName));
            }
            return await query.ToListAsync();
        }

      

     

       
    }
}
