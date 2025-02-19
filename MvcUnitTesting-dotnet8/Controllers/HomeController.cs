using Microsoft.AspNetCore.Mvc;
using MvcUnitTesting_dotnet8.Models;
using System.Diagnostics;
using Tracker.WebAPIClient;

namespace MvcUnitTesting_dotnet8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRepository<Book> repository;

        public HomeController(IRepository<Book> bookRepo, ILogger<HomeController> logger)
        {
            ActivityAPIClient.Track(StudentID: "S00237686", StudentName: "Tristan Cawley", activityName: "Rad302 2025 Week 2 Lab 1",Task:"Running Initial Tests");
            repository = bookRepo;
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            var books = repository.GetAll();
            return View(books);
        }
        public IActionResult IndexG(string genre = null)
        {
            ViewData["Genre"] = genre;
            var books = repository.GetByGenre(genre);

            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre == genre).ToList();
            }
            ViewData["Genre"] = genre ?? "All Books";
            return View(books);
        }

        public IActionResult Privacy()
        {
            ViewData["Message"] = "Your Privacy is our concern";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
