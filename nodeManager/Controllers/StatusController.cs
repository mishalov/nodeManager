using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Docker.DotNet;
using System.IO;
using Docker.DotNet.Models;

namespace nodeManager.Controllers
{

    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {

        [HttpGet]
        public JsonResult Get()
        {
            StatusState statusState = StatusLogic.CheckStatus();
            return new JsonResult(statusState);
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
