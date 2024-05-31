namespace Models;

public class MotorbikeTaxAmount : BaseTaxAmount
{
    public int MotorbikeId { get; set; }
    public Motorbike? Motorbike { get; set; }
}