using Data.Context;
using DataTransferObject.Model;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql("Host=localhost;Port=5433;Username=postgres;Password=1234;Database=appdb")
    .Options;

using var context = new AppDbContext(options);

DatabaseInitializer.InitializeDatabase(context);

// Tilføj bruger
context.Users.Add(new User { UserName = "Test" });
context.SaveChanges();

// Hent og print
var users = context.Users.ToList();

foreach (var user in users)
{
    Console.WriteLine(user.UserName);
}