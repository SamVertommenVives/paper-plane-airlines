using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;

namespace PPA.Repositories.Interfaces;

public class FlightRouteDAO : IFlightRouteDAO
{
    private readonly PPADbContext _dbContext;


    public FlightRouteDAO(PPADbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<FlightRoute>?> GetFlightRoutesAsync()
    {
        return await _dbContext.FlightRoutes.ToListAsync();
    }
}