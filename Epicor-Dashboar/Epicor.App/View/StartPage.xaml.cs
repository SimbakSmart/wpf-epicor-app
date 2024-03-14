

using Epicor.App.UC;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Epicor.App.View
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {

        private bool _isMouseDown = false;
        private Point _initialMousePosition;

        private UserControl usc = null;
        public StartPage()
        {
            InitializeComponent();
            usc = new QueueControl();
            GridMain.Children.Add(usc);
            MainTitle.Text = "ANÁLISIS DE  REPORTES QUEUE";


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

        private void ResponsiveWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            _initialMousePosition = e.GetPosition(this);
        }

        private void ResponsiveWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                Point currentMousePosition = e.GetPosition(this);
                Vector offset = currentMousePosition - _initialMousePosition;
                Left += offset.X;
                Top += offset.Y;
            }
        }

        private void ResponsiveWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
        }
    }
}
