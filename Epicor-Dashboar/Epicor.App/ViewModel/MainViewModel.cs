
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;



namespace Epicor.App.ViewModel
{
  
    public class MainViewModel : ObservableObject
    {

        public bool IsChecked { get; set; }


        public MainViewModel()
        {
            IsChecked = false;
        }

        [RelayCommand]
        void ToggleCommand()
        {
            IsChecked = !IsChecked;
        }




    }
}
