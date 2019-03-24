using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    public static class StatusLogic
    {

        public ServerStatuses[] = {
    new ServerStatus(0, "Ожидание..."),
    new ServerStatus(1,  "ОК"),
    new ServerStatus(2, "DockerError")}
        

    public static StatusState CheckStatus()
        {
            return new StatusState(1, 1);
        }
    }
}
