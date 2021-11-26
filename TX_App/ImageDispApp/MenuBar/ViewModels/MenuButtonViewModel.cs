using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MenuBar.ViewModels
{
    public class MenuButtonViewModel: BindableBase
    {
        /// <summary>
        /// モード名
        /// </summary>
        private string _ModeName;
        public string ModeName
        {
            get { return _ModeName; }
            set { SetProperty(ref _ModeName, value); }
        }
        /// <summary>
        /// IsChecked
        /// </summary>
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { SetProperty(ref _IsChecked, value); }
        }
        /// <summary>
        /// 画像を開くコマンド
        /// </summary>
        public DelegateCommand ChangedCommand { get; private set; }

        public MenuButtonViewModel(IUnityContainer service)
        {
            ModeName = "Aモード";

            ChangedCommand = new DelegateCommand(() =>
            {
                if (_IsChecked)
                {
                    ModeName = "Aモード";
                }
                else 
                {
                    ModeName = "Bモード";
                }

            });

        }
    }

}
