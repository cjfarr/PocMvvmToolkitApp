namespace PocMvvmToolkitApp.Dialogs.ViewModels
{
    using Args;

    public class InputArgsDialogViewModel : DialogViewModel, IInputArgsDialogViewModel
    {
        public void SetInputArgs<TInputArgs>(TInputArgs args)
        {
            DialogInputArgs d = args as DialogInputArgs;

            this.Title = d.Title;
            this.Message = d.InputMessage;
        }

        private string message;
        public string Message
        {
            get => this.message;
            set => this.SetProperty(ref this.message, value);
        }
    }
}
