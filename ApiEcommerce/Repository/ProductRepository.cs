using System;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using ApiEcommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

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
        if (product == null)
        {
            return false;
        }
        _db.Products.Remove(product);
        return Save();
    }

    public Product? GetProduct(int id)
    {
        if (id < 0) return null;
        return _db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
    }

    public ICollection<Product> GetProducts()
    {
        return _db.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();
    }

    public ICollection<Product> GetProductsForCategory(int categoryId)
    {
        if (categoryId <= 0)
        {
            return new List<Product>();
        }
        return _db.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).OrderBy(p => p.Name).ToList();

    }

    public bool ProductExist(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }
        return _db.Products.Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim());
    }
    public bool ProductExist(int id)
    {
        if (id <= 0)
        {
            return false;
        }
        return _db.Products.Any(p => p.ProductId == id);
    }
    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;

    }

    public ICollection<Product> SearchProducts(string searchTeam)
    {
        IQueryable<Product> query = _db.Products;
        var searchTeamLowe = searchTeam.ToLower().Trim();
        if (!string.IsNullOrEmpty(searchTeam))
        {
            query = query.Include(p => p.Category).Where(p => p.Name.ToLower().Trim().Contains(searchTeamLowe) || p.Description.ToLower().Trim().Contains(searchTeamLowe));
        }
        return query.OrderBy(p => p.Name).ToList();
    }

    public bool UpdateProduct(Product product)
    {
        if (product == null) return false;
        product.UpdateDate = DateTime.Now;
        _db.Products.Update(product);

        return Save();
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
}
