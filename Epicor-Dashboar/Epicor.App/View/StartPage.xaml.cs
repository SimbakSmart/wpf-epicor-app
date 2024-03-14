
using Epicor.App.View.Controls;
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
            usc = new QueueControl();
            GridMain.Children.Add(usc);
        }

        private void ListViewMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemQueu":
                    usc = new QueueControl();
                    break;
                case "ItemUsers":
                    usc = new UsersControl();
                    break;
                default:
                    usc = new QueueControl();
                    break;
            }
            GridMain.Children.Add(usc);
            RootDrawerHost.IsLeftDrawerOpen = false;
        }
    }
}
