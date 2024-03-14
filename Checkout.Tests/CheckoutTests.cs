using Checkout;
namespace Checkout.Tests;

public class Tests
{
    private Catalogue _catalogue;
    private Checkout _checkout;

    [SetUp]
    public void Setup()
    {
        _catalogue = new Catalogue();
        _catalogue.Items = [
            new Item("A", 50, new SpecialPrice("3 for 130", 3, 130)),
            new Item("B", 30, new SpecialPrice("2 for 45", 2, 45)),
            new Item("C", 20),
            new Item("D", 15),
            ];
        _checkout = new Checkout(_catalogue);
    }

    [Test]
    public void ReturnsZeroIfNothingScanned()
    {
        int result = _checkout.GetTotalPrice();
        if (result == 0)
        {
            Assert.Pass("Success");
        }
        else
        {
            Assert.Fail("Failed");
        }
    }

    [Test]
    public void Return50IfOneItemPassed()
    {
        _checkout.Scan("A");
        int result = _checkout.GetTotalPrice();
        if (result == 50)
        {
            Assert.Pass("Success");
        }
        else
        {
            Assert.Fail("Failed");
        }
    }

    [Test]
    public void Return115()
    {
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("C");
        _checkout.Scan("D");

        int result = _checkout.GetTotalPrice();
        if (result == 115)
        {
            Assert.Pass("Success");
        }
        else
        {
            Assert.Fail("Failed! Get result: " + result);
        }
    }

    [Test]
    public void Return130()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");

        int result = _checkout.GetTotalPrice();
        if (result == 130)
        {
            Assert.Pass("Success");
        }
        else
        {
            Assert.Fail("Failed! Get result: " + result);
        }
    }


    [Test]
    public void ReturnIncludingSpecialPrice()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("A");
        _checkout.Scan("C");
        _checkout.Scan("D");

        int result = _checkout.GetTotalPrice();
        if (result == 130 + 30 + 20 + 15)
        {
            Assert.Pass("Success");
        }
        else
        {
            Assert.Fail("Failed! Get result: " + result);
        }
    }

    [Test]
    public void ReturnsCorrectTotal()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("C");
        _checkout.Scan("D");

        int result = _checkout.GetTotalPrice();
        if (result == 130 + 50 + 45 + 30 + 20 + 15)
        {
            Assert.Pass("Success");
        }
        else
        {
            Assert.Fail("Failed! Get result: " + result);
        }
    }
}
