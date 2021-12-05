using DispImage.Views;
using ImageCtrlDisp;
using ImageCtrlDisp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DispImage.ViewModels
{
    public class UC_MainPanelViewModel : BindableBase, IDisposable
    {

        private bool _IsChecked;
        public bool IsChecked
        {
            get => _IsChecked;
            set
            {
                if (_IsChecked == value) return;

                _IsChecked = value;

                if(value)
                {
                    _regionManager.RequestNavigate("ViewContentName", "UC_Panel_ModeA");
                }
                else 
                {
                    _regionManager.RequestNavigate("ViewContentName", "UC_Panel_ModeB");
                }

                RaisePropertyChanged();

            }
        }

        /// <summary>
        /// 閉じるコマンド
        /// </summary>
        public DelegateCommand Command { get; private set; }

        private readonly IRegionManager _regionManager;

        public UC_MainPanelViewModel(IUnityContainer service)
        {
            _regionManager = service.Resolve<IRegionManager>();
            //_regionManager.RequestNavigate("ViewContentName",nameof(ImageCtrlDisp.Views.ImageCtrlDispView));

            //IRegionViewRegistry
            //UC_Panel_ModeA
            _regionManager.RegisterViewWithRegion("ViewContentName", typeof(ImageCtrlDispView));
            //IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            //_ = regionMan.RegisterViewWithRegion(nameof(ImageCtrlDispModule), typeof(Views.ImageCtrlDispView));

            //_regionManager.RegisterViewWithRegion("ViewContentName", typeof(UC_Panel_ModeB));


            //Command = new DelegateCommand(() =>
            //{
            //    _regionManager.RequestNavigate("ViewContentName", "UC_Panel_ModeB");

            //});

            //_regionManager.RequestNavigate("ViewContentName", "UC_Panel_ModeA");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
