using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace nodeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        [HttpGet]
        public async Task<JsonResult> GetSwarmNodeInfo()
        {
            return new JsonResult(await ServerLogic.GetSwarmInfoAsync());
        }

        [HttpPost]
        public async Task<JsonResult> JoinSwarmNode([FromBody] SwarmManagerNode ManagerInfo)
        {
            Logger.Success("Request to join swarm node!");
            ServerLogic.InitSwarmManagerNode(ManagerInfo);
            try
            {
                bool isSuccess = await ServerLogic.JoinToSwarmAsync();
                if (isSuccess) Logger.Success("Successfuly joined to swarm!"); else Logger.Fail("Cant join to swarm!");
                return new JsonResult(await ServerLogic.GetSwarmInfoAsync());
            }
            catch (Exception e)
            {
                Logger.Fail($"Cant join to swarm: {e.Message}");
                return new JsonResult(e.Message);
            }
        }
    }
}