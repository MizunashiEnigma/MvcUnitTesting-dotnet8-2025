using MvcUnitTesting_dotnet8.Controllers;
using MvcUnitTesting_dotnet8.Models;

namespace MvcUnitTesting_dotnet8
{
    public class DbSeeder
    {
        private readonly BookDbContext _ctx;
        private readonly IWebHostEnvironment _environment;

        public DbSeeder(BookDbContext ctx, IWebHostEnvironment environment)
        {
            _ctx = ctx; 
            _environment = environment;
        }
        public void Seed()
        {
            _ctx.Database.EnsureCreated();
            if (!_ctx.Books.Any())
            {
                _ctx.Books.AddRange(new List<Book>()
                {
                   new Book { Genre="Fiction", ID=1, Name="Moby Dick", Price=12.50m},
                   new Book { Genre="Fiction", ID=2, Name="War and Peace", Price=17m},
                   new Book { Genre="Science Fiction", ID=1, Name="Escape from the vortex", Price=12.50m},
                   new Book { Genre="History", ID=2, Name="The Battle of the Somme", Price=22m},
                }
                );
                _ctx.SaveChanges();
            }
        }
    }
}
