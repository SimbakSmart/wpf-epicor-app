using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Epicor.Core.Models;
using Epicor.Infraestructure.Helpers;
using Epicor.Infraestructure.Services;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Windows.Markup;

namespace Epicor.App.ViewModel
{
    public partial class QueueViewModel : ObservableObject
    {


        private QueueServices qs = null;


        [ObservableProperty]
        private bool snackBarIsActive;
        [ObservableProperty]
        private string _messageSnackBar;

        [ObservableProperty]
        private string _message;

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

        [ObservableProperty]
        private List<Queues> _listUrgency = null;

        [ObservableProperty]
        private IEnumerable<ISeries> _seriesUrgency;


        public QueueViewModel()
        {
           
            qs = new QueueServices();
            Task.Run(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
            IsLoading= true;
            await GetTotalsAsync();
            await BarGraphByResponsableAsync();
            await UrgencyPieChartAsync();
            IsLoading = false;
            qs.Dispose();
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
        private async Task BarGraphByResponsableAsync(FiltersParams filters = null)
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
                Fill = new SolidColorPaint(new SKColor(25, 118, 210, 255)),


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

        private async Task UrgencyPieChartAsync()
        {
            int _index = 0;
            ListUrgency = await qs.GetTotalsByUrgencyAsync();
            string[] _urgencyArray = ListUrgency.Select(q => q.Urgency).ToArray();
            double[] _totalUrgencyArray = ListUrgency.Select(q => (double)q.Total).ToArray();

            SeriesUrgency = _totalUrgencyArray.AsPieSeries((value, series) =>
            {
                series.Name = _urgencyArray[_index++ % _urgencyArray.Length]+" "+value.ToString() ;
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
                series.DataLabelsSize = 20;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(0, 0, 0));
                series.Values = new List<double>() {value};

            });
        }

    

        [RelayCommand]
        private async Task SendRequesByDateRangeAsync()
        {
            try
            {
                IsLoading = true;
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    var filters = new FiltersParams.FiltersParamsBuilder()
                                     .WithStartDate(StartDate.Value)
                                     .WithEndDate(EndDate.Value)
                                     .Build();
                    await GetTotalsAsync(filters);
                    await BarGraphByResponsableAsync(filters);
                    Message = $"Consulta generada del {StartDate?.Date.ToShortDateString()} al {EndDate?.Date.ToShortDateString()}";
                    StartDate = null;
                    EndDate = null;
                    IsLoading = false;
                    MessageSnackBar = "La consulta fue generada con exito";
                    SnackBarIsActive = true;
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    SnackBarIsActive = false;

                }

            }
            catch
            {

            }
            finally
            {
                IsLoading = false;
                SnackBarIsActive = false;
                qs.Dispose();
            }
           
            
        }

        [RelayCommand]
        private async Task RefreshAsync()
        {
           
            try
            {
                IsLoading = true;
                await LoadDataAsync();
                Message = string.Empty;
                IsLoading = false;
                MessageSnackBar = "Carga de todos los resultados activos";
                SnackBarIsActive = true;
                await Task.Delay(TimeSpan.FromSeconds(3));
                SnackBarIsActive = false;
            }
            catch
            {

            }
            finally
            {
               IsLoading=false;
               SnackBarIsActive = false;
                qs.Dispose();
            }
        }

    }
}
