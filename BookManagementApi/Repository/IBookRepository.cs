using BookManagementApi.Models;

namespace BookManagementApi.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Getbooks();

        Task<Book> Getbook(int id);

        Task<Book> Addbook(Book data);

        Task<Book> Updatebook(Book info);

        Task<bool> Deletebook(int id);

        Task<IEnumerable<Book>> FilterBooks(string genre, string author);
    }
}