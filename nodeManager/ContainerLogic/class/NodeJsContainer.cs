using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public class NodeJsContainer : DockerContainer
    {
        public string UserId = "";
        public string FilePath = "";
        override public string Id
        {
            get;
            set;
        }
        public NodeJsContainer(string UserId, string FilePath)
        {
            if (UserId == "" || FilePath == "")
            {
                throw new Exception($"One of the initial fields is empty! UserId : {UserId}, FilePath : {FilePath} ");
            }
            if (!Directory.Exists(FilePath))
            {
                throw new Exception($"File doesnt exists! UserId : {UserId}, FilePath : {FilePath} ");
            }
            this.UserId = UserId;
            this.FilePath = FilePath;
        }

        override public async Task<bool> Create()
        {
            var Image = Client.Images.Where(image => image.RepoTags.IndexOf("node:8") != -1).First();
            string Port = (3000 + ContainerLogic.CountOfContainers).ToString();
            Logger.Success($"Predicted port is {Port}");
            var ContainerParams = new CreateContainerParameters
            {
                Image = Image.ID,
                Tty = false,
                Shell = new List<string> { "/bin/bash -c" },
                AttachStdin = true,
                AttachStdout = true,
                AttachStderr = true,
                Env = new List<string>() { $"PORT={Port}" },
                WorkingDir = "/home/node/",
                HostConfig = new HostConfig
                {
                    NetworkMode = "host",
                    PublishAllPorts = true,
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                        {
                            { Port, new List<PortBinding> { new PortBinding() { HostIP = "http://0.0.0.0", HostPort = Port } } }
                        },
                    Binds = new List<string>() { $"{FilePath}:/home/node/" }
                },
                Cmd = new List<string>() { "/bin/bash", "build.sh" },
            };
            try
            {
                var container = await Client.Docker.Containers.CreateContainerAsync(ContainerParams);
                Id = container.ID;
                return true;
            }
            catch (Exception e)
            {
                Logger.Fail($"Cant create the container: {e.Message}");
                return false;
            }
        }

        override public async Task<bool> Start()
        {
            if (Id == "")
            {
                throw new Exception("Cant start container: ID is Empty");
            }
            try
            {
                await Client.Docker.Containers.StartContainerAsync(Id, new ContainerStartParameters() { });
                Logger.Success($"Container is UP!");
                return true;
            }
            catch (Exception e)
            {
                Logger.Fail($"Container could not get run! {e.Message}");
                return false;
            }
        }

        public override async Task<bool> Remove()
        {
            try
            {
                await Client.Docker.Containers.RemoveContainerAsync(Id, new ContainerRemoveParameters()
                {
                    Force = true
                });
                Logger.Success($"Container {Id} was successfully removed");
                return true;

            }
            catch (Exception e)
            {
                Logger.Fail($"Cant remove container {Id} : {e.Message}");
                return false;
            }

        }
    }
}
