

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Epicor.App.ViewModel
{
    public partial class StartViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isActive =false;


        [RelayCommand]
        private void Toogle()
        {
            IsActive = !IsActive;
        }
    }
}
