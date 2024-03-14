namespace Checkout;

public class Item
{
  public string sku;
  public int price;
  public SpecialPrice specialPrice;
  public Item(string sku, int price)
  {
    this.sku = sku;
    this.price = price;
  }
  public Item(string sku, int price, SpecialPrice specialPrice)
  {
    this.sku = sku;
    this.price = price;
    this.specialPrice = specialPrice;
  }
}

public class SpecialPrice
{
  public string name;
  public int amount;
  public int specialPrice;
  public SpecialPrice(string name, int amount, int specialPrice)
  {
    this.name = name;
    this.amount = amount;
    this.specialPrice = specialPrice;
  }
}
