using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
        /// 最小桁数
        /// </summary>
        private int ScaleDecimalPlace { get; } = (int)Math.Pow(10, 2);
        /// <summary>
        /// スケール
        /// </summary>
        public float CurrentScale { get; private set; }
        /// <summary>
        /// 最大スケール
        /// </summary>
        public float MaxScale { get; private set; } = 4;
        /// <summary>
        /// 最小値
        /// </summary>
        public float MinScale { get; private set; }
        /// <summary>
        /// 表示サイズ 縦
        /// </summary>
        public float ActualSize_Height { get; private set; }
        /// <summary>
        /// 表示サイズ 縦
        /// </summary>
        public float ActualSize_Width { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ImageScaleControlor()
        {

        }
        /// <summary>
        /// ActualSizeを設定する
        /// </summary>
        /// <param name="width_actualsize"></param>
        /// <param name="height_actualsize"></param>
        public void SetActualSize(float width_actualsize, float height_actualsize, BitmapImage bitmapImage)
        {
            ActualSize_Height = height_actualsize;
            ActualSize_Width = width_actualsize;

            CalMinScaleSize(width_actualsize,height_actualsize,bitmapImage);
            EndSetActualSize?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// スケールサイズの最小値を計算するクラス
        /// </summary>
        public void CalMinScaleSize(float width_actualsize, float height_actualsize, BitmapImage bitmapImage)
        {
            List<float> vals = new List<float>()
                    {
                        height_actualsize,
                        width_actualsize,
                        (float)bitmapImage.Width,
                        (float)bitmapImage.Height,
                    };

            float canvmax = (float)Math.Max(bitmapImage.Width, bitmapImage.Height);

            float scrolmax = Math.Max(height_actualsize, width_actualsize);

            float scrolmin = Math.Min(height_actualsize, width_actualsize);

            if (canvmax > scrolmax)
            {//画像大きい
                var wrate = width_actualsize / bitmapImage.Width;
                var hrate = height_actualsize / bitmapImage.Height;

                if (wrate < hrate)
                {
                    MinScale = (float)(Math.Floor(wrate * ScaleDecimalPlace) / ScaleDecimalPlace);
                }
                else
                {
                    MinScale = (float)(Math.Floor(hrate * ScaleDecimalPlace) / ScaleDecimalPlace);
                }
            }
            else
            {//画像小さい
                MinScale = (float)(Math.Floor(scrolmin / (canvmax) * ScaleDecimalPlace) / ScaleDecimalPlace);
            }

            CurrentScale = MinScale;
        }
    }

    public interface IImageScaleControlor
    {
        /// <summary>
        /// ActualSizeを設定する
        /// </summary>
        /// <param name="width_actualsize"></param>
        /// <param name="height_actualsize"></param>
        void SetActualSize(float width_actualsize, float height_actualsize, BitmapImage bitmapImage);
        /// <summary>
        /// 最小値の取り出し
        /// </summary>
        /// <param name="image"></param>
        void CalMinScaleSize(float width_actualsize, float height_actualsize, BitmapImage bitmapImage);
        /// <summary>
        /// ActualSizeの設定完了
        /// </summary>
        event EventHandler EndSetActualSize;
    }
}
