
using Epicor.Core;
using System.Collections;

namespace Epicor.Infraestructure.Intefaces
{
    public  interface IQueueService
    {
        Task<int> TotalSupportCallOpenAsync();
        Task<List<Queues>> TotalActivesOpenByQueueAsync();
        Task<List<Queues>> TotalUrgencyOpenByQueueAsync();
        Task<List<Queues>> TotalPriorityOpenByQueueAsync();
    }
}
