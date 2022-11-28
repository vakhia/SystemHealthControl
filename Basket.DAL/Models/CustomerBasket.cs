namespace Basket.DAL.Models;

public class CustomerBasket
{
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; }

    public CustomerBasket()
    {
    }

    public CustomerBasket(string id)
    {
        Id = id;
    }
}