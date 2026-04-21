using Data.Interfaces;

namespace Data.UnitOfWork;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IUserRepository Users { get; }
    ISalesRepository Sales { get; }

    Task SaveChangesAsync();
}