using System.ComponentModel.DataAnnotations.Schema;
namespace MyApi.models;
public class Staff
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { set; get; }
    public string Username { set; get; } = string.Empty;
    public string Password { set; get; } = string.Empty;
}