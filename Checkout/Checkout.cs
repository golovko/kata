using System.Collections;

namespace Checkout;

public class Checkout : ICheckout
{
    private Catalogue _catalogue;
    private List<Item> _scannedItems = new();
    private Hashtable groupedScannedSku { get; set; }
    
    public Checkout(Catalogue catalogue)
    {
        _catalogue = catalogue;
        this.groupedScannedSku = new Hashtable();
    }

    public int GetTotalPrice()
    {
        if (_scannedItems.Count <= 0) return 0;
        int result = 0;
        
        foreach (Item item in _scannedItems)
        {
            if (!groupedScannedSku.Contains(item.sku))
            {
                groupedScannedSku.Add(item.sku, 1);
            }
            else
            {
                groupedScannedSku[item.sku] = (int)groupedScannedSku[item.sku]! + 1;
            }
        }

        foreach (DictionaryEntry item in groupedScannedSku)
        {
            Item catalogItem = _catalogue.Items.FirstOrDefault(element => element.sku == (string)item.Key)!;
            int regularPrice = catalogItem!.price;
            int scannedAmount = (int)item.Value!;

            if (catalogItem.specialPrice != null && scannedAmount >= catalogItem.specialPrice.amount)
            {
                int specialPriceAmount = catalogItem.specialPrice.amount;
                int specialPrice = catalogItem.specialPrice.specialPrice;
                int specialPriceTotal = (int)(Math.Floor((decimal)scannedAmount / (decimal)specialPriceAmount) * (decimal)specialPrice);
                result += specialPriceTotal;
                if (scannedAmount % specialPriceAmount != 0)
                {
                    int leftover = scannedAmount % specialPriceAmount * regularPrice;
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

    public void Scan(string sku)
    {
        if (_catalogue.Items.Length == 0) {
            throw new Exception("Set catalogue first");
            }
        Item? item = _catalogue.Items.FirstOrDefault(element => element.sku == sku);
        if (item != null)
        {
            _scannedItems.Add(item);
        }
        else
        {
            throw new Exception("Item not found in a catalogue");
        }
    }
}
