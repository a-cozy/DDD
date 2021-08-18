using MainModel;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Unity;

namespace DispImageWindow.ViewModels
{
    public class DispImageWindowViewModel : BindableBase
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
        private double _CurrentLeft;
        public double CurrentLeft
        {
            get { return _CurrentLeft; }
            set { SetProperty(ref _CurrentLeft, value); }
        }
        /// <summary>
        /// 線の現在位置Y
        /// </summary>
        private double _CurrentTop;
        public double CurrentTop
        {
            get { return _CurrentTop; }
            set { SetProperty(ref _CurrentTop, value); }
        }
        /// <summary>
        /// 回転線の現在位置X
        /// </summary>
        private double _CurrentRight;
        public double CurrentRight
        {
            get { return _CurrentRight; }
            set { SetProperty(ref _CurrentRight, value); }
        }
        /// <summary>
        /// 回転線の現在位置Y
        /// </summary>
        private double _CurrentBottom;
        public double CurrentBottom
        {
            get { return _CurrentBottom; }
            set { SetProperty(ref _CurrentBottom, value); }
        }
        /// <summary>
        /// 回転線の角度
        /// </summary>
        private double _RotAngle;
        public double RotAngle
        {
            get { return _RotAngle; }
            set { SetProperty(ref _RotAngle, value); }
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
        /// 画像ローダー
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 画像倍率調整I/F
        /// </summary>
        private readonly IScaleAdjuster _Adjuter;

        /// <summary>
        /// スクロール変更
        /// </summary>
        public ICommand ScrollChanged { get; private set; }

        public DispImageWindowViewModel(IUnityContainer service)
        {

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

                        CurrentLeft = 0;
                        CurrentTop = 0;
                        double centX = ImageSource.Width / 2d;
                        double centY = ImageSource.Height / 2d;
                        //var dddd = new Point(this.designerItem.Width * this.designerItem.RenderTransformOrigin.X,
                        //          this.designerItem.Height * this.designerItem.RenderTransformOrigin.Y)
                        var ddd = Math.Atan2(centX, centY);

                        var radians = Math.Atan2(CurrentLeft, CurrentTop);
                        var angle = radians * (180 / Math.PI);

                        CurrentLeft = 0;
                        CurrentTop = ImageSource.Height / 2 + ImageSource.Height / 4;
                        CurrentRight = 0;
                        CurrentBottom = ImageSource.Height / 2 + ImageSource.Height / 4;
                        RotAngle = 30;
                        //var radians = Math.Atan2(transPoint.Y, transPoint.X);
                        //var angle = radians * (180 / Math.PI);

                        var tmp = 1F / (ZoomRate * 0.5) * 100;
                        LineThickness = (float)(Math.Ceiling(tmp) / 100F);
                    }
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

            _LoadImage.RequestImage();
        }
    }
}
