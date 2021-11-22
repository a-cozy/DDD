using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Unity;

namespace MainModel
{
    /// <summary>
    /// データ読込
    /// </summary>
    public class LoadData : ILoadData, IDisposable
    {
        /// <summary>
        /// SHA計算完了
        /// </summary>
        public event EventHandler EndLoadData;
        /// <summary>
        /// SHA256
        /// </summary>
        public string ID_SHA256 { get; private set; }
        /// <summary>
        /// FileFullPath
        /// </summary>
        public string FileFullPath { get; private set; }
        /// <summary>
        /// データを読込　
        /// </summary>
        /// <param name="path"></param>
        public void DoLoadData(string path)
        {
            string result = "";
            using (SHA256 sha = new SHA256CryptoServiceProvider())
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] hash_sha256 = sha.ComputeHash(fileStream);
                    foreach (int idx in Enumerable.Range(0, hash_sha256.Length))
                    {
                        result = result + string.Format("{0:X2}", hash_sha256[idx]);
                    }
                }
            }

            ID_SHA256 = result;

            FileFullPath = path;

            EndLoadData?.Invoke(this, new EventArgs());
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
    /// データ読込 I/F
    /// </summary>
    public interface ILoadData
    {
        /// <summary>
        /// データ読込完了
        /// </summary>
        event EventHandler EndLoadData;
        /// <summary>
        /// データ読込指示
        /// </summary>
        /// <param name="path"></param>
        void DoLoadData(string path);
    }
}
