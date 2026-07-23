using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostgreSQL_DotNet_Core.DBContext;

namespace PostgreSQL_DotNet_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("all-gen-info")]
        public async Task<ActionResult> CallPostgresFunction()
        {
            var accgeninf = await _context.accgeninf             
              .ToListAsync();

            return Ok(accgeninf);
        }
    }
}
