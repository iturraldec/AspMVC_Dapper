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

    public async Task<ActionResult> Index()
    {
        IEnumerable<Category> categories;

        using (var connection = new SqlConnection(_connectionString)) 
        {    
            var sql = "SELECT CategoryId, CategoryName, Description FROM Categories Order By CategoryName";      
            categories = await connection.QueryAsync<Category>(sql);
        }
        
        IEnumerable<SelectListItem> items = from value in categories
                                            select new SelectListItem(value.CategoryName, value.CategoryId.ToString());

        return View(items);
    }

    [HttpGet("GetProductos/{idCategoria}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductos([FromRoute] int idCategoria)
    {
        IEnumerable<Product> products;

        using (var connection = new SqlConnection(_connectionString)) 
        {    
            var sql = @"SELECT ProductId, CategoryId, ProductName, QuantityPerUnit, UnitPrice 
                        FROM Products 
                        WHERE CategoryId = @IdCategoria 
                        Order By ProductName";
            products = await connection.QueryAsync<Product>(sql, new {IdCategoria = idCategoria});
        }

        if (products == null || !products.Any())
        {
            return NotFound();
        }

        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult AddProduct([FromBody] ProductRequest productRequest)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var sql = "INSERT INTO Products (CategoryId, ProductName, QuantityPerUnit, UnitPrice) VALUES (@CategoryId, @Name, @Unit, @Price)";
		    var rowsAffected = connection.Execute(sql, productRequest);
	    }

        return CreatedAtAction(nameof(GetProductos), new { idCategoria = productRequest.CategoryId }, productRequest);
    }
}