using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    /// <summary>
    /// 画像表示クラス
    /// </summary>
    public class ImageDisplay: IImageDisplay
    {
        /// <summary>
        /// 画像更新
        /// </summary>
        public event EventHandler ChangedImage;
        /// <summary>
        /// 画像表示情報
        /// </summary>
        public ImageDispInf ImageDispInf { get; private set; }
        /// <summary>
        /// 画像の倍率調整
        /// </summary>
        private readonly IImageScaleControlor _ScaleCtrl;
        /// <summary>
        /// ビットマップ
        /// </summary>
        private readonly IImageArrayToBitmap _ImageBitmap;
        /// <summary>
        /// 画像表示クラス
        /// </summary>
        /// <param name="scalectrl"></param>
        /// <param name="imagebitmap"></param>
        public ImageDisplay(IImageScaleControlor scalectrl, IImageArrayToBitmap imagebitmap)
        {
            _ScaleCtrl = scalectrl;
            _ScaleCtrl.EndSetActualSize += (s, e) =>
            {
                if (ImageDispInf != null && !ImageDispInf.IsSetRangeScalse)
                {
                    ImageScaleControlor tmpctrl = s as ImageScaleControlor;

                    ImageDispInf.ActualSize_Height = tmpctrl.ActualSize_Height;
                    ImageDispInf.ActualSize_Width = tmpctrl.ActualSize_Width;
                    ImageDispInf.MaxDispScale = tmpctrl.MaxScale;
                    ImageDispInf.MinDispScale = tmpctrl.MinScale;
                    ImageDispInf.CurrentDispScale = tmpctrl.CurrentScale;
                }
            };
            _ImageBitmap = imagebitmap;
            _ImageBitmap.EndConvertBitmap += (s, e) => 
            {
                ImageDispInf = (s as ImageArrayToBitmap).ImageDispInf;
            };
        }
    }
    /// <summary>
    /// 画像表示
    /// </summary>
    public interface IImageDisplay
    {
        event EventHandler ChangedImage;

    }
}
