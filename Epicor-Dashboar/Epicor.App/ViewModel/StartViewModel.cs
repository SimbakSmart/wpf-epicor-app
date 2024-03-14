

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Epicor.App.View;
using System.Windows;
using System.Windows.Input;

namespace Epicor.App.ViewModel
{
    public partial class StartViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isChecked;


        public StartViewModel()
        {
            IsChecked = false;
        }


        [RelayCommand]
        private void ToggleOpen()
        {
            IsChecked = true;
        }

        [RelayCommand]
        private void ToggleClose()
        {
            IsChecked = false;
        }


        //[RelayCommand]
        //private void MaxWindow()
        //{

        //}
        // Maximize App Command
        private ICommand _maxAppCommand;
        public ICommand MaxAppCommand => _maxAppCommand ??= new RelayCommand<object>(MaxApp);



        // Maximize App
        public void MaxApp(object obj)
        {
            //StartPage win = obj as StartPage;
             StartPage win = new StartPage();
            if (win.WindowState == WindowState.Normal)
            {
                win.WindowState = WindowState.Maximized;
            }
            else if (win.WindowState == WindowState.Maximized)
            {
                win.WindowState = WindowState.Normal;
            }
        }

        //public void MaxApp(object obj)
        //{
        //    if (obj is StartPage win && win.WindowState != null)
        //    {
        //        if (win.WindowState == WindowState.Normal)
        //        {
        //            win.WindowState = WindowState.Maximized;
        //        }
        //        else if (win.WindowState == WindowState.Maximized)
        //        {
        //            win.WindowState = WindowState.Normal;
        //        }
        //    }
        //}
    }
}

