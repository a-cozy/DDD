using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Runtime;
using System;
using System.IO;
using System.Linq;

namespace UT_AppDisp
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// pythonライブラリを共有して使うための変数
        /// </summary>
        public dynamic np;

        [TestMethod]
        public void TestMethod1()
        {
            // python環境にパスを通す
            // TODO: 環境に合わせてパスを直すこと
            var PYTHON_HOME = Environment.ExpandEnvironmentVariables(@"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python37_64");
            // pythonnetが、python本体のDLLおよび依存DLLを見つけられるようにする
            AddEnvPath(
              PYTHON_HOME,
              Path.Combine(PYTHON_HOME, @"Library\bin")
            );

        //    // python環境に、PYTHON_HOME(標準pythonライブラリの場所)を設定
            PythonEngine.PythonHome = PYTHON_HOME;

        //    // pythonの処理をする＝numpyの定義とバージョンをラベルに表示させる
            using (Py.GIL())
            {
        //        // numpyのインポート
                np = Py.Import("numpy");
        //        // numpyのバージョンを変数に格納
                dynamic np_version = np.__version__;
        //        // string型にして文字列と連結させラベルに表示
        //        var dd = "numpyバージョン：" + np_version.ToString();
            }
        }

        /// <summary>
        /// プロセスの環境変数PATHに、指定されたディレクトリを追加する(パスを通す)。
        /// </summary>
        /// <param name="paths">PATHに追加するディレクトリ。</param>
        public static void AddEnvPath(params string[] paths)
        {
            var envPaths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).ToList();
            foreach (var path in paths)
            {
                if (path.Length > 0 && !envPaths.Contains(path))
                {
                    envPaths.Insert(0, path);
                }
            }
            Environment.SetEnvironmentVariable("PATH", string.Join(Path.PathSeparator.ToString(), envPaths), EnvironmentVariableTarget.Process);
        }
    }
}
