

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    }
}

