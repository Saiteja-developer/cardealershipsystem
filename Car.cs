using System;

[Serializable]
public class Car : Vehicle
{
    public string Type { get; set; }

    public Car(string make, string model, int year, string color, double price, string type)
        : base(make, model, year, color, price)
    {
        Type = type;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Car Details: {Make} {Model}");
    }
}
