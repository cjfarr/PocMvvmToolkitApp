namespace PocMvvmToolkitApp.ViewModels
{
    using Services;

    public class TsaMainPageViewModel : MainPageViewModel
    {
        public TsaMainPageViewModel(
            ITestService testService,
            INavigationService navigationService,
            IDialogService dialogService) 
            : base(testService, navigationService, dialogService)
        {
        }

        public override string Message
        {
            get => $"TSA Main Page. TestService.GetHashCode() = {this.testService.GetHashCode()}";
        }

        public override void OnNavigatedTo(object parameter)
        {
            System.Diagnostics.Debug.WriteLine("TSA MainPageViewModel.OnNavigatedTo called");
        }

        public override void OnNavigatedFrom(object parameter)
        {
            System.Diagnostics.Debug.WriteLine("TSA MainPageViewModel.OnNavigatedFrom called");
        }
    }
}
