using CommonService.ImageToData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MainModel
{
    public class LoadImager : ILoadImager, IDisposable
    {
        /// <summary>
        /// 画像
        /// </summary>
        public BitmapImage DispImage { get; private set; }
        /// <summary>
        /// 画像
        /// </summary>
        public BitmapImage NullImage { get; private set; }
        /// <summary>
        /// パス
        /// </summary>
        public string ImgPath { get; private set; }
        /// <summary>
        /// 読込完了
        /// </summary>
        public event EventHandler CmpLoadImage;
        /// <summary>
        /// 読込クリア
        /// </summary>
        public event EventHandler ClearImage;
        /// <summary>
        /// 画像読み込みクラス
        /// </summary>
        public LoadImager(IInitModel init)
        {
            //_InitModel = init;
            //_InitModel.DoInit += (s, e) =>
            //{
            //    if (s is InitModel im)
            //    {
            //        ImgPath = im.CurrentDir;
            //        if (File.Exists(ImgPath))
            //        {
            //            NewMethod1(ImgPath);
            //        }
            //    }
            //};

            int max = ushort.MaxValue;

            int min = ushort.MinValue;

            int imgw = max - min;

            int imgb = imgw / 2;

            int width = 10;
            int height = 10;
            ushort[] data = new ushort[width * height];

            byte[] bdata = Service.MakeByteData(width, height, 16, data);

            uint[] lut = Service.UpdateLut(16, imgb, imgw, 1F);

            Bitmap bmp = Service.Convert(bdata, width, height, 16, lut);

            NullImage = Service.Bitmap2BitmapImage(bmp);
        }
        /// <summary>
        /// ファイル開
        /// </summary>
        /// <param name="path"></param>
        public void OpenFile(string path)
        {
            if(!string.IsNullOrEmpty(ImgPath))
            {
                ClearImage?.Invoke(this, new EventArgs());
            }

            if (File.Exists(path))
            {
                //_InitModel.SetDir(path);
                NewMethod1(path);
                CmpLoadImage?.Invoke(this, new EventArgs());
                //占有しないパターン-2
                //NewMethod(path);
            }
        }

            void NewMethod1(string path)
            {
                ImgPath = path;

                _ = Service.ConvertBitmapToGrayScale(new Bitmap(path), out ushort[] data, out int width, out int height);

                int max = data.Max();

                int min = data.Min();

                int imgw = max - min;

                int imgb = imgw / 2;

                byte[] bdata = Service.MakeByteData(width, height, 16, data);

                uint[] lut = Service.UpdateLut(16, imgb, imgw, 1F);

                Bitmap bmp = Service.Convert(bdata, width, height, 16, lut);

                DispImage = Service.Bitmap2BitmapImage(bmp);


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
        }

        /// <summary>
        /// 要求
        /// </summary>
        public void RequestImage()
        {
            //_InitModel?.Request();
        }
        public void DoClear() 
        {
            if (!string.IsNullOrEmpty(ImgPath))
            {
                ImgPath = null;
                ClearImage?.Invoke(this, new EventArgs());
            }
        }

        public void Dispose()
        {
            Debug.WriteLine("破棄");
        }
    }

    public interface ILoadImager
    {
        /// <summary>
        /// 読込クリア
        /// </summary>
        event EventHandler ClearImage;
        /// <summary>
        /// 読込完了
        /// </summary>
        event EventHandler CmpLoadImage;
        /// <summary>
        /// ファイル開
        /// </summary>
        /// <param name="path"></param>
        void OpenFile(string path);

        void DoClear();
        /// <summary>
        /// 要求
        /// </summary>
        void RequestImage();
    }
}
