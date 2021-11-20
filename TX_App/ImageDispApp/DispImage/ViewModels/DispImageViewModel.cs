using MainModel;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
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
        /// 表示画像
        /// </summary>
        private BitmapImage _ImageSource;
        public BitmapImage ImageSource
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource, value); }
        }
        /// <summary>
        /// タイトル
        /// </summary>
        private string _Title;
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        /// <summary>
        /// Zoomレート
        /// </summary>
        private float _ZoomScale;
        public float ZoomScale
        {
            get { return _ZoomScale; }
            set { SetProperty(ref _ZoomScale, value); }
        }
        /// <summary>
        /// 最大Zoomレート
        /// </summary>
        private float _MaxSlider;
        public float MaxSlider
        {
            get { return _MaxSlider; }
            set { SetProperty(ref _MaxSlider, value); }
        }
        /// <summary>
        /// 最大Zoomレート
        /// </summary>
        private float _MinSlider;
        public float MinSlider
        {
            get { return _MinSlider; }
            set { SetProperty(ref _MinSlider, value); }
        }
        private readonly IRegionManager _RegionManager;

        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service"></param>
        public DispImageViewModel(IUnityContainer service)
        {
            _RegionManager = service.Resolve<IRegionManager>();

            MaxSlider = 4;
            ZoomScale = 2;

            _ImageSource = new BitmapImage();

            _LoadImage = service.Resolve<ILoadImager>();
            _LoadImage.EndLoadImage += (s, e) =>
            {
                Debug.WriteLine($"{nameof(DispImage)} ViewModel imageloaded");
                ImageSource = (s as LoadImager).DispImage;
                Title = Path.GetFileName((s as LoadImager).ImgPath);
                Debug.WriteLine($"{Title}");
            };

            _LoadImage.ClearImage += (s, e) => 
            {
                Debug.WriteLine($"{nameof(DispImage)} ViewModel Cleared");
                RemoveModule<DispImage.Views.DispImage>("DispImageModule");
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
