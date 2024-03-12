using System.Windows;
using System.Windows.Input;

namespace Epicor.App
{

    public partial class MainWindow : Window
    {
        private bool _isMouseDown = false;
        private Point _initialMousePosition;
        public MainWindow()
        {
            InitializeComponent();
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
