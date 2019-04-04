using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public static class ServerLogic
    {
        public static SwarmManagerNode Manager = null;
        public static async Task<SwarmInspectResponse> GetSwarmInfoAsync()
        {
            var swarmInfo = await Client.Docker.Swarm.InspectSwarmAsync();
            return swarmInfo;
        }

        public static async Task<string> GetSwarmWorkerTokenAsync()
        {
            var swarmInfo = await Client.Docker.Swarm.InspectSwarmAsync();
            return swarmInfo.JoinTokens.Worker;
        }

        public static bool InitSwarmManagerNode(SwarmManagerNode node)
        {
            if (node.Ip == "" || node.Port == "") throw new Exception($"Cant add Swarm manager node! Port or IP is empty: {node.Ip}:{node.Port}");
            Manager = node;
            return true;
        }

        public static async Task<bool> JoinToSwarmAsync()
        {
            if (Manager == null) throw new Exception($"Cant join to swarm! Manager info is empty!");
            await Client.Docker.Swarm.JoinSwarmAsync(new SwarmJoinParameters()
            {
                JoinToken = Manager.Token,
                ListenAddr = "0.0.0.0:2377",
                RemoteAddrs = new List<string>() { ServerLogic.Manager.ToString() }
            });
            return true;
        }
    }

}