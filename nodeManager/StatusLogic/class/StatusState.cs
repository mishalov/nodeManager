using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nodeManager
{
    /*
     * Статусы сервера: 
     * 0: Pending - Данные еще не получены или не могут быть получены
     * 1: OK - Все хорошо
     * 3: DockerError - проблемы с развертыванием docker контейнеров, проблемы с получением информации
     * 
     */

    public class StatusState
    {
        public ServerStatus ServerStatus { get; set; }
        public int CountOfContainersAlive { get; set; }

        public StatusState(ServerStatus ServerStatus, int CountOfContainersAlive)
        {
            this.ServerStatus = ServerStatus;
            this.CountOfContainersAlive = CountOfContainersAlive;
        }
    }
}

