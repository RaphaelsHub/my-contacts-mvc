namespace MyContact.Mvc.Models;

public class Contact
{
    public int Id { get; set; } = Guid.NewGuid().GetHashCode();
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
}