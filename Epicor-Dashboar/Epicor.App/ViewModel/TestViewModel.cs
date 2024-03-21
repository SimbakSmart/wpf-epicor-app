

using CommunityToolkit.Mvvm.ComponentModel;
using Epicor.Core.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.VisualElements;
using Epicor.Infraestructure.Services;
using System.Drawing;

namespace Epicor.App.ViewModel
{
    public partial class TestViewModel : ObservableObject
    {
        private QueueServices qs = null;

        [ObservableProperty]
        public IEnumerable<ISeries> _series;

        [ObservableProperty]
        public LabelVisual _title;


        [ObservableProperty]
        private List<Queues> _listUrgency = null;


        public TestViewModel()
        {
             qs = new QueueServices();
            PieCharAsync();
        }
        //private Color[] sliceColors = { SKColor.Red, SKColor.Blue, SKColor.Green, SKColor.Orange, SKColor.Purple };

        //private SKColor GetSliceColor(int index)
        //{
        //    return sliceColors[index % sliceColors.Length];
        //}
        private async void PieCharAsync()
        {

            // you can convert any array, list or IEnumerable<T> to a pie series collection:
            //Series  =new[] { 2, 4, 1, 4, 3 }.AsPieSeries();


            int _index = 0;
            ListUrgency = await qs.GetTotalsByUrgencyAsync();
            string[] _urgencyArray = ListUrgency.Select(q => q.Urgency).ToArray();
            double[] _totalUrgencyArray = ListUrgency.Select(q => (double)q.Total).ToArray();
            //List<double> valueList = new List<double>() { _totalUrgencyArray[_index++ % _totalUrgencyArray.Length] };

            Series = _totalUrgencyArray.AsPieSeries((value, series) =>
            {
                series.Name = _urgencyArray[_index++ % _urgencyArray.Length]+" "+value.ToString(); 
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle;
                series.DataLabelsSize = 20;
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(0, 0, 0));
                //series.Fill = new SolidColorPaint(new SKColor(255, 255, 255));

            });

            //Series = _totalUrgencyArray.AsPieSeries((value, series) =>
            //{
            //    series.Name = _urgencyArray[_index++ % _urgencyArray.Length];
            //   // series.Values = valueList;
            //    // series.DataLabelsFormatter = point => point.PrimaryValue.ToString("N2") + " elements";

            //});

                    Title =  new LabelVisual
                    {
                        Text = "My chart title",
                        TextSize = 25,
                        Padding = new LiveChartsCore.Drawing.Padding(15),
                        Paint = new SolidColorPaint(SKColors.DarkSlateGray)
                    };



        }



    }
}
