using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    public class ImageScaleControlor : IImageScaleControlor
    {
        /// <summary>
        /// 画像表示情報
        /// </summary>
        public ImageDispInf ImageDispInf { get; private set; }
        /// <summary>
        /// ActualSizeの設定完了
        /// </summary>
        public event EventHandler EndSetActualSize;
        /// <summary>
        /// 画像ロードクラス I/F
        /// </summary>
        private readonly IImageArrayToBitmap _ImageInf;
        /// <summary>
        /// 
        /// </summary>
        public ImageScaleControlor(IImageArrayToBitmap imgartobi)
        {
            _ImageInf = imgartobi;
            _ImageInf.EndConvertBitmap += (s, e) =>
            {
                ImageDispInf = (s as ImageArrayToBitmap).ImageDispInf;
            };
        }
        /// <summary>
        /// ActualSizeを設定する
        /// </summary>
        /// <param name="width_actualsize"></param>
        /// <param name="height_actualsize"></param>
        public void SetActualSize(int width_actualsize, int height_actualsize)
        {

            ImageDispInf.ActualSize_Height = height_actualsize;


            EndSetActualSize?.Invoke(this, new EventArgs());
        }
    }

    public interface IImageScaleControlor
    {
        /// <summary>
        /// ActualSizeを設定する
        /// </summary>
        /// <param name="width_actualsize"></param>
        /// <param name="height_actualsize"></param>
        void SetActualSize(int width_actualsize,int height_actualsize);
        /// <summary>
        /// ActualSizeの設定完了
        /// </summary>
        event EventHandler EndSetActualSize;
    }
}
