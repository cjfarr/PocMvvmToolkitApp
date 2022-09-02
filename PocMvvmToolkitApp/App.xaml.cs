using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PocMvvmToolkitApp.Dialogs;
using PocMvvmToolkitApp.Dialogs.ViewModels;
using PocMvvmToolkitApp.Pages;
using PocMvvmToolkitApp.Services;
using PocMvvmToolkitApp.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PocMvvmToolkitApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private ServiceCollection services;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            this.services = new ServiceCollection();
            this.services.AddSingleton<ITestService>(new TestService());

            ITestService testService = this.services[0].ImplementationInstance as ITestService;
            System.Diagnostics.Debug.WriteLine(testService);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            NavigationService navigationService = new NavigationService(rootFrame);

            navigationService.RegisterViewModel<IMainPageViewModel, MainPageViewModel>(services);
            navigationService.RegisterPage<IMainPageViewModel, MainPage>();

            navigationService.RegisterViewModel<ISecondPageViewModel, SecondPageViewModel>(services);
            navigationService.RegisterPage<ISecondPageViewModel, SecondPage>();

            navigationService.RegisterViewModel<INestedPageViewModel, NestedPageViewModel>(services);
            navigationService.RegisterPage<INestedPageViewModel, NestedPage>();

            services.AddSingleton<INavigationService>(navigationService);
            services.AddSingleton<ITestService>(new TestService());

            ////Dialog view models
            DialogService dialogService = new DialogService();
            dialogService.RegisterDialog<INoArgsDialogViewModel, NoArgsDialogViewModel, NoArgsDialog>(services);
            dialogService.RegisterDialog<IInputArgsDialogViewModel, InputArgsDialogViewModel, InputArgsDialog>(services);

            services.AddSingleton<IDialogService>(dialogService);

            Ioc.Default.ConfigureServices(services.BuildServiceProvider());

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    navigationService.Navigate<IMainPageViewModel>(e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
