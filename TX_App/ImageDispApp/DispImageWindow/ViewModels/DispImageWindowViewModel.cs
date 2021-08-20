using MainModel;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class DispImageWindowViewModel : BindableBase, IDestructible
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
        /// 線の現在位置X1
        /// </summary>
        private double _PointX1;
        public double PointX1
        {
            get { return _PointX1; }
            set { SetProperty(ref _PointX1, value); }
        }
        /// <summary>
        /// 線の現在位置X2
        /// </summary>
        private double _PointX2;
        public double PointX2
        {
            get { return _PointX2; }
            set { SetProperty(ref _PointX2, value); }
        }
        /// <summary>
        /// 回転線の現在位置Y1
        /// </summary>
        private double _PointY1;
        public double PointY1
        {
            get { return _PointY1; }
            set { SetProperty(ref _PointY1, value); }
        }
        /// <summary>
        /// 回転線の現在位置Y
        /// </summary>
        private double _PointY2;
        public double PointY2
        {
            get { return _PointY2; }
            set { SetProperty(ref _PointY2, value); }
        }
        /// <summary>
        /// 回転線の角度
        /// </summary>
        //private double _RotAngle;
        //public double RotAngle
        //{
        //    get { return _RotAngle; }
        //    set { SetProperty(ref _RotAngle, value); }
        //}

        ///// <summary>
        /// 回転線の角度
        ///// </summary>
        private double _RotAngle;
        public double RotAngle
        {
            get => _RotAngle;
            set
            {
                if (_RotAngle == value)return;
                
                _RotAngle = value;
                
                RaisePropertyChanged();

            }
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
        /// <summary>
        /// MV → Vへの操作用
        /// </summary>
        private readonly IEventAggregator _EventAggregator;

        public DispImageWindowViewModel(IUnityContainer service)
        {

            _EventAggregator = service.Resolve<IEventAggregator>();

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
            _LoadImage.ClearImage += (s, e) =>
            {
                if (s is LoadImager li)
                {
                    _IsOpenProc = true;
                    ZoomRate = 0.1F;
                    ImageSource = li.NullImage;

                   // Task.WaitAll(Task.Delay(1000));
                }

                _EventAggregator.GetEvent<PubSubEvent<object>>().Publish("");

            };


            _Adjuter = service.Resolve<IScaleAdjuster>();
            _Adjuter.ChangeZoomRate += (s, e) =>
            {
                if (s is ScaleAdjuster sa)
                {
                    ZoomRate = sa.ZoomRate;

                    if (ZoomRate != 0)
                    {

                        //double centX = sa.ImageHeight / 2d;
                        //double centY = sa.ImageWidth / 2d;
                        //double ddd = Math.Atan2(0 - centX, 0 - centY);
                        //double ang = (ddd + Math.PI/4) * (180 / Math.PI);
                        //if (ang < 0)
                        //{
                        //    ang += 360;
                        //}
                        //RotAngle = ang;

                        //if (ddd < 0)
                        //{
                        //    ddd += Math.PI;
                        //}

                        //var a = Math.Tan((RotAngle*Math.PI)/180);
                        //var b = centY - a * centX;


                        //PointY1 = b;
                        //PointX1 = (PointY1 - b) / a;
                        //PointY2 = a * sa.ImageWidth + b;
                        //PointX2 = (sa.ImageHeight - b)/ a;


                        var tmp = 1F / (ZoomRate * 0.5) * 100;
                        LineThickness = (float)(Math.Ceiling(tmp) / 100F);
                    }
                }
            };

            ScrollChanged = new ActionCommand((d) =>
            {
                if (_IsOpenProc && d is ScrollViewer sv)
                {
                    //Debug.WriteLine();
                    var width = sv.ActualWidth;// + sv.DesiredSize.Width;
                    var height = sv.ActualHeight;// + sv.DesiredSize.Height;
                    _Adjuter.DoCaleInitScale((float)width, (float)height);
                    _IsOpenProc = false;
                }
            });

            _LoadImage.RequestImage();
        }

        public void Destroy()
        {

        }
    }
}
