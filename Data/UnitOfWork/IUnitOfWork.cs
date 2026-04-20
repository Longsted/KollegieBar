using Data.Interfaces;

namespace Data.UnitOfWork;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IUserRepository Users { get; }

    Task SaveChangesAsync();
}