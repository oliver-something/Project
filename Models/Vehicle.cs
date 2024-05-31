using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public abstract class Vehicle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [MaxLength(100, ErrorMessage = "Max Len 100 characters")]
    public string? VehicleType { get; set; }

    public abstract string? GetVehicleType();
}