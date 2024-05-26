using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace YourNamespace.Controllers;

public class ClientsController : ControllerBase
{
    private readonly TripService _tripService;

    public ClientsController(TripService tripService)
    {
        _tripService = tripService;
    }

    [HttpDelete("/api/clients/{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var result = await _tripService.DeleteClientAsync(idClient);
        if (!result)
        {
            return BadRequest("Could not delete client.");
        }

        return NoContent();
    }
}