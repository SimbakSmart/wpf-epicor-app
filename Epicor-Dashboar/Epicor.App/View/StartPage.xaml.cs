
using Epicor.App.View.Controls;
using Epicor.Infraestructure.Services;
using System.Windows;
using System.Windows.Controls;


namespace Epicor.App.View
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {

        private UserControl usc = null;
        public StartPage()
        {
            InitializeComponent();
            //usc = new QueueControl();
            //GridMain.Children.Add(usc);
            //MainTitle.Text = "ANÁLISIS DE  REPORTES QUEUE";

            SetTotal();
        }

        private async Task SetTotal()
        {
            QueueService qs = new QueueService();

            int total = await qs.GetTotalAsync();
            TotalCount.Content = total.ToString();
        }

        private void ListViewMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            GridMain.Children.Clear();
            string _title=string.Empty;
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemQueu":
                    usc = new QueueControl();
                    _title ="ANÁLISIS DE  REPORTES QUEUE";
                    break;
                case "ItemUsers":
                    usc = new UsersControl();
                    _title = "ANÁLISIS DE  REPORTES USUARIOS";
                    break;
                default:
                    usc = new QueueControl();
                    _title = "ANÁLISIS DE  REPORTES QUEUE";
                    break;
            }
            MainTitle.Text = _title;
            GridMain.Children.Add(usc);
            RootDrawerHost.IsLeftDrawerOpen = false;
        }
    }
}
