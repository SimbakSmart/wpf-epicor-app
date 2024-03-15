
using CommunityToolkit.Mvvm.ComponentModel;
using Epicor.Infraestructure.Services;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Epicor.Core;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Measure;
using System.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing;

namespace Epicor.App.ViewModel
{
    public partial class QueueViewModel : ObservableObject
    {

        private QueueService qs = null;
        [ObservableProperty]
        private int _total;

        [ObservableProperty]
        private ColumnSeries<double> _userSeries;
        [ObservableProperty]
        private List<Queues> _listActivesOnQueue = null;

        [ObservableProperty]
        public ISeries[] _series;

        [ObservableProperty]
        public Axis[] _xAxes;


        public QueueViewModel()
        {
            qs = new QueueService();
            Task.Run(async () => await LoadAllTheInformationAsync());

        }

        private async Task LoadAllTheInformationAsync()
        {
            await GetTotalSupportCallOpenAsync();
            await BarGraphAsync();
            qs.Dispose();
        }

        private async Task GetTotalSupportCallOpenAsync()
        {
            Total = await qs.TotalSupportCallOpenAsync();
        }



        private async Task BarGraphAsync()
        {
            ListActivesOnQueue?.Clear();
            ListActivesOnQueue = await qs.TotalActivesOpenByQueueAsync();

            UserSeries = new ColumnSeries<double>()
            {
                Name = "Reportes Activos",
                Values = ListActivesOnQueue.Select(q => (double)q.Total).ToList(),
                Padding = 1,
                MaxBarWidth = double.PositiveInfinity,
               

            };

            Axis _axis = new Axis()
            {
                Labels = ListActivesOnQueue.Select(q => q.Name).ToList(),
                TextSize = 12,
                LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                IsVisible = true,
                LabelsRotation = -90, 
                Position = AxisPosition.Start, 
                Padding= new LiveChartsCore.Drawing.Padding(0)
            };

            Series = new ISeries[] { UserSeries };
            XAxes = new Axis[] { _axis };
        }

    }
}
