using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public abstract class DockerContainer
    {
        public abstract string Id
        {
            get;
            set;
        }
        public abstract Task<bool> Create();
        public abstract Task<bool> Start();
        public abstract Task<bool> Remove();
        //public abstract Task<bool> ExecuteInitialCommand();
    }
}
