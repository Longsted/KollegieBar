
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

    public virtual DataTransferObject.Model.Product? GetProduct(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid product Id");
        }
        
        var product = _context.Products.Find(id);

        if (product == null)
        {
            throw new ArgumentNullException("Product not found");
        }

        return ProductMapper.Map(product);
    }

    public virtual void Update(DataTransferObject.Model.Product dto)
    {
        var existing = _context.Products.Find(dto.Id);
        if (existing == null)
        {
            throw new NullReferenceException("Product not found");
        }
        existing.Name = dto.Name;
        existing.CostPrice = dto.CostPrice;
        existing.StockQuantity = dto.StockQuantity;
        _context.SaveChanges();
    }

    public void AddProduct(DataTransferObject.Model.Product product)
    {
        _context.Products.Add(ProductMapper.Map(product));
        _context.SaveChanges();
    }
    
    public virtual void Create(DataTransferObject.Model.Product dto) 
    {
        _context.Products.Add(ProductMapper.Map(dto));
        _context.SaveChanges();
    }
    
    public virtual void UpdateStock(int productId, int newTotalStock)
    {
        var product = _context.Products.Find(productId);
        if (product != null)
        {
            
            product.StockQuantity = newTotalStock;
            _context.SaveChanges();
        }
    }

    public virtual List<DataTransferObject.Model.Product> GetAllProducts()
    {
        return _context.Products
            .ToList()
            .Select(ProductMapper.Map)
            .ToList();
    }

    public virtual void UpdateMaxStock(int productId, int newMaxStock)
    {
        var product = _context.Products.Find(productId);
        if(product != null)
            product.MaxStockQuantity = newMaxStock;
        _context.SaveChanges();
    }
    public virtual void UpdateMinStock(int productId, int newMinStock)
    {
        var product = _context.Products.Find(productId);
        if(product != null)
            product.MinStockQuantity = newMinStock;
        _context.SaveChanges();
    }
    
}