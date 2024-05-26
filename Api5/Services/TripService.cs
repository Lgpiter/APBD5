using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Requests;


namespace WebApplication1.Services;

public class TripService : ITripService
{
    private readonly TripDbContext _context;

    public TripService(TripDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trip>> GetTripAsync()
    {
        var trips = from trip in _context.Trips
            orderby trip.DateFrom descending
            select trip;

        return await trips.ToListAsync();
    }

    public async Task<bool> AssignClientToTripAsync(int idTrip, ClientDto clientDto)
    {
        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null)
        {
            return false;
        }

        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Pesel == clientDto.Pesel);
        if (client == null)
        {
            client = new Client
            {
                Pesel = clientDto.Pesel, 
                FirstName = clientDto.FirstName, 
                LastName = clientDto.LastName,
                Email = clientDto.Email
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

        }

        var existingAssignment = await _context.Client_Trips.SingleOrDefaultAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == idTrip);
        if (existingAssignment != null)
            return false;

        var clientTrip = new Client_Trip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.Now
        };

        _context.Client_Trips.Add(clientTrip);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);
        if (client == null)
        {
            return false;
        }

        bool hasTrips = await _context.Client_Trips.AnyAsync(ct => ct.IdClient == idClient);
        if (hasTrips)
        {
            return false;
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return true;
    }
}