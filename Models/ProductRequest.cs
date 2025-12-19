namespace AspMVC_Dapper.Models;

public class ProductRequest
{
  public int CategoryId {get; set;}
  public string Name {get; set;} = null!;
  public string Unit {get; set;} = null!;
  public decimal Price {get; set;}
}