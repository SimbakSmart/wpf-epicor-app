
using CommunityToolkit.Mvvm.ComponentModel;
using Epicor.Infraestructure.Services;

namespace Epicor.App.ViewModel
{
    public partial class QueueViewModel : ObservableObject
    {

        [ObservableProperty]
        private int _total;

        public QueueViewModel()
        {
           
            Task.Run(async () => await GetTotalAsync());


        }

        private async Task GetTotalAsync()
        {
            QueueService qs = new QueueService();

            Total = await qs.GetTotalAsync();
         
        }
    }
}
