
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Epicor.Core;
using Epicor.Core.Models;
using Epicor.Infraestructure.Helpers;
using Epicor.Infraestructure.Services;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;

namespace Epicor.App.ViewModel
{
    public partial class QueueViewModel : ObservableObject
    {


        private QueueServices qs = null;

        [ObservableProperty]
        private DateTime? _startDate;

        [ObservableProperty]
        private DateTime? _endDate;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private List<Queues> _total;


        [ObservableProperty]
        private int _granTotal;

        [ObservableProperty]
        private int _totalOpen;

        [ObservableProperty]
        private int _totalClosed;


        [ObservableProperty]
        private ColumnSeries<double> _userSeriesBar;

        [ObservableProperty]
        private List<Queues> _listBar = null;

        [ObservableProperty]
        public ISeries[] _seriesBar;

        [ObservableProperty]
        public Axis[] _xAxesBar;


        public QueueViewModel()
        {
           
            qs = new QueueServices();
            Task.Run(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
            IsLoading= true;
            await GetTotalsAsync();
            await BarGraphAsync();
            IsLoading = false;
        }
        private async Task GetTotalsAsync(FiltersParams filters = null)
        {
            if (filters != null)
            {
                Total = await qs.GetTotalsAsync(filters);
            }
            else
            {
                Total = await qs.GetTotalsAsync();
            }

            GranTotal = Total.Select(t => t.Total).FirstOrDefault();
            TotalOpen = Total.Select(t => t.TotalOpen).FirstOrDefault();
            TotalClosed = Total.Select(t => t.TotalClosed).FirstOrDefault();
        }
        private async Task BarGraphAsync(FiltersParams filters = null)
        {
            ListBar?.Clear();

            if (filters != null)
            {
                ListBar = await qs.GetTotalsByResponsableAsync(filters);
            }
            else
            {
                ListBar = await qs.GetTotalsByResponsableAsync();
            }

                      
            UserSeriesBar = new ColumnSeries<double>()
            {
                Name = "Reportes Activos",
                Values = ListBar.Select(q => (double)q.Total).ToList(),
                Padding = 1,
                MaxBarWidth = double.PositiveInfinity,


            };
            Axis _axis = new Axis()
            {
                Labels = ListBar.Select(q => q.Name).ToList(),
                TextSize = 12,
                LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                IsVisible = true,
                LabelsRotation = -90,
                Position = AxisPosition.Start,
                Padding = new LiveChartsCore.Drawing.Padding(0)
            };

            SeriesBar = new ISeries[] { UserSeriesBar };
            XAxesBar = new Axis[] { _axis };
        }

       

        [RelayCommand]
        private async Task SendRequesByDateRangeAsync()
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                var filters = new FiltersParams.FiltersParamsBuilder()
                                 .WithStartDate(StartDate.Value)
                                 .WithEndDate(EndDate.Value)
                                 .Build();
                await GetTotalsAsync(filters);
                await BarGraphAsync(filters);
                StartDate = null;
                EndDate = null;
            }
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
            await LoadDataAsync();
        }

    }
}
