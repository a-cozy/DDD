using MainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Ioc;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using Unity;
using Unity.Injection;
using Unity.ObjectBuilder;
using Unity.Builder;

namespace UT_AppDisp
{
    [TestClass]
    public class UnitTest1
    {
        private static IUnityContainer ServiceRegist()
        {
            IUnityContainer service = new UnityContainer();

            service.RegisterType<IInitModel, InitModel>(TypeLifetime.PerContainer);

            service.RegisterType<ILoadData, LoadData>(TypeLifetime.PerContainer);

            service.RegisterType<ILoadImager, LoadImager>(TypeLifetime.PerContainer);

            service.RegisterType<IPoint2DegRadWin, Point2DegRadWin>(TypeLifetime.PerContainer);

            service.RegisterType<IPoint2Center, Point2Center>(TypeLifetime.PerContainer);

            service.RegisterType<ITestModelA, TestModelA>(TypeLifetime.PerContainer,new InjectionConstructor("a"));

            return service;
        }
        [TestMethod]
        public void 回転_画像テスト512_512_Point_0_0()
        {
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), "UTData", "Jpegs", "test512×512-0001.jpg");
            Assert.IsTrue(File.Exists(fullpath));
            using (IUnityContainer Service = ServiceRegist())
            {
                IPoint2Center dd = Service.Resolve<IPoint2Center>();
                ILoadImager ldimgservice = Service.Resolve<ILoadImager>();
                IPoint2DegRadWin lrservice = Service.Resolve<IPoint2DegRadWin>();
                lrservice.EndCalDeg += (s, e) => 
                {
                    Point2DegRadWin p2drw = s as Point2DegRadWin;
                    Assert.AreEqual(0F,Math.Round(p2drw.RotAngle, 2));
                    Assert.AreEqual(-1F, Math.Round(p2drw.RotSlope, 2));
                };

                ldimgservice.OpenFile(fullpath);
                lrservice.DoMouseDrage(new Point(0, 0));
            };
        }
        [TestMethod]
        public void 回転_画像テスト512_512_Point_512_512()
        {
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), "UTData", "Jpegs", "test512×512-0001.jpg");
            Assert.IsTrue(File.Exists(fullpath));
            using (IUnityContainer Service = ServiceRegist())
            {
                IPoint2DegRadWin lrservice = Service.Resolve<IPoint2DegRadWin>();
                lrservice.EndCalDeg += (s, e) =>
                {
                    Point2DegRadWin p2drw = s as Point2DegRadWin;
                    Assert.AreEqual(0F, Math.Round(p2drw.RotAngle, 2));
                    Assert.AreEqual(-1, Math.Round(p2drw.RotSlope, 2));
                };
                ILoadImager ldimgservice = Service.Resolve<ILoadImager>();
                ldimgservice.OpenFile(fullpath);
                lrservice.DoMouseDrage(new Point(512, 512));
            };
        }
        [TestMethod]
        public void 回転_画像テスト512_512_Point_0_512()
        {
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), "UTData", "Jpegs", "test512×512-0001.jpg");
            Assert.IsTrue(File.Exists(fullpath));
            using (IUnityContainer Service = ServiceRegist())
            {
                IPoint2DegRadWin lrservice = Service.Resolve<IPoint2DegRadWin>();
                lrservice.EndCalDeg += (s, e) =>
                {
                    Point2DegRadWin p2drw = s as Point2DegRadWin;
                    Assert.AreEqual(0F, Math.Round(p2drw.RotAngle, 2));
                    Assert.AreEqual(-1, Math.Round(p2drw.RotSlope, 2));
                };
                ILoadImager ldimgservice = Service.Resolve<ILoadImager>();
                ldimgservice.OpenFile(fullpath);
                lrservice.DoMouseDrage(new Point(0, 512));
            };
        }
        [TestMethod]
        public void 回転_画像テスト512_512_Point_512_0()
        {
            string fullpath = Path.Combine(Directory.GetCurrentDirectory(), "UTData", "Jpegs", "test512×512-0001.jpg");
            Assert.IsTrue(File.Exists(fullpath));
            using (IUnityContainer Service = ServiceRegist())
            {
                IPoint2DegRadWin lrservice = Service.Resolve<IPoint2DegRadWin>();
                lrservice.EndCalDeg += (s, e) =>
                {
                    Point2DegRadWin p2drw = s as Point2DegRadWin;
                    Assert.AreEqual(0F, Math.Round(p2drw.RotAngle, 2));
                    Assert.AreEqual(-1, Math.Round(p2drw.RotSlope, 2));
                };
                ILoadImager ldimgservice = Service.Resolve<ILoadImager>();
                ldimgservice.OpenFile(fullpath);
                lrservice.DoMouseDrage(new Point(512,0));
            };
        }
        [TestMethod]
        public void 画像テスト512_512()
        {
            string savedir = Path.Combine(Directory.GetCurrentDirectory(), "UTData", "Jpegs");
            Assert.IsTrue(Directory.Exists(savedir));         
            //ファイル検索
            string fullpath = Directory.EnumerateFiles(
                            savedir, // 検索開始ディレクトリ
                            $"*.jpg",
                            SearchOption.TopDirectoryOnly).ToList().Find(p=>Path.GetFileName(p)== "test512×512-0001.jpg");
            Assert.IsTrue(File.Exists(fullpath));
            using (IUnityContainer Service = ServiceRegist())
            {
                var test = Service.Resolve<ITestModelA>();
                test.Run();
                var ss = Service.Resolve<ILoadImager>();
                ss.CmpLoadImage += (s, e) => 
                {
                    if(s is LoadImager li)
                    {
                        Assert.AreEqual(512, li.DispImage.PixelWidth);
                        //Assert.AreEqual("512", Math.Round(li.DispImage.Width,0).ToString());
                        Assert.AreEqual(512, li.DispImage.PixelHeight);
                        //Assert.AreEqual("512", Math.Round(li.DispImage.Height, 0).ToString());
                    }
                };
                ss.OpenFile(fullpath);
            };
        }
        [TestMethod]
        public void 画像テスト812_512()
        {
            string savedir = Path.Combine(Directory.GetCurrentDirectory(), "UTData", "Jpegs");
            Assert.IsTrue(Directory.Exists(savedir));
            //ファイル検索
            string fullpath = Directory.EnumerateFiles(
                            savedir, // 検索開始ディレクトリ
                            $"*.jpg",
                            SearchOption.TopDirectoryOnly).ToList().Find(p => Path.GetFileName(p) == "test812×512-0001.jpg");
            Assert.IsTrue(File.Exists(fullpath));
            using (IUnityContainer Service = ServiceRegist())
            {
                var test = Service.Resolve<ITestModelA>();
                test.Run();
                var ss = Service.Resolve<ILoadImager>();
                ss.CmpLoadImage += (s, e) =>
                {
                    if (s is LoadImager li)
                    {
                        Assert.AreEqual(812, li.DispImage.PixelWidth);
                        //Assert.AreEqual("812", Math.Round(li.DispImage.Width, 0).ToString());
                        Assert.AreEqual(512, li.DispImage.PixelHeight);
                        //Assert.AreEqual("512", Math.Round(li.DispImage.Height, 0).ToString());
                    }
                };
                ss.OpenFile(fullpath);
            };
        }
        [TestMethod]
        public void 画像テスト1536_1536()
        {
            string savedir = Path.Combine(Directory.GetCurrentDirectory(), "UTData", "Jpegs");
            Assert.IsTrue(Directory.Exists(savedir));
            //ファイル検索
            string fullpath = Directory.EnumerateFiles(
                            savedir, // 検索開始ディレクトリ
                            $"*.jpg",
                            SearchOption.TopDirectoryOnly).ToList().Find(p => Path.GetFileName(p) == "test1536×1536-0001.jpg");
            Assert.IsTrue(File.Exists(fullpath));
            using (IUnityContainer Service = ServiceRegist())
            {
                var test = Service.Resolve<ITestModelA>();
                test.Run();
                var ss = Service.Resolve<ILoadImager>();
                ss.CmpLoadImage += (s, e) =>
                {
                    if (s is LoadImager li)
                    {
                        Assert.AreEqual(1536, li.DispImage.PixelWidth);
                        Assert.AreEqual("1536", Math.Round(li.DispImage.Width, 0).ToString());
                        Assert.AreEqual(1536, li.DispImage.PixelHeight);
                        Assert.AreEqual("1536", Math.Round(li.DispImage.Height, 0).ToString());
                    }
                };
                ss.OpenFile(fullpath);
            };
        }

        [TestMethod]
        public void テスト()
        {
            IUnityContainer cont = new UnityContainer();

            cont.RegisterType<ITestModelA, TestModelA>();

            cont.Resolve<ITestModelA>();


                

        }
    }
}
