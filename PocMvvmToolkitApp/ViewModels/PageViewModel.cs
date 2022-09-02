namespace PocMvvmToolkitApp.ViewModels
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using PocMvvmToolkitApp.Services;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;

    public abstract class PageViewModel : ObservableObject, IPageViewModel
    {
        protected readonly INavigationService navigationService;
        protected CoreDispatcher dispatcher;

        public PageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public virtual void OnNavigatedTo(object parameter)
        {
            CoreApplicationView view = CoreApplication.GetCurrentView();
            this.dispatcher = view.Dispatcher;
        }

        public virtual void OnNavigatedFrom(object parameter)
        {
        }
    }
}
