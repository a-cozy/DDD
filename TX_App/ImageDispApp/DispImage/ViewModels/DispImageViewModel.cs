using MainModel;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Unity;

namespace DispImage.ViewModels
{
    public class DispImageViewModel : BindableBase,IDisposable
    {
        /// <summary>
        /// UC_DispImageViewMode
        /// </summary>
        private ObservableCollection<UC_DispImageViewModel> _UC_DispImage;
        public ObservableCollection<UC_DispImageViewModel> UC_DispImage
        {
            get { return _UC_DispImage; }
            set { SetProperty(ref _UC_DispImage, value); }
        }
        /// <summary>
        /// 選択タブのIdx
        /// </summary>
        private int _SelTabIdx;
        public int SelTabIdx
        {
            get { return _SelTabIdx; }
            set { SetProperty(ref _SelTabIdx, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        private readonly IRegionManager _RegionManager;
        /// <summary>
        /// 
        /// </summary>
        private readonly IImageCollector _ImageCollection;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service"></param>
        public DispImageViewModel(IUnityContainer service)
        {
            _RegionManager = service.Resolve<IRegionManager>();

            UC_DispImage = new ObservableCollection<UC_DispImageViewModel>();

            _ImageCollection = service.Resolve<IImageCollector>();
            _ImageCollection.ChangedImageCollection += (s, e) =>
            {
                UC_DispImageViewModel tmp = new UC_DispImageViewModel(service);
                tmp.Closed += (_s,_e) =>
                {
                    UC_DispImage.Remove(_s as UC_DispImageViewModel);
                    SelTabIdx = UC_DispImage.Count() - 1;
                };
                UC_DispImage.Add(tmp);
                SelTabIdx = UC_DispImage.Count() - 1;
            };
        }
        /// <summary>
        /// ViewModel削除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regionName"></param>
        private void RemoveModule<T>(string regionName) where T:UserControl
        {
            var viewToRemove = _RegionManager.Regions[regionName].
                Views.FirstOrDefault(p => p.GetType().Name == typeof(T).Name);

            if(viewToRemove != null)
            {
                _RegionManager.Regions[regionName].Remove(viewToRemove);
            }
        }
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            Debug.WriteLine($"{nameof(DispImage)} ViewModel is disposing");
        }
    }
}
