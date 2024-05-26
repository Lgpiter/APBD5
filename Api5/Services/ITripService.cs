namespace WebApplication1.Services;

using WebApplication1.Models;
using WebApplication1.Requests;

public interface ITripService
{
    public Task<IEnumerable<Trip>> GetTripAsync();
    public Task<bool> AssignClientToTripAsync(int idTrip, ClientDto clientDto);

    public Task<bool> DeleteClientAsync(int idClient);
}