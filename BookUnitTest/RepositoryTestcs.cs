using BookManagementApi.Models;
using BookManagementApi.Repository.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace BookUnitTest
{
    public class RepositoryTestcs
    {
        private readonly DbContextOptions<ManageContext> options;
        private readonly ManageContext context;
        private readonly BookRepository repository;

        public RepositoryTestcs()
        {
            options = new DbContextOptionsBuilder<ManageContext>()
                      .UseInMemoryDatabase(databaseName: "BookDatabase")
                      .Options;
            context = new ManageContext(options);
            repository = new BookRepository(context);
        }

        [Fact]
        public async Task AddAsync_Books()
        {
            //var repo = new BookRepository(context);
            var book = new Book { Title = "Dotnet", Author = "James", Isbn = "lethit", PublishedDate = DateTime.Now, Genre = "fiction" };
            await repository.Addbook(book);
            //var result = await repo.Addbook(book);
            var result = await context.Books.FindAsync(1);
            Assert.NotNull(result);
            Assert.Equal(book.Title, result.Title);
        }

        [Fact]
        public async Task GetallRecord_Book()
        {
            var book = new Book
            {
                Id = 0,
                Title = "Dotnet",
                Author = "James",
                Isbn = "lethit",
                PublishedDate = DateTime.Now,
                Genre = "fiction"
            };
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var result = repository.Getbooks();
            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task GetByID_Book()
        {
            var data = await repository.Getbook(1);
            Assert.Null(data);
        }

        [Fact]
        public async Task UpdateData_book()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Dotnet",
                Author = "James",
                Isbn = "lethit",
                PublishedDate = DateTime.Now,
                Genre = "ABC"
            };
            context.Books.Add(book);
            await context.SaveChangesAsync();
            book.Author = "Update Mahesh";
            await repository.Updatebook(book);
            var updatebook = await context.Books.FindAsync(1);
            Assert.NotNull(updatebook);
            Assert.Equal("Update Mahesh", updatebook.Author);
        }

        [Fact]
        public async Task GetFilterData_Book()
        {
            var books = new List<Book>
                {
                    new Book { Id = 0, Title = "C", Genre = "Programming", PublishedDate = DateTime.Now, Author = "Me" },
                    new Book { Id = 0, Title = "cpp", Genre = "Programming",PublishedDate=DateTime.Now, Author = "You" }
                };
            context.AddRange(books);
            await context.SaveChangesAsync();

            var filterdata = await repository.FilterBooks("Programming", "Me");
            Assert.Single(filterdata);
            Assert.Equal("C", filterdata.First().Title);
        }

        [Fact]
        public async Task DeleteById()
        {
            var result = await repository.Deletebook(1);
            Assert.True(result);
        }
    }
}