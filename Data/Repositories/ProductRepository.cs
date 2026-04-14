
using Data.Context;
using DataTransferObject.Model;

namespace Data.Repositories;

public class ProductRepository
{
    private readonly AppDbContext _context;

    private ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public void CreateSnack(Snack snack)
    {
        _context.Products.Add(snack);
        _context.SaveChanges();
    }

    public void CreateLiquidWithAlcohol(LiquidWithAlcohol liquidWithAlcohol)
    {
        _context.Products.Add(liquidWithAlcohol);
        _context.SaveChanges();
    }

    public void CreateLiquidWithoutAlcohol(LiquidWithoutAlcohol liquidWithoutAlcohol)
    {
        _context.Products.Add(liquidWithoutAlcohol);
        _context.SaveChanges();
    }
    public void CreateConsumables(Consumables consumables)
    {
        _context.Products.Add(consumables);
        _context.SaveChanges();
    }
}