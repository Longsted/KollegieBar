using DataTransferObject.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    internal class UserContext : DbContext
    {
        public UserContext() : base()//Husk base("users")
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Server=SERVERNAME;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
        */
    }
    
}

