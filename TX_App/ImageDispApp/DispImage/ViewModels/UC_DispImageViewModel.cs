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
        /// タイトル
        /// </summary>
        private bool _IsSetMinScale;
        /// <summary>
        /// タイトル
        /// </summary>
        public bool IsSetMinScale
        {
            get { return _IsSetMinScale; }
            set { SetProperty(ref _IsSetMinScale, value); }
        }
        /// <summary>
        /// Load Event
        /// </summary>
        public ICommand ScrollChanged { get; private set; }
        ///// <summary>
        ///// Load Event
        ///// </summary>
        public ICommand ImageLoaded { get; private set; }
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
        private readonly IImageScaleControlor _Adjuter;
        /// <summary>
        /// 画像表示ユーザーコントロール
        /// </summary>
        public UC_DispImageViewModel(IUnityContainer service)
        {
            
            MaxSlider = 4;
            ZoomScale = 2;

            _ImageSource = new BitmapImage();
            _ImageArrayToBitmap = service.Resolve<IImageArrayToBitmap>();
            _ImageArrayToBitmap.RequestBitmap += (s, e) =>
            {
                if (Title == null)
                {
                    Title = Path.GetFileName((s as ImageArrayToBitmap).ImageDispInf.ImgPath);
                    ImageSource = (s as ImageArrayToBitmap).ImageDispInf.DispImage;
                    Debug.WriteLine($"{nameof(DispImage)}の{Title} ViewModel imageloaded");
                }
            };

            _Adjuter = service.Resolve<IImageScaleControlor>();

            FittingCmd = new ActionCommand((d) => 
            {
                Debug.WriteLine($"{Title} FittingCmd");
            });

            ScrollChanged = new ActionCommand((d) => 
            {
                if (!IsSetMinScale)
                    SetMinScaleMethod(d);
            });
            ImageLoaded = new ActionCommand((d) =>
            {
                if (!IsSetMinScale)
                    SetMinScaleMethod(d);
            });

            ClearCmd = new DelegateCommand(() =>
            {
                Debug.WriteLine($"{Title} is Closeed");
                Dispose();
                GC.SuppressFinalize(this);
                Closed.Invoke(this, new EventArgs());
            });

            _ImageArrayToBitmap.Request();
        }
        /// <summary>
        /// 最小設定値を設定
        /// </summary>
        /// <param name="d"></param>
        private void SetMinScaleMethod(object d)
        {
            //Debug.WriteLine($"{Title}の最小設定値を設定");
            _Adjuter.EndSetActualSize += (s, e) =>
            {
                Debug.WriteLine($"{Title}の最小設定値を設定");
                ZoomScale = (s as ImageScaleControlor).CurrentScale;
                MinSlider = (s as ImageScaleControlor).MinScale;
                MaxSlider = (s as ImageScaleControlor).MaxScale;
            };

            _Adjuter.SetActualSize(
                (float)(d as ScrollViewer).ActualWidth,
                (float)(d as ScrollViewer).ActualHeight,
                    ImageSource
                );

            IsSetMinScale = true;
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
