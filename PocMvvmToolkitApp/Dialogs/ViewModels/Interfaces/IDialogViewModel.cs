namespace PocMvvmToolkitApp.Dialogs.ViewModels
{
    public interface IDialogViewModel
    {
        string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Occurs when Primary Button is invoked
        /// </summary>
        /// <returns>CanClose true/false</returns>
        bool OnPrimaryButtonInvoked();

        /// <summary>
        /// Occurs when Secondary Button is invoked
        /// </summary>
        /// <returns>CanClose true/false</returns>
        bool OnSecondaryButtonInvoked();

        /// <summary>
        /// Occurs when Closing Button is invoked
        /// </summary>
        /// <returns>CanClose true/false</returns>
        bool OnClosingButtonInvoked();

        /// <summary>
        /// Occurs when dialog is closing
        /// </summary>
        void OnClosing();
    }

    public interface IDialogInputViewModel : IDialogViewModel
    {
        void SetInputArgs<TInputArgs>(TInputArgs args);
    }

    public interface IDialogReturnViewModel : IDialogViewModel
    {
        TReturnArgs GetReturnArgs<TReturnArgs>();
    }

    public interface IDialogInputAndReturnViewModel : IDialogViewModel
    {
        void SetInputArgs<TInputArgs>(TInputArgs args);

        TReturnArgs GetReturnArgs<TReturnArgs>();
    }
}
