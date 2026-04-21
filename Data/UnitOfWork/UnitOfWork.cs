using Data.Context;
using Data.Interfaces;
using Data.Repositories;

namespace Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;


    public IProductRepository Products { get; }
    public IUserRepository Users { get; }
    public ISalesRepository Sales { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        Products = new ProductRepository(_dbContext);
        Users = new UserRepository(_dbContext);
        Sales = new SaleRepository(dbContext);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}