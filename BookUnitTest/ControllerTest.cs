using BookManagementApi.Controllers;
using BookManagementApi.Models;
using BookManagementApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookUnitTest
{
    public class ControllerTest
    {
        private readonly Mock<IBookRepository> _repo;
        private readonly BookController controller;

        public ControllerTest()
        {
            _repo = new Mock<IBookRepository>();
            controller = new BookController(_repo.Object);
        }

        [Fact]
        public async Task GetAllBooks()
        {
            var books = new List<Book>
                {
                    new Book { Id = 0, Title = "C", Genre = "Programming", PublishedDate = DateTime.Now, Author = "Me" },
                    new Book { Id = 0, Title = "cpp", Genre = "Programming",PublishedDate=DateTime.Now, Author = "You" }
                };

            _repo.Setup(repo => repo.Getbooks()).ReturnsAsync(books);

            var result = await controller.GetAllbooks();
            var done = Assert.IsType<OkObjectResult>(result);
            var backbooks = Assert.IsType<List<Book>>(done.Value);
            Assert.Equal(2, backbooks.Count());
        }

        [Fact]
        public async Task GetById()
        {
            var book = new Book { Id = 1, Title = "Dotnet", Author = "James", Isbn = "lethit", PublishedDate = DateTime.Now, Genre = "fiction" };

            _repo.Setup(repo => repo.Getbook(1)).ReturnsAsync(book);

            var result = await controller.GetBybooks(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(1, returnBook.Id);
        }

        [Fact]
        public async Task AddByController()
        {
            var book = new Book { Id = 2, Title = "Dotnet", Author = "Shubh", Isbn = "lethit", PublishedDate = DateTime.Now, Genre = "fiction" };
            _repo.Setup(repo => repo.Addbook(book)).ReturnsAsync(book);
            var result = await controller.Addbooks(book);
            var done = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnBook = Assert.IsType<Book>(done.Value);
            Assert.Equal(2, returnBook.Id);
        }

        [Fact]
        public async Task UpdateByController()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Dotnet",
                Author = "James",
                Isbn = "lethit",
                PublishedDate = DateTime.Now,
                Genre = "fiction"
            };
            var result = await controller.Updatebooks(book, 2);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Book ID is mismatch", badRequestResult.Value);
        }
    }
}