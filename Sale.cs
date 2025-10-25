using System;

[Serializable]
public class Sale
{
    public Vehicle Vehicle { get; set; }
    public string BuyerName { get; set; }
    public string BuyerContact { get; set; }
    public DateTime SaleDate { get; set; }

    public Sale(Vehicle vehicle, string buyerName, string buyerContact)
    {
        Vehicle = vehicle;
        BuyerName = buyerName;
        BuyerContact = buyerContact;
        SaleDate = DateTime.Now;
    }
}
