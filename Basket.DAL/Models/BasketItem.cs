namespace Basket.DAL.Models;

public class BasketItem
{
    public int Id { get; set; }
    public string MedicineName { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
}