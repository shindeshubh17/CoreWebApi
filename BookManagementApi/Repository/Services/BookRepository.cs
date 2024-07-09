using BookManagementApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BookManagementApi.Repository.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly ManageContext dataContext;

        public BookRepository(ManageContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Book> Addbook(Book data)
        {
            var result = await dataContext.Books.AddAsync(data);
            await dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Deletebook(int id)
        {
            var result = await dataContext.Books.FindAsync(id);
            if (result != null)
            {
                dataContext.Books.Remove(result);
                await dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Book> Getbook(int id)
        {
            var data = await dataContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                return data;
            }
            return null;
        }

        public async Task<IEnumerable<Book>> Getbooks()
        {
            return await dataContext.Books.ToListAsync();
        }

        public async Task<Book> Updatebook(Book info)
        {
            dataContext.Entry(info).State = EntityState.Modified;
            await dataContext.SaveChangesAsync();
            return info;
        }

        public async Task<IEnumerable<Book>> FilterBooks(string genre, string author)
        {
            if (!string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(author))
            {
                var result = await dataContext.Books.Where(b => b.Genre == genre && b.Author == author).ToListAsync();
                return result;
            }
            return null;
        }
    }
}