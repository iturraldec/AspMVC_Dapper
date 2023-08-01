using Microsoft.AspNetCore.Mvc;
using AspMVC_Dapper.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspMVC_Dapper.Controllers;

public class HomeController : Controller
{
    private readonly string _connectionString;

    public HomeController()
    {
        _connectionString = "Server=localhost,1433; Database=Northwind; User ID=sa; Password=J1z01234_; Encrypt=False;";
    }

    public ActionResult Index()
    {
        var categories = new List<Category>();

        using (var connection = new SqlConnection(_connectionString)) 
        {    
            var sql = "SELECT CategoryId, CategoryName, Description FROM Categories Order By CategoryName";      
            categories = connection.Query<Category>(sql).ToList(); 
        }
        
        IEnumerable<SelectListItem> items = from value in categories
                                            select new SelectListItem(value.CategoryName, value.CategoryId.ToString());

        return View(items);
    }

    public ActionResult GetProductos(int? idCategoria)
    {
        var products = new List<Product>();

        using (var connection = new SqlConnection(_connectionString)) 
        {    
            var sql = "SELECT ProductId, CategoryId, ProductName, QuantityPerUnit, UnitPrice FROM Products WHERE CategoryId = @IdCategoria Order By ProductName";
            products = connection.Query<Product>(sql, new {IdCategoria = idCategoria}).ToList(); 
        }

        return Ok(products);
    }

    [HttpPost]
    public ActionResult AddProduct([FromBody] ProductRequest productRequest)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = "INSERT INTO Products (CategoryId, ProductName, QuantityPerUnit, UnitPrice) VALUES (@CategoryId, @Name, @Unit, @Price)";
		    var rowsAffected = connection.Execute(sql, productRequest);
	    }

        return Ok(productRequest);
    }
}