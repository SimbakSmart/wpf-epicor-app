
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Epicor.App.ViewModel
{
    public partial class UsersViewModel : ObservableObject
    {

        [ObservableProperty]
       // [NotifyCanExecuteChangedFor(nameof(ActionEvent))]
        private string? firstName;


        public UsersViewModel()
        {
            FirstName = "Mi Nombre";
        }


        [RelayCommand]
        private void ActionEvent()
        {
            FirstName = "Otro Nombere";
        }
    }
}
