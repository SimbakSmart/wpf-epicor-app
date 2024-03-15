
using CommunityToolkit.Mvvm.ComponentModel;
using Epicor.Infraestructure.Services;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Epicor.Core;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

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

 
        [ObservableProperty]
        private IEnumerable<ISeries> _seriesUrgency;
        [ObservableProperty]
        private List<Queues> _listUrgency = null;


        [ObservableProperty]
        private IEnumerable<ISeries> _seriesPriority;
        [ObservableProperty]
        private List<Queues> _listPriority = null;

        public QueueViewModel()
        {
            qs = new QueueService();
            Task.Run(async () => await LoadAllTheInformationAsync());
            
        }

        private async Task LoadAllTheInformationAsync()
        {
            await GetTotalSupportCallOpenAsync();
            await BarGraphAsync();
            await UrgencyPieChartAsync();
            await PriorityPieChartAsync();
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
                Padding = new LiveChartsCore.Drawing.Padding(0)
            };

            Series = new ISeries[] { UserSeries };
            XAxes = new Axis[] { _axis };
        }
        private async Task UrgencyPieChartAsync()
        {
            int _index = 0;
            ListUrgency = await qs.TotalUrgencyOpenByQueueAsync();
            string[] _urgencyArray = ListUrgency.Select(q => q.Urgency).ToArray();
            double[] _totalUrgencyArray = ListUrgency.Select(q => (double)q.Total).ToArray();


            SeriesUrgency = _totalUrgencyArray.AsPieSeries((value, series) =>
            {
                series.Name = _urgencyArray[_index++ % _urgencyArray.Length];
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                series.DataLabelsSize = 20;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                //series.DataLabelsFormatter = point =>
                //{
                //    int index = (int)point.PrimaryValue;
                //    return $"{_urgencyArray[index]}";
                //};
                //series.DataLabelsFormatter =
                //   point =>
                //       $"This slide takes {point.Coordinate.PrimaryValue} " +
                //       $"out of {point.StackedValue!.Total} parts";
                //series.ToolTipLabelFormatter = point => $"{point.StackedValue!.Share:P2}";
            });
        }
        private async Task PriorityPieChartAsync()
        {
            int _index = 0;
            ListPriority = await qs.TotalPriorityOpenByQueueAsync();
            string[] _priorityArray = ListPriority.Select(q => q.Priority).ToArray();
            double[] _totalPritorityArray = ListPriority.Select(q => (double)q.Total).ToArray();


            SeriesPriority = _totalPritorityArray.AsPieSeries((value, series) =>
            {
                series.Name = _priorityArray[_index++ % _priorityArray.Length];
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                series.DataLabelsSize = 20;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
            });
        }



    }

}
