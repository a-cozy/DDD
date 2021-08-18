using CommonService.ImageToData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MainModel
{
    public class LoadImager : ILoadImager
    {
        /// <summary>
        /// 画像
        /// </summary>
        public BitmapImage DispImage { get; private set; }
        /// <summary>
        /// パス
        /// </summary>
        public string ImgPath { get; private set; }
        /// <summary>
        /// 読込完了
        /// </summary>
        public event EventHandler CmpLoadImage;

        private readonly IInitModel _InitModel;
        /// <summary>
        /// 画像読み込みクラス
        /// </summary>
        public LoadImager(IInitModel init)
        {
            _InitModel = init;
            _InitModel.DoInit += (s, e) => 
            {
                if(s is InitModel im)
                {
                    ImgPath = im.CurrentDir;
                    NewMethod(ImgPath);
                }
            };



        }
        /// <summary>
        /// ファイル開
        /// </summary>
        /// <param name="path"></param>
        public void OpenFile(string path)
        {

            _InitModel.SetDir(path);
            ImgPath = path;
            //_ = Service.ConvertBitmapToGrayScale(new Bitmap(path), out ushort[] data, out int width, out int height);

            //int max = data.Max();

            //int min = data.Min();

            //int imgw = max - min;

            //int imgb = imgw / 2;

            //byte[] bdata = Service.MakeByteData(width, height, 16, data);

            //uint[] lut = Service.UpdateLut(16,imgb, imgw, 1F);

            //Bitmap bmp = Service.Convert(bdata, width, height, 16, lut);

            //占有しないパターン-2
            NewMethod(path);
        }

        private void NewMethod(string path)
        {
            BitmapImage bmpImage = new BitmapImage();
            FileStream stream = File.OpenRead(path);
            bmpImage.BeginInit();
            bmpImage.CacheOption = BitmapCacheOption.OnLoad;
            bmpImage.StreamSource = stream;
            bmpImage.EndInit();
            stream.Close();
            DispImage = bmpImage;


            //DispImage = Service.Bitmap2BitmapImage(bmp);

            CmpLoadImage?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// 要求
        /// </summary>
        public void RequestImage()
        {

                _InitModel?.Request();
                //CmpLoadImage?.Invoke(this, new EventArgs());
            
        }
    }

    public interface ILoadImager
    {
        /// <summary>
        /// 読込完了
        /// </summary>
        event EventHandler CmpLoadImage;
        /// <summary>
        /// ファイル開
        /// </summary>
        /// <param name="path"></param>
        void OpenFile(string path);
        /// <summary>
        /// 要求
        /// </summary>
        void RequestImage();
    }
}
