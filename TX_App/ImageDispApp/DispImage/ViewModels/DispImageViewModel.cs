using MainModel;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Unity;

namespace DispImage.ViewModels
{
    public class DispImageViewModel : BindableBase
    {
        /// <summary>
        /// 画像開く処理完了？
        /// </summary>
        private bool _IsOpenProc = false;
        /// <summary>
        /// 画像倍率
        /// </summary>
        private float _ZoomRate;
        public float ZoomRate
        {
            get { return _ZoomRate; }
            set { SetProperty(ref _ZoomRate, value); }
        }
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
        /// スクロール変更
        /// </summary>
        public ICommand ScrollChanged { get; private set; }
        /// <summary>
        /// 画像ローダー
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 画像倍率調整I/F
        /// </summary>
        private readonly IScaleAdjuster _Adjuter;
        /// <summary>
        /// 倍率を変更してもスケールバーの位置が変更されない措置
        /// </summary>
        private readonly IEventAggregator _EventAggregator;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service"></param>
        public DispImageViewModel(IUnityContainer service)
        {
            _EventAggregator = service.Resolve<IEventAggregator>();

            _ImageSource = new BitmapImage();

            _LoadImage = service.Resolve<ILoadImager>();
            _LoadImage.CmpLoadImage += (s, e) =>
            {
                if (s is LoadImager li)
                {
                    _IsOpenProc = true;
                    ZoomRate = 1;
                    ImageSource = li.DispImage;
                }
            };

            _Adjuter = service.Resolve<IScaleAdjuster>();
            _Adjuter.ChangeZoomRate += (s, e) =>
            {
                if (s is ScaleAdjuster sa)
                {
                    ZoomRate = sa.ZoomRate;
                }
            };

            ScrollChanged = new ActionCommand((d) =>
            {
                if (_IsOpenProc && d is ScrollViewer sv)
                {
                    _Adjuter.DoCaleInitScale((float)sv.ActualWidth, (float)sv.ActualHeight);
                    _IsOpenProc = false;
                }
            });
        }
    }
}
