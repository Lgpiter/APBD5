namespace WebApplication1.Models;

public class Trip
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public ICollection<Client_Trip> Client_Trips { get; set; }
    public ICollection<Country_Trip> Country_Trips { get; set; } 
}