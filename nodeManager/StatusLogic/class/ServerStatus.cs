using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public class ServerStatus
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ServerStatus(int Id, string Description)
        {
            this.Id = Id;
            this.Description = Description;
        }
    }
}
