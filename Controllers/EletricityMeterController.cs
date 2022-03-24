using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EletricityMeterController : ControllerBase
    {
        // GET: api/EletricityMeter
        [HttpGet]
        public IActionResult Get(string meterNum, string value, string timeStamp)
        {
           var csvString = string.Join(",", meterNum, value, timeStamp );
           System.IO.File.AppendText(csvString);
		   return Ok(csvString);
        }
    }
}
