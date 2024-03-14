using System.Collections;

namespace Checkout;

public class Checkout : ICheckout
{
    private Catalogue _catalogue;
    public List<Item> scanned = new();
    public Dictionary<string, List<Item>> specialGroups { get; set; }
    public Hashtable hashtable { get; set; }

    public Checkout(Catalogue catalogue)
    {
        _catalogue = catalogue;
        this.hashtable = new Hashtable();
    }

    public int GetTotalPrice()
    {
        if (scanned.Count <= 0) return 0;
        int result = 0;
        foreach (var item in scanned)
        {
            if (!hashtable.Contains(item.sku))
            {
                hashtable.Add(item.sku, 1);
            }
            else
            {
                hashtable[item.sku] = (int)hashtable[item.sku]! + 1;
            }
        }

        foreach (DictionaryEntry item in hashtable)
        {
            System.Console.WriteLine(item);
            var catalogItem = _catalogue.Items.FirstOrDefault(element => element.sku == (string)item.Key);
            var regularPrice = catalogItem.price;
            var scannedAmount = (int)item.Value!;

            if (catalogItem.specialPrice != null && scannedAmount >= catalogItem.specialPrice.amount)
            {
                var specialPriceAmount = catalogItem.specialPrice.amount;
                var specialPrice = catalogItem.specialPrice.specialPrice;
                var specialPriceTotal = (int)(Math.Floor((decimal)scannedAmount / (decimal)specialPriceAmount) * (decimal)specialPrice);
                result += specialPriceTotal;
                if (scannedAmount % specialPriceAmount != 0)
                {
                    var leftover = scannedAmount % specialPriceAmount * regularPrice;
                    result += leftover;
                }
            }
            else
            {
                result += scannedAmount * regularPrice;
            }
        }

        return result;
    }

    public void Scan(string item)
    {
        if (_catalogue.Items.Length == 0) throw new Exception("Set catalogue first");
        var result = _catalogue.Items.FirstOrDefault(element => element.sku == item);
        if (result != null)
        {
            scanned.Add(result);
        }
        else
        {
            throw new Exception("Item not found in a catalogue");
        }
    }
}
