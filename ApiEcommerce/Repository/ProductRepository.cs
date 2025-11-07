using System;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using ApiEcommerce.Repository.IRepository;

namespace ApiEcommerce.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateProduct(Product product)
    {
        product.CreationDate = DateTime.Now;
        _db.Products.Add(product);
        return Save();
    }

    public bool DeleteProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Product? GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public ICollection<Product> GetProducts()
    {
        throw new NotImplementedException();
    }

    public ICollection<Product> GetProductsForCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public bool ProductExists(string name)
    {
        return _db.Products.Any(p => p.Name == name);
    }
    public bool ProductExist(int id)
    {
        return _db.Products.Any(p => p.ProductId == id);
    }
    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;

    }

    public ICollection<Product> SearchProduct(string name)
    {
        throw new NotImplementedException();
    }

    public bool UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public bool BuyProduct(string name, int cant)
    {
        if (string.IsNullOrEmpty(name) || cant <= 0) return false;
        var product = _db.Products.FirstOrDefault(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
        if (product == null || product.Stock < cant) return false;
        product.Stock -= cant;
        _db.Products.Update(product);
        return Save();
    }
    public bool ProductExists(int id)
    {
        throw new NotImplementedException();
    }
}
