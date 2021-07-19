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
        /// 倍率
        /// </summary>
        public float ZoomRate { get; private set; }
        /// <summary>
        /// 画像高さ
        /// </summary>
        public float ImageHeight { get; private set; }
        /// <summary>
        /// 画像幅
        /// </summary>
        public float ImageWidth { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ScaleAdjuster(IUnityContainer service)
        {
            _LoadImage = service.Resolve<ILoadImager>();
            _LoadImage.CmpLoadImage += (s, e) =>
            {
                if (s is LoadImager li)
                {
                    ImageHeight = (float)li.DispImage.Height;
                    ImageWidth = (float)li.DispImage.Width;
                }
            };
        }
        /// <summary>
        /// 初期倍率の計算
        /// </summary>
        /// <param name="RealWidth"></param>
        /// <param name="RealHeight"></param>
        public void DoCaleInitScale(float initScrollActualWidth,
                                    float initScrollActualHeight)
        {
            List<float> vals = new List<float>()
                    {
                        initScrollActualWidth,
                        initScrollActualHeight,
                        ImageWidth,
                        ImageHeight
                    };

            var canvmax = Math.Max(ImageHeight, ImageWidth);
            var scrolmax = Math.Max(initScrollActualHeight, initScrollActualWidth);

            if (canvmax > scrolmax)
            {//画像大きい
                ZoomRate = (float)(Math.Floor(vals.Min() / vals.Max() * ScaleDecimalPlace) / ScaleDecimalPlace);
            }
            else
            {//画像小さい
                float scrolmin = Math.Min(initScrollActualHeight, initScrollActualWidth);
                ZoomRate = (float)(Math.Floor(scrolmin / canvmax * ScaleDecimalPlace) / ScaleDecimalPlace);
            }
            CmpInitZoomRateImage?.Invoke(this, new EventArgs());
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
        void DoCaleInitScale(float scrollwidth, float scrollheight);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ZoomValue"></param>
        void SetZoomValue(float zoomValue);
    }
}
