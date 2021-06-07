using System;
using Microsoft.EntityFrameworkCore;
using DB_task9.Data;
using System.Linq;
using System.Collections.Generic;

namespace DB_task9
{
    class Program
    {
        static void Main(string[] args)
        {
            NorthwindContext context = new NorthwindContext();
            while (true)
            {
                Console.WriteLine("Введите номер задания");
                var number = Convert.ToInt32(Console.ReadLine());

                switch (number)
                {
                    case 1:
                        var CompanyNames = context.Customers.Select(x => x.CompanyName).ToList();
                        CompanyNames.ForEach(x => Console.WriteLine(x));
                        break;
                    case 2:
                        var Products = context.Products.Where(x => x.Supplier == context.Suppliers.FirstOrDefault()).ToList();
                        Products.ForEach(x => { Console.WriteLine(x.ProductName + " " + x.UnitPrice); });
                        break;
                    case 3:
                        var emp = context.Employees.Where(x => x.EmployeeId == 8).FirstOrDefault();
                        Console.WriteLine(emp.FirstName + " " + emp.LastName);
                        break;
                    case 4:
                        var catName = context.Products.Where(x => x.ProductId == 55).First();
                        Console.WriteLine(context.Categories.Where(x => x.CategoryId == catName.CategoryId).First().CategoryName);
                        break;
                    case 5:
                        context.Suppliers.Add(new Models.Supplier
                        {
                            CompanyName = "Test"
                        });
                        context.SaveChanges();
                        Console.WriteLine(context.Suppliers.Where(x => x.CompanyName == "Test").FirstOrDefault().SupplierId);
                        break;
                    case 6:
                        context.Suppliers.Add(new Models.Supplier
                        {
                            CompanyName = "Test2",
                            Products = new List<Models.Product>
                        {
                            new Models.Product {ProductName = "Product A"},
                            new Models.Product {ProductName = "Product B"}
                        }
                        });
                        context.SaveChanges();
                        var sup = context.Suppliers.Where(x => x.CompanyName == "Test2").FirstOrDefault();
                        Console.WriteLine(sup.CompanyName);
                        sup.Products.ToList().ForEach(x => Console.WriteLine(x.ProductName));
                        break;
                    case 7:
                        context.Products.Remove(context.Products.Where(x => x.ProductName == "Product A").FirstOrDefault());
                        context.SaveChanges();
                        var supp = context.Suppliers.Where(x => x.CompanyName == "Test2").FirstOrDefault();
                        Console.WriteLine(supp.CompanyName);
                        supp.Products.ToList().ForEach(x => Console.WriteLine(x.ProductName));
                        break;
                    case 8: 
                        var supplier = context.Suppliers.Where(x=> x.SupplierId == 18).First();
                        supplier.PostalCode = "12345";
                        context.SaveChanges();
                        Console.WriteLine(context.Suppliers.Where(x=> x.SupplierId == 18).First().PostalCode);
                        break;
                    case 9:
                        var empl = context.Employees.OrderBy(x => x.LastName).Skip(2).Take(5).ToList();
                        empl.ForEach(x => Console.WriteLine(x.LastName));
                        break;
                    case 10:
                        var cats = context.Categories.ToList();
                        cats.ForEach(x =>
                        {
                            Console.WriteLine(x.CategoryName + " " + x.Products.Count());
                        });
                        break;
                    case 11:

                    default:
                        break;
                }
            }

        }
    }
}
