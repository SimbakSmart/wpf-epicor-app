using System.Windows;
using System.Windows.Controls;

namespace Epicor.App.UC
{
    /// <summary>
    /// Interaction logic for QueueControl.xaml
    /// </summary>
    public partial class QueueControl : UserControl
    {
        public QueueControl()
        {
            InitializeComponent();
        }

        //private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (e.NewSize.Width <=700)
        //    {
        //        stackPanel.Orientation = Orientation.Vertical;
        //    }
        //    else
        //    {
        //        stackPanel.Orientation = Orientation.Horizontal;
        //    }
        //}
    }
}
