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
            using (_logger.BeginScope(new[] { new KeyValuePair<string, object>("kbn", "2") }))
            {
                _logger.LogDebug("システムメッセージだぜ");
            }
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

    //class MyLogEvent : IEnumerable<KeyValuePair<string, object>>
    //{
    //    List<KeyValuePair<string, object>> _properties = new List<KeyValuePair<string, object>>();

    //    public string Message { get; }

    //    public MyLogEvent(string message)
    //    {
    //        Message = message;
    //    }

    //    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    //    {
    //        return _properties.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

    //    public MyLogEvent AddProp(string name, object value)
    //    {
    //        _properties.Add(new KeyValuePair<string, object>(name, value));
    //        return this;
    //    }

    //    public static Func<MyLogEvent, Exception, string> Formatter { get; } = (l, e) => l.Message;
    //}
}
