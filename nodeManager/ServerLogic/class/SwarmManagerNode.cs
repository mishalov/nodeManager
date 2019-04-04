using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public class SwarmManagerNode
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Token { get; set; }

        public override string ToString()
        {
            return $"{Ip}:{Port}";
        }
    }
}
