using Epicor.App.Controls;
using System.Windows;
using System.Windows.Controls;

namespace Epicor.App.Views
{

    public partial class StartPage : Window
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    usc = new UserControlHome();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    usc = new UserControlQueue();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
         
            RootDrawerHost.IsLeftDrawerOpen = false;

        }
    }
}
