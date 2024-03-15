
using Epicor.Core;
using System.Collections;

namespace Epicor.Infraestructure.Intefaces
{
    public  interface IQueueService
    {
       Task<int> TotalSupportCallOpenAsync();
        Task<List<Queues>> TotalActivesOpenByQueueAsync();
        List<Queues> TotalActivesOpenByQueue();
    }
}
