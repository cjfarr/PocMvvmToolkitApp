namespace PocMvvmToolkitApp.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using PocMvvmToolkitApp.ViewModels;
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Frame> frames;
        private readonly Dictionary<Type, Type> pageToViewModelMap;
        private readonly Dictionary<Type, Type> viewModelToPageMap;
        private readonly Dictionary<Type, Type> interfaceToViewModelMap;

        private const string rootKey = "rootFrame";

        public NavigationService(Frame rootFrame)
        {
            this.frames = new Dictionary<string, Frame>();

            rootFrame.Tag = rootKey;
            this.frames[rootKey] = rootFrame;
            this.pageToViewModelMap = new Dictionary<Type, Type>();
            this.viewModelToPageMap = new Dictionary<Type, Type>();
            this.interfaceToViewModelMap = new Dictionary<Type, Type>();
        }

        public void AddFrame(Frame frame, string frameId)
        {
            frame.Tag = frameId;
            this.frames[frameId] = frame;
        }

        public void RemoveFrame(string frameId)
        {
            if (this.frames.ContainsKey(frameId))
            {
                this.frames[rootKey].Navigated -= this.OnRootFrameNavigatedTo;
                this.frames.Remove(frameId);
            }
        }

        /// <summary>
        /// For root frame only
        /// </summary>
        public bool CanGoBack
        {
            get => this.frames[rootKey].CanGoBack;
        }

        /// <summary>
        /// For root frame only
        /// </summary>
        public void GoBack()
        {
            this.frames[rootKey].Navigated += this.OnRootFrameNavigatedTo;
            this.frames[rootKey].GoBack();
        }

        public void RegisterViewModel<TInterface, TViewModel>(ServiceCollection services)
        {
            this.interfaceToViewModelMap[typeof(TInterface)] = typeof(TViewModel);
            services.AddTransient(typeof(TInterface), typeof(TViewModel));
        }

        public void RegisterPage<TViewModelInterface, TPage>()
        {
            this.pageToViewModelMap[typeof(TPage)] = typeof(TViewModelInterface);
            this.viewModelToPageMap[typeof(TViewModelInterface)] = typeof(TPage);
        }

        public void Navigate(Type noMvvmPageType, object parameter = null)
        {
            this.frames[rootKey].Navigated += this.OnRootFrameNavigatedTo;
            this.frames[rootKey].Navigate(noMvvmPageType, parameter);
        }

        public void Navigate<TViewModel>(string frameId = null)
        {
            this.Navigate<TViewModel>(null, frameId);
        }

        /// <summary>
        /// I had wanted to give a type parameter of TArgs,
        /// but I don't think I can enforce that through
        /// Frame.Navigate
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="frameId"></param>
        public void Navigate<TViewModel>(object parameter, string frameId = null)
        {
            Type pageType = this.MapPageType<TViewModel>();

            if (string.IsNullOrEmpty(frameId))
            {
                frameId = rootKey;
            }

            this.frames[frameId].Navigated += this.OnRootFrameNavigatedTo;
            this.frames[frameId].Navigate(pageType, parameter);
        }

        private void OnRootFrameNavigatedTo(object sender, NavigationEventArgs e)
        {
            string frameId = rootKey;
            Frame frame = sender as Frame;
            if (frame?.Tag != null)
            {
                frameId = frame.Tag.ToString();
            }

            this.frames[frameId].Navigated -= this.OnRootFrameNavigatedTo;
            Page page = e.Content as Page;
            if (this.pageToViewModelMap.ContainsKey(page.GetType()))
            {
                PageViewModel viewModel = Ioc.Default.GetService(this.pageToViewModelMap[page.GetType()]) as PageViewModel;
                page.DataContext = viewModel;
            }
        }

        private Type MapPageType<TViewModel>()
        {
            Type key = typeof(TViewModel);

            if (!this.viewModelToPageMap.ContainsKey(key))
            {
                throw new Exception($"{key.Name} was not mapped for navigation");
            }

            return this.viewModelToPageMap[key];
        }
    }
}
