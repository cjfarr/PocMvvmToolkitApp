using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;

namespace PocMvvmToolkitApp.Services
{
    public interface INavigationService
    {
        bool CanGoBack
        {
            get;
        }

        void AddFrame(Frame frame, string frameId);

        void RemoveFrame(string frameId);

        void RegisterViewModel<TInterface, TViewModel>(ServiceCollection services);

        void RegisterPage<TViewModelInterface, TPage>();

        void GoBack();

        /// <summary>
        /// Use when a page has not yet been converted to MVVM pattern.
        /// Eventually delete this method.
        /// </summary>
        /// <param name="noMvvmPageType"></param>
        /// <param name="parameter"></param>
        void Navigate(Type noMvvmPageType, object parameter = null);

        void Navigate<TViewModel>(string frameId = null);

        void Navigate<TViewModel>(object parameter, string frameId = null);
    }
}
