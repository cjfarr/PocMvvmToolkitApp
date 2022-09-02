using PocMvvmToolkitApp.Services;

namespace PocMvvmToolkitApp.ViewModels
{
    public class NestedPageViewModel : PageViewModel, INestedPageViewModel
    {
        public NestedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public string Title 
        {
            get => "Nested Page";
        }
    }
}
