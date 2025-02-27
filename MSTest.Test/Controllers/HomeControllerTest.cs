﻿using Telerik.JustMock;
using MvcUnitTesting_dotnet8.Models;
using MvcUnitTesting_dotnet8.Controllers;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;


namespace MvcUnitTesting.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Returns_All_books_In_DB()
        {
            //Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            Mock.Arrange( () => bookRepository.GetAll()).
                Returns(new List<Book>()
                {
                    new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                    new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                    new Book { Genre="Science Fiction", ID=1, Name="Escape from the vortex", Price=12.50m},
                    new Book { Genre="History", ID=2, Name="The Battle of the Somme", Price=22m},
                }).MustBeCalled();

            //Act
            HomeController controller = new HomeController(bookRepository,null);
            ViewResult viewResult = controller.Index() as ViewResult;
            var model = viewResult.Model as IEnumerable<Book>;

            //Assert
            Assert.AreEqual(4, model.Count());

        }

       
        [TestMethod]
        public void Privacy()
        {
            // Arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            HomeController controller = new HomeController(bookRepository,null);

            // Act
            ViewResult result = controller.Privacy() as ViewResult;

            // Assert
            Assert.AreEqual("Your Privacy is our concern", result.ViewData["Message"]);
        }

        [TestMethod]
        public void show_ViewData_genre_test()
        {
            //arrange
            var bookRepository = Mock.Create<IRepository<Book>>();
            HomeController controller = new HomeController(bookRepository, null);

            //act
            var result = controller.IndexG("Fiction") as ViewResult;

            //assert
            Assert.AreEqual("Fiction", result.ViewData["Genre"]);
        }

        [TestMethod]
        public void test_book_by_genre()
        {
            //arrange
            var bookRepository = Mock.Create<IRepository<Book>>();

            Mock.Arrange(() => bookRepository.GetAll())
             .Returns(new List<Book>()
             {
                new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                new Book { Genre="Science Fiction", ID=3, Name="Escape from the Vortex", Price=12.50m},
                new Book { Genre="History", ID=4, Name="The Battle of the Somme", Price=22m},
             }).MustBeCalled();

            HomeController controller = new HomeController(bookRepository, null);

            //act
            ViewResult viewResult = controller.IndexG(null) as ViewResult;
            var model = viewResult.Model as IEnumerable<Book>;

            //assert
            Assert.AreEqual(4, model.Count());

        }


       
    }
}
