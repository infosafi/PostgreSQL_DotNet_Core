using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PostgreSQL_DotNet_Core.DBContext;
using PostgreSQL_DotNet_Core.Helper;
using System.Data;
using System.Reflection.Emit;

namespace PostgreSQL_DotNet_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly SPProcessAccess? _spProcessAccess;
        public MyController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("PostgresConnection");
            _spProcessAccess = new SPProcessAccess(connectionString);
        }

        [HttpGet("all-gen-info")]
        public async Task<ActionResult> CallPostgresFunction()
        {
            var accgeninf = await _context.accgeninf             
              .ToListAsync();

            return Ok(accgeninf);
        }

        [HttpGet("get-reports")]
        public async Task<ActionResult> CallPostgresFunctionReports(string comcod, string label, string fromdate, string todate)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("comcod", comcod);
            map.Add("label", "TB_COMPANY_" + label);
            map.Add("fromdate", fromdate);
            map.Add("todate", todate);

            DataSet? ds = _spProcessAccess.ExecutePostgresFunction("sp_report_accounts_tb_final01", map);


            var accgeninf = await _context.accgeninf
              .ToListAsync();

            return Ok(ds);
        }
    }
}
