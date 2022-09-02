using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMvvmToolkitApp.ViewModels
{
    public interface IPageViewModel
    {
        void OnNavigatedTo(object parameter);

        void OnNavigatedFrom(object parameter);
    }
}
