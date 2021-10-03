using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainModel
{
    public class Point2DegRadWin : IPoint2DegRadWin
    {
        /// <summary>
        /// 計算完了
        /// </summary>
        public event EventHandler EndCalDeg;
        /// <summary>
        /// 中心位置
        /// </summary>
        public Point Center { get; private set; }
        /// <summary>
        /// 横サイズ
        /// </summary>
        public double DispWidth { get; private set; }
        /// <summary>
        /// 縦サイズ
        /// </summary>
        public double DispHeight { get; private set; }
        /// <summary>
        /// 回転線の表示角度
        /// </summary>
        public double RotAngle { get; private set; }
        /// <summary>
        /// 回転線の表示角度
        /// </summary>
        public double RotSlope { get; private set; }
        /// <summary>
        /// 中心位置I/F
        /// </summary>
        private readonly IPoint2Center _Point2Cent;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="scale"></param>
        public Point2DegRadWin(IPoint2Center limage)
        {
            _Point2Cent = limage;
            _Point2Cent.EndChangedCenter += (s, e) => 
            {
                var pc = s as Point2Center;
                DispHeight = pc.DispHeight;
                DispWidth = pc.DispWidth;
                Center = pc.CenterPoint;
            };
        }
        /// <summary>
        /// マウスドラック実施
        /// </summary>
        /// <param name="point"></param>
        public void DoMouseDrage(Point point)
        {


            EndCalDeg?.Invoke(this, new EventArgs());
        }
    }

    public interface IPoint2DegRadWin
    {
        event EventHandler EndCalDeg;

        void DoMouseDrage(Point point);
    }

}
