using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreNlog.Model;
using Microsoft.Extensions.Logging;
using NLog;

namespace AspNetCoreNlog.Controllers
{
    [Produces("application/json")]
    [Route("api/Logs")]
    public class LogsController : Controller
    {
        private readonly LogDbContext _context;

        private readonly ILogger<LogsController> _logger;

        public LogsController(LogDbContext context, ILogger<LogsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Logs
        [HttpGet]
        public IEnumerable<Logs> GetLogs()
        {
            return _context.Logs;
        }

        // GET: api/Logs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogs([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var logs = await _context.Logs.SingleOrDefaultAsync(m => m.Id == id);

            if (logs == null)
            {
                return NotFound();
            }

            return Ok(logs);
        }

        // PUT: api/Logs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogs([FromRoute] int id, [FromBody] Logs logs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != logs.Id)
            {
                return BadRequest();
            }

            _context.Entry(logs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Logs
        [HttpPost]
        public async Task<IActionResult> PostLogs([FromBody] Logs logs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Logs.Add(logs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogs", new { id = logs.Id }, logs);
        }

        // POST: api/Logs
        [HttpPost("Kbn")]
        public async Task<IActionResult> PostKbnLogs([FromBody] Logs logs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //logs.Kbn = "2";
            _context.Logs.Add(logs);
            //await _context.SaveChangesAsync();
            var logger = LogManager.GetLogger(GetType().FullName);
            LogEventInfo theEvent = new LogEventInfo(NLog.LogLevel.Debug, "", "Pass my custom value");
            theEvent.Properties["kbn"] = "2";
            logger.Log(theEvent);

            return CreatedAtAction("GetLogs", new { id = logs.Id }, logs);
        }

        // DELETE: api/Logs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogs([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var logs = await _context.Logs.SingleOrDefaultAsync(m => m.Id == id);
            if (logs == null)
            {
                return NotFound();
            }

            _context.Logs.Remove(logs);
            await _context.SaveChangesAsync();

            return Ok(logs);
        }

        private bool LogsExists(int id)
        {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}