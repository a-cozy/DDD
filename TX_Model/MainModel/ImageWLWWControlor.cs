using CommonService.ImageToData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    /// <summary>
    /// 画像表示制御
    /// </summary>
    public class ImageWLWWControlor : IImageWLWWControlor
    {
        /// <summary>
        /// 画像表示情報
        /// </summary>
        public ImageDispInf ImageDispInf { get; private set; }
        ///// <summary>
        ///// WindowLevel
        ///// </summary>
        //public int WindowLevel { get; private set; }
        ///// <summary>
        ///// WindowWidth
        ///// </summary>
        //public int WindowWidth { get; private set; }
        /// <summary>
        /// 画像表示準備完了
        /// </summary>
        public event EventHandler LUTChanged;
        /// <summary>
        /// 画像のビット値
        /// </summary>
        private const int _BitNum = 16;
        /// <summary>
        /// 画像ロードクラス I/F
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 
        /// </summary>
        public ImageWLWWControlor(ILoadImager li)
        {
            _LoadImage = li;
            _LoadImage.EndLoadImage += (s, e) => 
            {
                ImageDispInf = (s as LoadImager).ImageDispInfs;
                Cal_Default_LUT(ImageDispInf);
            };
        }
        /// <summary>
        ///LUT初期化 
        /// </summary>
        /// <param name="image"></param>
        public void Cal_Default_LUT(ImageDispInf image)
        {
            image.WW = image.MaxValue - image.MinValue;
            image.WL = image.WW / 2;
            image.LUT = Service.UpdateLut(_BitNum, image.WL, image.WW, 1F);
            ImageDispInf = image;
            LUTChanged?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// WindowLevel WindowWidthの変更
        /// </summary>
        /// <param name="WindowLevel"></param>
        /// <param name="WindowWidth"></param>
        public void Do_Cal_LUT(int windowlevel, int windowswidth, ImageDispInf image)
        {
            image.WW = windowswidth;
            image.WL = windowlevel;
            image.LUT = Service.UpdateLut(_BitNum, image.WL, image.WW, 1F);
            ImageDispInf = image;
            LUTChanged?.Invoke(this, new EventArgs());
        }
    }
    /// <summary>
    /// 画像表示制御I/F
    /// </summary>
    public interface IImageWLWWControlor
    {
        /// <summary>
        /// LUT変換完了
        /// </summary>
        event EventHandler LUTChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        void Cal_Default_LUT(ImageDispInf image);
        /// <summary>
        /// WindowLevel WindowWidthの変更
        /// </summary>
        /// <param name="WindowLevel"></param>
        /// <param name="WindowWidth"></param>
        void Do_Cal_LUT(int windowLevel, int windowWidth, ImageDispInf image);
    }
}
