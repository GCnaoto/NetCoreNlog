using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreNlog.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreNlog.Controllers
{
    [ServiceFilter(typeof(LogFilter))]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private  ILogger<ValuesController> _logger;
        private readonly LogDbContext _context;
        
        public ValuesController(ILogger<ValuesController> logger, LogDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogCritical("nlog is working from a controller");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogError("getにてエラー発生");
            return _context.Logs.Where(x => x.Id == id).SingleOrDefaultAsync().Result?.Message;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
