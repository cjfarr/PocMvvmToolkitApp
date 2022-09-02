namespace PocMvvmToolkitApp.Dialogs
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.Input;
    using PocMvvmToolkitApp.Args;
    using PocMvvmToolkitApp.Dialogs.ViewModels;
    using PocMvvmToolkitApp.Services;
    using System;
    using System.Windows.Input;
    using Windows.Foundation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public enum ExtendedContentDialogButton
    {
        //
        // Summary:
        //     Not explicitly set, default button will be only button or primary button
        NotSet,
        //
        // Summary:
        //     No button is specified as the default.
        None,
        //
        // Summary:
        //     The primary button is the default.
        Primary,
        //
        // Summary:
        //     The secondary button is the default.
        Secondary,
        //
        // Summary:
        //     The close button is the default.
        Close
    }

    public class ExtendedContentDialog : ContentControl
    {
        public event Action<ExtendedContentDialog> Closing;

        #region [-- Title DP --]
        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(ExtendedContentDialog.Title),
                typeof(String),
                typeof(ExtendedContentDialog),
                new PropertyMetadata(null));
        #endregion [-- Title DP --]

        #region [-- Primary Button --]
        public event TypedEventHandler<ExtendedContentDialog, DialogButtonClickArgs> PrimaryButtonClick;

        #region [-- PrimaryButtonText DP --]
        public string PrimaryButtonText
        {
            get { return (string)GetValue(PrimaryButtonTextProperty); }
            set { SetValue(PrimaryButtonTextProperty, value); }
        }

        public static readonly DependencyProperty PrimaryButtonTextProperty =
            DependencyProperty.Register(
                nameof(ExtendedContentDialog.PrimaryButtonText), 
                typeof(string), 
                typeof(ExtendedContentDialog), 
                new PropertyMetadata(null));
        #endregion [-- PrimaryButtonText DP --]

        #region [-- PrimaryButtonCommand DP --]
        public ICommand PrimaryButtonCommand
        {
            get { return (ICommand)GetValue(PrimaryButtonCommandProperty); }
            set { SetValue(PrimaryButtonCommandProperty, value); }
        }

        private static readonly DependencyProperty PrimaryButtonCommandProperty =
            DependencyProperty.Register(
                nameof(ExtendedContentDialog.PrimaryButtonCommand), 
                typeof(ICommand), 
                typeof(ExtendedContentDialog), 
                new PropertyMetadata(null));
        #endregion [-- PrimaryButtonCommand DP --]

        #region [-- PrimaryButtonStyle DP --]
        public Style PrimaryButtonStyle
        {
            get { return (Style)GetValue(PrimaryButtonStyleProperty); }
            set { SetValue(PrimaryButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty PrimaryButtonStyleProperty =
            DependencyProperty.Register("PrimaryButtonStyle", typeof(Style), typeof(ExtendedContentDialog), new PropertyMetadata(null));
        #endregion [-- PrimaryButtonStyle DP --]

        #region [-- PrimaryButtonVisibility DP --]
        public Visibility PrimaryButtonVisibility
        {
            get { return (Visibility)GetValue(PrimaryButtonVisibilityProperty); }
            set { SetValue(PrimaryButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty PrimaryButtonVisibilityProperty =
            DependencyProperty.Register("PrimaryButtonVisibility", typeof(Visibility), typeof(ExtendedContentDialog), new PropertyMetadata(Visibility.Collapsed));
        #endregion [-- PrimaryButtonVisibility DP --]
        #endregion [-- Primary Button --]

        #region [-- Secondary Button --]
        public event TypedEventHandler<ExtendedContentDialog, DialogButtonClickArgs> SecondaryButtonClick;

        #region [-- SecondaryButtonText DP --]
        public string SecondaryButtonText
        {
            get { return (string)GetValue(SecondaryButtonTextProperty); }
            set { SetValue(SecondaryButtonTextProperty, value); }
        }

        public static readonly DependencyProperty SecondaryButtonTextProperty =
            DependencyProperty.Register("SecondaryButtonText", typeof(string), typeof(ExtendedContentDialog), new PropertyMetadata(null));
        #endregion [-- SecondaryButtonText DP --]

        #region [-- SecondaryButtonCommand DP --]
        public ICommand SecondaryButtonCommand
        {
            get { return (ICommand)GetValue(SecondaryButtonCommandProperty); }
            set { SetValue(SecondaryButtonCommandProperty, value); }
        }

        private static readonly DependencyProperty SecondaryButtonCommandProperty =
            DependencyProperty.Register("SecondaryButtonCommand", typeof(ICommand), typeof(ExtendedContentDialog), new PropertyMetadata(null));
        #endregion [-- SecondaryButtonText DP --]

        #region [-- SecondaryButtonVisibility DP --]
        public Visibility SecondaryButtonVisibility
        {
            get { return (Visibility)GetValue(SecondaryButtonVisibilityProperty); }
            set { SetValue(SecondaryButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty SecondaryButtonVisibilityProperty =
            DependencyProperty.Register("SecondaryButtonVisibility", typeof(Visibility), typeof(ExtendedContentDialog), new PropertyMetadata(Visibility.Collapsed));
        #endregion [-- SecondaryButtonVisibility DP --]

        #region [-- SecondaryButtonStyle DP --]
        public Style SecondaryButtonStyle
        {
            get { return (Style)GetValue(SecondaryButtonStyleProperty); }
            set { SetValue(SecondaryButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty SecondaryButtonStyleProperty =
            DependencyProperty.Register("SecondaryButtonStyle", typeof(Style), typeof(ExtendedContentDialog), new PropertyMetadata(null));
        #endregion [-- SecondaryButtonStyle DP --]
        #endregion

        #region [-- Close Button --]
        public event TypedEventHandler<ExtendedContentDialog, DialogButtonClickArgs> CloseButtonClick;

        #region [-- CloseButtonText DP --]
        public String CloseButtonText
        {
            get { return (String)GetValue(CloseButtonTextProperty); }
            set { SetValue(CloseButtonTextProperty, value); }
        }

        public static readonly DependencyProperty CloseButtonTextProperty =
            DependencyProperty.Register("CloseButtonText", typeof(String), typeof(ExtendedContentDialog), new PropertyMetadata(null));
        #endregion [-- CloseButtonText DP --]

        #region [-- CloseButtonCommand DP --]
        public ICommand CloseButtonCommand
        {
            get { return (ICommand)GetValue(CloseButtonCommandProperty); }
            set { SetValue(CloseButtonCommandProperty, value); }
        }

        private static readonly DependencyProperty CloseButtonCommandProperty =
            DependencyProperty.Register("CloseButtonCommand", typeof(ICommand), typeof(ExtendedContentDialog), new PropertyMetadata(null));
        #endregion [-- CloseButtonText DP --]

        #region [-- ClosedButtonStyle DP --]
        public Style ClosedButtonStyle
        {
            get { return (Style)GetValue(ClosedButtonStyleProperty); }
            set { SetValue(ClosedButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ClosedButtonStyleProperty =
            DependencyProperty.Register("ClosedButtonStyle", typeof(Style), typeof(ExtendedContentDialog), new PropertyMetadata(null));
        #endregion [-- ClosedButtonStyle DP --]

        #region [-- ClosedButtonVisibility DP --]
        public Visibility ClosedButtonVisibility
        {
            get { return (Visibility)GetValue(ClosedButtonVisibilityProperty); }
            set { SetValue(ClosedButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ClosedButtonVisibilityProperty =
            DependencyProperty.Register("ClosedButtonVisibility", typeof(Visibility), typeof(ExtendedContentDialog), new PropertyMetadata(Visibility.Collapsed));
        #endregion [-- ClosedButtonVisibility DP --]
        #endregion

        #region [-- DefaultButton DP --]
        public ExtendedContentDialogButton DefaultButton
        {
            get { return (ExtendedContentDialogButton)GetValue(DefaultButtonProperty); }
            set { SetValue(DefaultButtonProperty, value); }
        }

        public static readonly DependencyProperty DefaultButtonProperty =
            DependencyProperty.Register("DefaultButton", typeof(ExtendedContentDialogButton), typeof(ExtendedContentDialog), new PropertyMetadata(ExtendedContentDialogButton.NotSet));
        #endregion [-- DefaultButton DP --]

        #region [-- ButtonAlignment DP --]
        public HorizontalAlignment ButtonAlignment
        {
            get { return (HorizontalAlignment)GetValue(ButtonAlignmentProperty); }
            set { SetValue(ButtonAlignmentProperty, value); }
        }

        private static readonly DependencyProperty ButtonAlignmentProperty =
            DependencyProperty.Register("ButtonAlignment", typeof(HorizontalAlignment), typeof(ExtendedContentDialog), new PropertyMetadata(HorizontalAlignment.Center));
        #endregion [-- ButtonAlignment DP --]

        public bool IsFullScreen { get; set; }

        public bool AlwaysVisible { get; set; }

        private IDialogService dialogService => Ioc.Default.GetService<IDialogService>();
        private IDialogViewModel dialogViewModel => DataContext as IDialogViewModel;

        public ExtendedContentDialog()
        {
            this.PrimaryButtonCommand = new RelayCommand(this.OnPrimaryButtonCommand);
            this.PrimaryButtonVisibility = Visibility.Visible;
        }

        private async void OnPrimaryButtonCommand()
        {
            if (this.dialogViewModel != null)
            {
                bool canClose = this.dialogViewModel.OnPrimaryButtonInvoked();
                if (canClose)
                {
                    this.dialogViewModel.OnClosing();
                    this.Closing?.Invoke(this);
                    await this.dialogService.HideDialog(this);
                }
            }
        }
    }
}
