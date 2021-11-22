using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    /// <summary>
    /// 中心位置をPointに変換する
    /// </summary>
    public class Point2Center: IPoint2Center
    {
        /// <summary>
        /// 計算完了
        /// </summary>
        public event EventHandler EndChangedCenter;
        /// <summary>
        /// 中心位置
        /// </summary>
        public Point CenterPoint { get; private set; }
        /// <summary>
        /// 横サイズ
        /// </summary>
        public double DispWidth { get; private set; }
        /// <summary>
        /// 縦サイズ
        /// </summary>
        public double DispHeight { get; private set; }
        /// <summary>
        /// 画像読込I/F
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limage"></param>
        public Point2Center(ILoadImager limage)
        {
            //_LoadImage = limage;
            //_LoadImage.CmpLoadImage += (s, e) =>
            //{
            //    //var li = s as LoadImager;
            //    //DispHeight = li.DispImage.PixelHeight;
            //    //double y = DispHeight / 2;
            //    //DispWidth = li.DispImage.PixelWidth;
            //    //double x = DispWidth / 2;
            //    //CenterPoint = new Point(x, y);
            //};
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ChangedCenterPoint(double x, double y)
        {
            CenterPoint = new Point(x, y);
            EndChangedCenter?.Invoke(this, new EventArgs());
        }
    }
    /// <summary>
    /// 中心位置をPointに変換する I/F
    /// </summary>
    public interface IPoint2Center
    {
        /// <summary>
        /// 中心変更
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void ChangedCenterPoint(double x, double y);
        /// <summary>
        /// 計算完了
        /// </summary>
        event EventHandler EndChangedCenter;
    }
}
