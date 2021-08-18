using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_CC_Sample
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPF_CC_Sample"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPF_CC_Sample;assembly=WPF_CC_Sample"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class RotateLine_MouseDrag : Thumb
    {
        static RotateLine_MouseDrag()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RotateLine_MouseDrag), new FrameworkPropertyMetadata(typeof(RotateLine_MouseDrag)));
        }

        Point oldPoint;
        Point newPoint;
        int d;
        RotateTransform transe = new RotateTransform();

        public RotateLine_MouseDrag()
        {
            DragDelta += (s, e) =>
            {
                double centX = CanvasActualWidth / 2d;
                double centY = CanvasActualHeight / 2d;

                newPoint.X = oldPoint.X + e.HorizontalChange;
                newPoint.Y = oldPoint.Y + e.VerticalChange;

                double ddd = Math.Atan2(newPoint.X - centX, newPoint.Y - centY);
                if (ddd < 0)
                {
                    ddd += Math.PI;
                }
                var angle = Math.Tan(ddd) * 180 / Math.PI;

                Debug.WriteLine($"new {Math.Tan(ddd) * 180 / Math.PI}");

                var a = -Math.Tan(ddd);

                if (a < 0)
                {
                    var b = -a * centX;
                    var exx = -b / a;
                    var exxx = (CanvasActualHeight - b) / a;
                    Canvas.SetLeft(this, exx);
                    Canvas.SetTop(this, 0);
                    Canvas.SetBottom(this, CanvasActualHeight);
                    Canvas.SetRight(this, exxx);
                }
                else 
                {
                    var b = a * centX;
                }

                //double expectposiX = Canvas.GetLeft(this) + e.HorizontalChange;
                //double expectposiY = Canvas.GetTop(this) + e.VerticalChange ;
                //if (expectposi > 0 && expectposi <= CanvasActualWidth)
                //{
                //    Canvas.SetLeft(this, expectposi);
                //}
            };

            DragStarted += (s, e) => 
            {

            };

            MouseMove += (s, e) =>
            {
                List<Point> fourps = new List<Point>()
                {
                    new Point(0,0),
                    new Point(CanvasActualWidth,0),
                    new Point(0,CanvasActualHeight),
                    new Point(CanvasActualWidth,CanvasActualHeight),
                };

                double centX = CanvasActualWidth / 2d;
                double centY = CanvasActualHeight / 2d;
                //double twopi = Math.PI / 2;

                Point point = e.GetPosition(this);

                oldPoint = e.GetPosition(this);

                double ddd = Math.Atan2(point.X - centX, point.Y - centY);

                if (ddd < 0)
                {
                    ddd += Math.PI;
                }
                var angle = Math.Tan(ddd) * 180 / Math.PI;

                Debug.WriteLine($"old {angle.ToString()}");

                this.Cursor = Cursors.SizeNESW;

                //var radians = Math.Atan2(CurrentLeft, CurrentTop);


                //double lineTop = Canvas.GetTop(this);
                //double lineLeft = Canvas.GetLeft(this);
                //double lineBottom = Canvas.GetBottom(this);
                //double lineRight = Canvas.GetRight(this);




                //if (Math.Abs(point.Y - expectposiTop) > 5.0 
                //    && Math.Abs(point.X - expectposiLeft) > 5.0 
                //     && Math.Abs(Angle) > 45)
                //{.SizeNS;
                //}

                //    this.Cursor = Cursors

                //Thumb b = s as Thumb;
                //if (Mouse.Captured == b)
                //{
                //    Point origin = new Point(CanvasActualWidth / 2, CanvasActualHeight / 2);
                //    var rawPoint = Mouse.GetPosition(b);
                //    var transPoint = new Point(rawPoint.X - origin.X, rawPoint.Y - origin.Y);
                //    var radians = Math.Atan2(transPoint.Y, transPoint.X);
                //    var angle = radians * (180 / Math.PI);
                //    transe.Angle = angle;
                //    this.RenderTransform = transe;
                //}
                //    double expectposi = Canvas.GetLeft(this);
                //Point point = e.GetPosition(this);
                //if (Math.Abs(point.X - expectposi) > 5.0)
                //{
                //    this.Cursor = Cursors.SizeWE;
                //}
            };
        }



        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(RotateLine_MouseDrag), new PropertyMetadata(0d));



        public int CanvasActualWidth
        {
            get { return (int)GetValue(CanvasActualWidthProperty); }
            set { SetValue(CanvasActualWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanvasActualWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasActualWidthProperty =
            DependencyProperty.Register("CanvasActualWidth", typeof(int), typeof(RotateLine_MouseDrag), new PropertyMetadata(0));



        public int CanvasActualHeight
        {
            get { return (int)GetValue(CanvasActualHeightProperty); }
            set { SetValue(CanvasActualHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanvasActualHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasActualHeightProperty =
            DependencyProperty.Register("CanvasActualHeight", typeof(int), typeof(RotateLine_MouseDrag), new PropertyMetadata(0));



    }
}
