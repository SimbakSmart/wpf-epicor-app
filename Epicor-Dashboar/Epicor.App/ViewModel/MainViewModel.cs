

//using PropertyChanged;
//using System.Threading.Tasks;
//using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;



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

        //public ICommand ToggleCommand { get; private set; }

        //public MainViewModel()
        //{
        //    IsOpen = false;

        //    //ToggleCommand = new Command
        //    //(async () => await ToggleMenuAsync());
        //   // ToggleCommand = new RelayCommand(ToggleMenuAsyn);
        //}
        ////private Task<bool> ToggleMenuAsync()
        //{
        //   return IsOpen = !IsOpen;
        //}

    }
}
