using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string productName, double productPrice, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUE (@productName, @productPrice, @categoryID);",
            new { productName = productName, productPrice = productPrice, categoryID = categoryID });
        }

        public void UpdateProduct(string productUpdate, string newName, double newPrice, int newCategoryID)
        {
            _connection.Execute("UPDATE products SET name = @newName, price = @newPrice, CategoryID = @newCategoryID WHERE name = @productUpdate;",
                new { productUpdate = productUpdate, newName = newName, newPrice = newPrice, newCategoryID = newCategoryID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE productID = @productID; DELETE FROM sales WHERE productID = @productID; DELETE FROM reviews WHERE productID = @productID",
                new { productID = productID });
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }

    }
}
