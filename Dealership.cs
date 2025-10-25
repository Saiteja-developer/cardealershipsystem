using System;
using System.Collections.Generic;

[Serializable]
public class Dealership
{
    public List<Vehicle> Inventory { get; set; }
    public List<Sale> SalesHistory { get; set; }

    public Dealership()
    {
        Inventory = new List<Vehicle>();
        SalesHistory = new List<Sale>();
    }

    public void AddVehicle(Vehicle vehicle)
    {
        Inventory.Add(vehicle);
    }

    public bool SellVehicle(int index, string buyerName, string buyerContact)
    {
        if (index < 0 || index >= Inventory.Count) return false;
        var vehicle = Inventory[index];
        Inventory.RemoveAt(index);
        SalesHistory.Add(new Sale(vehicle, buyerName, buyerContact));
        return true;
    }
}
