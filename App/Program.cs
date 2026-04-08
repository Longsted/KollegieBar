using Data.Context;
using Domain.Entities;

using var context = new AppDbContextFactory().CreateDbContext(null);

// INSERT
context.Users.Add(new User { Name = "Choomba" });
context.SaveChanges();

// READ
var users = context.Users.ToList();

foreach (var user in users)
{
    Console.WriteLine(user.Name);
}