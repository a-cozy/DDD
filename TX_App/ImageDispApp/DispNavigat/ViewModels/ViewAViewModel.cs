using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DispNavigat.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand<string> ShowViewCommand { get; }


        /// <summary>
        /// TypeA ViewModel
        /// </summary>
        private ViewTypeAViewMode _TypeA;
        public ViewTypeAViewMode TypeA
        {
            get => _TypeA;
            set
            {
                if (_TypeA == value) return;
                _TypeA = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// TypeA ViewModel
        /// </summary>
        private ViewTypeBViewMode _TypeB;
        public ViewTypeBViewMode TypeB
        {
            get => _TypeB;
            set
            {
                if (_TypeB == value) return;
                _TypeB = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// TypeA ViewModel
        /// </summary>
        private BindableBase _CurrentType;
        public BindableBase CurrentType
        {
            get => _CurrentType;
            set
            {
                if (_CurrentType == value) return;
                _CurrentType = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private readonly IRegionManager _RegionManager;

        public ViewAViewModel(IUnityContainer service)
        {
            _RegionManager = service.Resolve<IRegionManager>();
            this.ShowViewCommand = new DelegateCommand<string>((d) =>
            {
                if(CurrentType== TypeA)
                {
                    CurrentType = TypeB;
                }
                else if(CurrentType == TypeB)
                {
                    CurrentType = TypeA;
                }
                
            });

            TypeA = new ViewTypeAViewMode();
            TypeB = new ViewTypeBViewMode();
            CurrentType = TypeA;


            Message = "View A from your Prism Module";
        }
    }
}
