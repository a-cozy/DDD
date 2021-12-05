using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispImage.ViewModels
{
    public class UC_Panel_ModeAViewModel:BindableBase, INavigationAware
    {
        public UC_Panel_ModeAViewModel()
        {
            Debug.WriteLine($"{nameof(UC_Panel_ModeAViewModel)} is constracted");

        }


        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // このViewが表示された状態から切り替わるときに実行される
            Debug.WriteLine($"{nameof(UC_Panel_ModeAViewModel)} OnNavigatedFrom is called");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // このViewが表示されるときに実行される
            Debug.WriteLine($"{nameof(UC_Panel_ModeAViewModel)} OnNavigatedTo is called");
        }
    }
}
