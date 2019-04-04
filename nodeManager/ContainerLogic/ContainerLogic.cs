using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public static class ContainerLogic
    {
        public static List<DockerContainer> Containers = new List<DockerContainer>();
        public static int CountOfContainers
        {
            get
            {
                return Containers.Count();
            }
        }
    }
}
