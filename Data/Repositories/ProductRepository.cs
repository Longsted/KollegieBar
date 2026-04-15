
using Data.Context;
using Data.Mappers;
using DataTransferObject.Model;

namespace Data.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Create(DataTransferObject.Model.Product dto) 
    {
        _context.Products.Add(ProductMapper.Map(dto));
        _context.SaveChanges();
    }
}