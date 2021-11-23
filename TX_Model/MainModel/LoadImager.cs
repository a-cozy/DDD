using CommonService.ImageToData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MainModel
{
    public class LoadImager : ILoadImager, IDisposable
    {
        /// <summary>
        /// 画像情報
        /// </summary>
        public ImageDispInf ImageDispInfs { get; private set; }
        /// <summary>
        /// SHA256
        /// </summary>
        public string ID_SHA256 { get; private set; }
        /// <summary>
        /// 読込完了
        /// </summary>
        public event EventHandler EndLoadImage;
        /// <summary>
        /// データ読込I/F
        /// </summary>
        private readonly ILoadData _LoadData;
        /// <summary>
        /// 画像読み込みクラス
        /// </summary>
        public LoadImager(ILoadData loaddata)
        {
            _LoadData = loaddata;
            _LoadData.EndLoadData += (s, e) => 
            {
                ID_SHA256 = (s as LoadData).ID_SHA256;
                PathToBitmapImage((s as LoadData).FileFullPath);
            };
        }
        /// <summary>
        /// ファイル開
        /// </summary>
        /// <param name="path"></param>
        public void OpenFile(string path)
        {
            if (File.Exists(path))
            {
                _LoadData.DoLoadData(path);
            }
            else
            {
                throw new Exception($"{path} isn't exist!");
            }
        }
        /// <summary>
        /// pathからbitmapImageに変更
        /// </summary>
        /// <param name="path"></param>
        void PathToBitmapImage(string path)
        {
            _ = Service.ConvertBitmapToGrayScale(new Bitmap(path), out ushort[] data, out int width, out int height);

            ImageDispInfs = new ImageDispInf
            {
                ID_sha256 = ID_SHA256,
                ImgPath = path,
                ImgArray = data,
                Width  = width,
                Height = height,
                MaxValue = data.Max(),
                MinValue = data.Min(),
            };

            EndLoadImage?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            Debug.WriteLine("破棄");
        }
    }
    /// <summary>
    /// 画像表示情報
    /// </summary>
    public class ImageDispInf
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID_sha256 { get; set; }
        /// <summary>
        /// 画像
        /// </summary>
        public BitmapImage DispImage { get; set; }
        /// <summary>
        /// パス
        /// </summary>
        public string ImgPath { get; set; }
        /// <summary>
        /// 画像Array
        /// </summary>
        public ushort[] ImgArray { get; set; }
        /// <summary>
        /// LUT
        /// </summary>
        public uint[] LUT { get; set; }
        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 諧調　WL
        /// </summary>
        public int WL { get; set; }
        /// <summary>
        /// 諧調 WW
        /// </summary>
        public int WW { get; set; }
        /// <summary>
        /// 輝度最大値
        /// </summary>
        public int MaxValue { get; set; }
        /// <summary>
        /// 輝度最大値
        /// </summary>
        public int MinValue { get; set; }
        /// <summary>
        /// 実際の表示サイズ
        /// </summary>
        public float ActualSize_Width { get; set; }
        /// <summary>
        /// 実際の表示サイズ
        /// </summary>
        public float ActualSize_Height { get; set; }
        /// <summary>
        /// 現在の表示スケールサイズ
        /// </summary>
        public float CurrentDispScale { get; set; }
        /// <summary>
        /// 最小の表示スケールサイズ
        /// </summary>
        public float MinDispScale { get; set; }
        /// <summary>
        /// 最大の表示スケールサイズ
        /// </summary>
        public float MaxDispScale { get; set; }
        /// <summary>
        /// スケールを設定したか？
        /// </summary>
        public bool IsSetRangeScalse { get; set; }
    }
    /// <summary>
    /// 画像ロード
    /// </summary>
    public interface ILoadImager
    {
        /// <summary>
        /// 読込完了
        /// </summary>
        event EventHandler EndLoadImage;
        /// <summary>
        /// ファイル開
        /// </summary>
        /// <param name="path"></param>
        void OpenFile(string path);
    }
}
