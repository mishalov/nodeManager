using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nodeManager.Controller;

namespace nodeManager.Controllers
{
    public class ProgressReporter : IProgress<JSONMessage>
    {
        public void Report(JSONMessage json)
        {
            if (json != null && json.Progress != null)
            {
                Console.WriteLine($"progress: {json.Progress.Current}");
            }

        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {


        [HttpPost("{UserId}")]
        [HttpGet("{id}")]
        public async Task<JsonResult> Test(int UserId, [FromBody] CreateContainerPOSTParameters param)
        {
            try
            {
                string FileBase64 = param.FileBase64;
                byte[] data = Convert.FromBase64String(FileBase64);
                string decodedString = Encoding.UTF8.GetString(data);
                string programDirectory = $"/home/evgenii/app/{UserId}";
                Directory.CreateDirectory(programDirectory);
                FileWorker.Copy("/home/evgenii/ServerTemplates/NodeJs", programDirectory);
                System.IO.File.WriteAllText($"{programDirectory}/program.js", decodedString);
                NodeJsContainer container = new NodeJsContainer(UserId.ToString(), $"/home/evgenii/app/{UserId}");
                bool isCreated = await container.Create();
                bool isStarted = await container.Start();
                ContainerLogic.Containers.Add(container);
                Logger.Success($"Container is created. Count of containers : {ContainerLogic.CountOfContainers}");
                return new JsonResult(Client.Images);
            }
            catch (Exception e)
            {
                Logger.Fail(e.Message);
                return new JsonResult(e.Message);
            }
        }
    }
}