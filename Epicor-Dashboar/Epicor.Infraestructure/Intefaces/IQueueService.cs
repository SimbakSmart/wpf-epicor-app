
namespace Epicor.Infraestructure.Intefaces
{
    public  interface IQueueService
    {
       Task<int> GetTotalAsync();
    }
}
