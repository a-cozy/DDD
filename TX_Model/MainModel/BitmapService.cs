using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CommonService.ImageToData
{
    public static class Service
    {
        /// <summary>
        /// 指定した画像からグレースケール画像を作成する
        /// </summary>
        /// <param name="img">基の画像</param>
        /// <returns>作成されたグレースケール画像</returns>
        public static bool ConvertBitmapToGrayScale(Bitmap bmp, out ushort[] data,out int width, out int height)
        {
            data = new ushort[bmp.Width * bmp.Height];
            width = bmp.Width;
            height = bmp.Height;
            //newImgのGraphicsオブジェクトを取得
            BitmapData bmpdata = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
            );

            // グレイスケール用のアレ
            int rp = (int)(0.298912 * 1024);
            int gp = (int)(0.586611 * 1024);
            int bp = (int)(0.114478 * 1024);

            // バイト配列にコピー
            byte[] ba = new byte[bmp.Width * bmp.Height * 4];
            Marshal.Copy(bmpdata.Scan0, ba, 0, ba.Length);

            // 処理
            int pixsize = bmp.Width * bmp.Height * 4;
            int idx = 0;
            for (int i = 0; i < pixsize; i += 4)
            {
                // 画像のバイト列って、ARGB じゃなくて、BGRAになってるっぽい？？
                byte g = (byte)((bp * ba[i + 0] + gp * ba[i + 1] + rp * ba[i + 2]) >> 10);
                ba[i + 0] = g;      // ブルー
                ba[i + 1] = g;      // グリーン
                ba[i + 2] = g;      // レッド
                ba[i + 3] = 0xFF;   // アルファ
                data[idx] = (ushort)(g);
                idx++;
            }

            // 元のところに書き込む。
            //Marshal.Copy(ba, 0, bmpdata.Scan0, ba.Length);

            bmp.UnlockBits(bmpdata);

            return true;
        }

        public static Array FileConvertArray(string filename, out System.Windows.Media.PixelFormat format, out int width, out int height)
        {
            Array arr;

            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                BitmapFrame bitmapFrame = BitmapFrame.Create(
                    fs,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default
                    )
                    ;

                width = bitmapFrame.PixelWidth;
                height = bitmapFrame.PixelHeight;
                format = bitmapFrame.Format;

                BitmapMetadata metadata = bitmapFrame.Metadata as BitmapMetadata;

                var dddd = metadata.Comment;

                int stride = ((width * format.BitsPerPixel + 31) / 32) * 4;

                if (format == PixelFormats.Gray16)
                {
                    arr = new ushort[width * height];
                }
                else if (format == PixelFormats.Rgb48)
                {
                    arr = new ushort[width * 3 * height];
                }
                else
                {
                    arr = new byte[stride * height];
                }

                // 輝度データを配列へコピー
                bitmapFrame.CopyPixels(arr, stride, 0);
            }
            return arr;
        }

        public static Bitmap Convert(byte[] input, int width, int height, int bits, uint[] lut)
        {
            var bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var rect = new Rectangle(0, 0, width, height);

            var bitmap_data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            ConvertCore(width, height, bits, input, bitmap_data, lut);

            bitmap.UnlockBits(bitmap_data);

            return bitmap;
        }

        static unsafe void ConvertCore(int width, int height, int bits, byte[] input, BitmapData output, uint[] lut)
        {
            ushort mask = (ushort)((1 << bits) - 1);

            int in_stride = output.Stride;
            int out_stride = width * 2;

            byte* out_data = (byte*)output.Scan0;

            fixed (byte* in_data = input)
            {
                for (int y = 0; y < height; y++)
                {
                    uint* out_row = (uint*)(out_data + (y * in_stride));
                    ushort* in_row = (ushort*)(in_data + (y * out_stride));

                    for (int x = 0; x < width; x++)
                    {
                        ushort in_pixel = (ushort)(in_row[x] & mask);
                        out_row[x] = lut[in_pixel];
                    }
                }
            }
        }

        public static byte[] MakeByteData(int width, int height, int bits, ushort[] data)
        {
            int max = 1 << bits;
            byte[] pixels = new byte[width * height * 2];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixel = (int)data[(y * width) + x];
                    int addr = ((y * width) + x) * 2;
                    pixels[addr + 1] = (byte)((pixel & 0xFF00) >> 8);
                    pixels[addr + 0] = (byte)((pixel & 0x00FF));
                }
            }

            return pixels;
        }

        public static uint[] UpdateLut(int bits, int bright, int contrast, float gamma)
        {
            // Create a linear LUT to convert from grayscale to ARGB
            int max_input = 1 << bits;
            int Low = bright - (contrast / 2);
            int High = bright + (contrast / 2);
            uint[] lut = new uint[max_input];
            float intensity = 0;

            ushort[] data = new ushort[max_input];

            for (int i = 0; i < max_input; i++)
            {
                if (i < Low)
                    intensity = 0;
                else if (i > High)
                    intensity = byte.MaxValue;
                else
                    intensity = ((float)(i - Low) / contrast) * (float)0xFF;

                lut[i] = (uint)(0xFF000000L | ((byte)intensity * 0x00010101L));
                data[i] = (ushort)intensity;
            }
            return lut;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        /// <summary>
        /// BmpをBitmapSourecに変換
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource BmpToWPFBmp(System.Drawing.Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();

            BitmapSource source;
            source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hBitmap);
            return source;
        }

        public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
