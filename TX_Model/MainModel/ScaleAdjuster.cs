using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MainModel
{
    /// <summary>
    /// 
    /// </summary>
    public class ScaleAdjuster : IScaleAdjuster
    {
        /// <summary>
        /// 初期倍率計算完了
        /// </summary>
        public event EventHandler CmpInitZoomRateImage;
        /// <summary>
        /// 倍率設定
        /// </summary>
        public event EventHandler ChangeZoomRate;
        /// <summary>
        /// 画像を中心位置にする
        /// </summary>
        public event EventHandler ResetImage;
        /// <summary>
        /// 最小桁数
        /// </summary>
        public int ScaleDecimalPlace { get; } = (int)Math.Pow(10,2);
        /// <summary>
        /// 最小値
        /// </summary>
        public float MinValue { get; private set; }
        /// <summary>
        /// 倍率
        /// </summary>
        public float ZoomRate { get; private set; }
        /// <summary>
        /// 画像高さ
        /// </summary>
        public float HeightDpi { get; private set; }
        /// <summary>
        /// 画像幅
        /// </summary>
        public float WidthDpi { get; private set; }
        /// <summary>
        /// 画像高さ
        /// </summary>
        public float ActualImageHeight { get; private set; }
        /// <summary>
        /// 画像幅
        /// </summary>
        public float ActualImageWidth { get; private set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ScaleAdjuster()
        {

        }
        /// <summary>
        /// 初期倍率の計算
        /// </summary>
        /// <param name="RealWidth"></param>
        /// <param name="RealHeight"></param>
        public void DoCaleInitScale(float initScrollActualWidth,
                                    float initScrollActualHeight,
                                    float dpi_width,
                                    float dpi_height)
        {
            WidthDpi = dpi_width;
            HeightDpi = dpi_height;

            ActualImageHeight = initScrollActualHeight;
            ActualImageWidth = initScrollActualWidth;
            List<float> vals = new List<float>()
                    {
                        initScrollActualWidth,
                        initScrollActualHeight,
                        WidthDpi,
                        HeightDpi
                    };

            float canvmax = Math.Max(HeightDpi, WidthDpi);
            
            float scrolmax = Math.Max(initScrollActualHeight, initScrollActualWidth);
            
            float scrolmin = Math.Min(initScrollActualHeight, initScrollActualWidth);

            if (canvmax > scrolmax)
            {//画像大きい
                var wrate = initScrollActualWidth / WidthDpi;
                var hrate = initScrollActualHeight / HeightDpi;

                if(wrate<hrate)
                {
                    ZoomRate = (float)(Math.Floor(wrate * ScaleDecimalPlace) / ScaleDecimalPlace);
                }
                else 
                {
                    ZoomRate = (float)(Math.Floor(hrate * ScaleDecimalPlace) / ScaleDecimalPlace);
                }      
            }
            else
            {//画像小さい
                ZoomRate = (float)(Math.Floor(scrolmin / (canvmax) * ScaleDecimalPlace) / ScaleDecimalPlace);
            }

            MinValue = ZoomRate;

            ChangeZoomRate?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ZoomValue"></param>
        public void SetZoomValue(float zoomValue)
        {
            ZoomRate = zoomValue;
            ChangeZoomRate?.Invoke(this, new EventArgs());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IScaleAdjuster
    {
        /// <summary>
        /// 倍率設定
        /// </summary>
        event EventHandler ChangeZoomRate;
        /// <summary>
        /// 初期倍率計算完了
        /// </summary>
        event EventHandler CmpInitZoomRateImage;
        /// <summary>
        /// 画像を中心位置にする
        /// </summary>
        event EventHandler ResetImage;
        /// <summary>
        /// 
        /// </summary>
        void DoCaleInitScale(float scrollwidth, float scrollheight, float pixelwidth, float pixelheight);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ZoomValue"></param>
        void SetZoomValue(float zoomValue);
    }
}
