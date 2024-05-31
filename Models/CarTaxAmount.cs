namespace Models;

public class CarTaxAmount : BaseTaxAmount
{
    public int CarId { get; set; }
    public Car? Car { get; set; }
}