using CommonService.ImageToData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    /// <summary>
    /// 画像のビットマップ変換
    /// </summary>
    public class ImageArrayToBitmap : IImageArrayToBitmap
    {
        /// <summary>
        /// 画像のビット値
        /// </summary>
        private const int _BitNum = 16;
        /// <summary>
        /// 画像表示情報
        /// </summary>
        public ImageDispInf ImageDispInf { get; private set; }
        /// <summary>
        /// bitmap変換完了
        /// </summary>
        public event EventHandler EndConvertBitmap;
        /// <summary>
        /// bitmap要求
        /// </summary>
        public event EventHandler RequestBitmap;
        /// <summary>
        /// WL/WW変換 I/F
        /// </summary>
        private readonly IImageWLWWControlor _WLWWCtrl;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wlwwctrl"></param>
        public ImageArrayToBitmap(IImageWLWWControlor wlwwctrl)
        {
            _WLWWCtrl = wlwwctrl;
            _WLWWCtrl.LUTChanged += (s, e) =>
            {
                ImageDispInf = (s as ImageWLWWControlor).ImageDispInf;
                DoConvertBitmap(ImageDispInf);
            };
        }
        /// <summary>
        /// bitmap変換
        /// </summary>
        public void DoConvertBitmap(ImageDispInf image)
        {
            byte[] bdata = Service.MakeByteData(image.Width, image.Height, _BitNum, image.ImgArray);
            Bitmap bmp = Service.Convert(bdata, image.Width, image.Height, 16, image.LUT);
            ImageDispInf.DispImage = Service.Bitmap2BitmapImage(bmp);
            EndConvertBitmap?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 要求
        /// </summary>
        public void Request() 
            => RequestBitmap?.Invoke(this, new EventArgs());
    }
    /// <summary>
    /// 画像のビットマップ変換I/F
    /// </summary>
    public interface IImageArrayToBitmap
    {
        /// <summary>
        /// bitmap変換完了
        /// </summary>
        event EventHandler EndConvertBitmap;
        /// <summary>
        /// bitmap要求
        /// </summary>
        event EventHandler RequestBitmap;
        /// <summary>
        /// bitmap変換
        /// </summary>
        void DoConvertBitmap(ImageDispInf image);
        /// <summary>
        /// bitmap要求
        /// </summary>
        void Request();
    }
}
