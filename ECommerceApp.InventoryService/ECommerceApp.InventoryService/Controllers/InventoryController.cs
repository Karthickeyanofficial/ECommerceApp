using ECommerceApp.InventoryService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.InventoryService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly InventoryDbContext _db;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(
        InventoryDbContext db,
        ILogger<InventoryController> logger)
    {
        _db = db;
        _logger = logger;
    }

    // GET: api/inventory/items
    [HttpGet("items")]
    public async Task<IActionResult> GetInventoryItems()
    {
        var items = await _db.InventoryItems
            .AsNoTracking()
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/inventory/reservations
    [HttpGet("reservations")]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _db.InventoryReservations
            .AsNoTracking()
            .Include(r => r.Items)
            .ToListAsync();

        return Ok(reservations);
    }

    // GET: api/inventory/reservation-items
    [HttpGet("reservation-items")]
    public async Task<IActionResult> GetReservationItems()
    {
        var items = await _db.InventoryReservationItems
            .AsNoTracking()
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/inventory/transactions
    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _db.InventoryTransactions
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(transactions);
    }
}