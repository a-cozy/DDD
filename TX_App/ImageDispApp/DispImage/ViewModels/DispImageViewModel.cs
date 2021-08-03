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
using System.Windows.Controls.Primitives;
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
        /// 線の現在位置X
        /// </summary>
        private double _CurrentX;
        public double CurrentX
        {
            get { return _CurrentX; }
            set { SetProperty(ref _CurrentX, value); }
        }
        /// <summary>
        /// 線の現在位置Y
        /// </summary>
        private int _CurrentPosiY;
        public int CurrentPosiY
        {
            get { return _CurrentPosiY; }
            set { SetProperty(ref _CurrentPosiY, value); }
        }
        /// <summary>
        /// マウス位置_X
        /// </summary>
        private double _MouseX;
        public double MouseX
        {
            get { return _MouseX; }
            set { SetProperty(ref _MouseX, value); }
        }
        /// <summary>
        /// マウス位置_Y
        /// </summary>
        private double _MouseY;
        public double MouseY
        {
            get { return _MouseY; }
            set { SetProperty(ref _MouseY, value); }
        }
        /// <summary>
        /// 線の太さ
        /// </summary>
        private float _LineThickness;
        public float LineThickness
        {
            get { return _LineThickness; }
            set { SetProperty(ref _LineThickness, value); }
        }

        /// <summary>
        /// スクロール変更
        /// </summary>
        public ICommand ScrollChanged { get; private set; }
        /// <summary>
        /// ラインのドラッグ
        /// </summary>
        public ICommand ThumbDragDeltaCommand { get; private set; }
        /// <summary>
        /// 画像ローダー
        /// </summary>
        private readonly IImageCoodinate _ImageCoodinate;
        /// <summary>
        /// 画像ローダー
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 画像倍率調整I/F
        /// </summary>
        private readonly IScaleAdjuster _Adjuter;
        /// <summary>
        /// MV → Vへの操作用
        /// </summary>
        //private readonly IEventAggregator _EventAggregator;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service"></param>
        public DispImageViewModel(IUnityContainer service)
        {
            //_EventAggregator = service.Resolve<IEventAggregator>();

            _ImageSource = new BitmapImage();
            LineThickness = 1;

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


                    if (ZoomRate != 0)
                    {
                        CurrentX = ImageSource.Width / 2;
                        var tmp = 1F / (ZoomRate * 0.5) * 100;
                        LineThickness = (float)(Math.Ceiling(tmp) / 100F);
                    }
                }
            };

            _ImageCoodinate = service.Resolve<IImageCoodinate>();
            _ImageCoodinate.PropertyChanged += (s, e) =>
            {
                if (s is ImageCoodinate ic)
                {
                    //CurrentX = (int)ic.CurrentX * (int)ZoomRate;
                    //CurrentX = (int)ic.CurrentX;
                    //_EventAggregator.GetEvent<PubSubEvent<float>>().Publish(CurrentX);
                }
            };

            ThumbDragDeltaCommand = new ActionCommand(x =>
            {
               // _EventAggregator.GetEvent<PubSubEvent<object>>().Publish(x);

                //Debug.WriteLine(CurrentX);
            });

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
