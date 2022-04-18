using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace BestBuyBestPractices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);



            //Create new Department
            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new Department name");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }


            //Create new Product
            var repo2 = new DapperProductRepository(conn);

            Console.WriteLine("Type a new product Name");
            var newProductName = Console.ReadLine();

            Console.WriteLine("Type the new product's Price");
            var newProductPrice = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Type the new product's Category ID");
            var newProductCategoryID = Convert.ToInt32(Console.ReadLine());

            repo2.CreateProduct(newProductName, newProductPrice, newProductCategoryID);




            //Update a product
            Console.WriteLine("Type name of product you want to update");
            var productToBeUpdated = Console.ReadLine();

            Console.WriteLine("Type new name of product");
            var newName = Console.ReadLine();

            Console.WriteLine("Type new price of product");
            var newPrice = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Type new CategoryID");
            var newCategoryID = Convert.ToInt32(Console.ReadLine());

            repo2.UpdateProduct(productToBeUpdated, newName, newPrice, newCategoryID);


            //Delete a product
            Console.WriteLine("Type the Product ID of the product you would like to delete");
            var productToBeDeleted = Convert.ToInt32(Console.ReadLine());

            repo2.DeleteProduct(productToBeDeleted);


            var products = repo2.GetAllProducts();

            foreach (var product in products)
            {
                Console.WriteLine(product.Name, product.Price, product.CategoryID);
            }

        }
    }
}
