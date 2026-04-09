using Microsoft.EntityFrameworkCore;
namespace Data.Context;

public class DatabaseInitializer
{
    /// <summary>
    /// Ensures that the database is created and up to date.
    /// 
    /// This method applies all pending Entity Framework migrations
    /// to the database at application startup.
    ///
    /// Why:
    /// - Keeps the database schema in sync with the code
    /// - Removes the need to manually run "dotnet ef database update"
    /// - Ensures all team members have the same database structure
    ///
    /// Important:
    /// - The database (Docker container) must be running before calling this
    /// - This should typically be called once at application startup
    /// </summary>
    public static void InitializeDatabase(AppDbContext context)
    {
        // Apply all pending migrations to the database.
        // If the database does not exist, it will be created.
        context.Database.Migrate();
    }
}