using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models;

public class Car
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    private bool isAvailable;
    public bool IsAvailable
    {
        get { return isAvailable; }
        set { isAvailable = value; }
    }
}