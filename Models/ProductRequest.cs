namespace AspMVC_Dapper.Models;

public class ProductRequest
{
  public int CategoryId {get; set;}
  public string Name {get; set;}
  public string Unit {get; set;}
  public decimal Price {get; set;}
}