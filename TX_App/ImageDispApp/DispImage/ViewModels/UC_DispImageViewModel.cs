using MainModel;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Unity;

namespace DispImage.ViewModels
{
    /// <summary>
    /// 画像表示ユーザーコントロール
    /// </summary>
    public class UC_DispImageViewModel : BindableBase, IUC_DispImageViewModel, IDisposable
    {
        /// <summary>
        /// Load Event
        /// </summary>
        public ICommand ScrollChanged { get; private set; }
        ///// <summary>
        ///// Load Event
        ///// </summary>
        public ICommand ImageLoaded { get; private set; }
        /// <summary>
        /// 画像開く処理完了？
        /// </summary>
        private bool _IsCreating = false;
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
        /// <summary>
        /// 閉じるコマンド
        /// </summary>
        public DelegateCommand ClearCmd { get; private set; }
        /// <summary>
        /// 画像のFittingコマンド
        /// </summary>
        public ICommand FittingCmd { get; private set; }
        /// <summary>
        /// 閉じると発生するイベント
        /// </summary>
        public event EventHandler Closed;
        /// <summary>
        /// 画像bitmap変換I/F
        /// </summary>
        private readonly IImageArrayToBitmap _ImageArrayToBitmap;
        /// <summary>
        /// 画像倍率調整I/F
        /// </summary>
        private readonly IScaleAdjuster _Adjuter;
        /// <summary>
        /// 画像表示ユーザーコントロール
        /// </summary>
        public UC_DispImageViewModel(IUnityContainer service)
        {
            //_EventAggregator = service.Resolve<IEventAggregator>();

            MaxSlider = 4;
            ZoomScale = 2;

            _ImageSource = new BitmapImage();
            _ImageArrayToBitmap = service.Resolve<IImageArrayToBitmap>();
            _ImageArrayToBitmap.RequestBitmap += (s, e) =>
            {
                try
                {
                    _IsCreating = true;
                    if (Title == null)
                    {
                        Debug.WriteLine($"{nameof(DispImage)} ViewModel imageloaded");
                        Title = Path.GetFileName((s as ImageArrayToBitmap).ImageDispInf.ImgPath);
                        ImageSource = (s as ImageArrayToBitmap).ImageDispInf.DispImage;
                    }
                }
                finally
                {
                    _IsCreating = false;
                }
            };

            //ScrollChanged = new ActionCommand((d) =>
            //{
            //    if (_IsCreating)
            //    {
            //        _Adjuter.DoCaleInitScale(
            //            (float)(d as ScrollViewer).ActualWidth,
            //            (float)(d as ScrollViewer).ActualHeight,
            //            (float)_ImageSource.Width,
            //            (float)_ImageSource.Height);
            //    }
            //});

            ImageLoaded = new ActionCommand((d) =>
            {

            });

            FittingCmd = new DelegateCommand(() => 
            {
                Debug.WriteLine("FittingCmd");
            });

            _Adjuter = service.Resolve<IScaleAdjuster>();
            _Adjuter.ChangeZoomRate += (s, e) =>
            {
                var sa = s as ScaleAdjuster;
                ZoomScale = sa.ZoomRate;
            };

            ClearCmd = new DelegateCommand(() =>
            {
                Debug.WriteLine("Closeed");
                Dispose();
                GC.SuppressFinalize(this);
                Closed.Invoke(this, new EventArgs());
            });

            _ImageArrayToBitmap.Request();
        }
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            Debug.WriteLine($"{nameof(UC_DispImageViewModel)} have been clearing");
        }
    }

    public interface IUC_DispImageViewModel
    {
        event EventHandler Closed;
    }
}
