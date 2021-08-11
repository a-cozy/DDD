using System;
using System.Collections.Generic;
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
                double expectposi = Canvas.GetLeft(this) + e.HorizontalChange;
                //if (expectposi > 0 && expectposi <= CanvasActualWidth)
                //{
                //    Canvas.SetLeft(this, expectposi);
                //}
            };

            MouseMove += (s, e) =>
            {
                Thumb b = s as Thumb;
                if (Mouse.Captured == b)
                {

                    var d = this.DataContext;
                    //this.designerItem = DataContext as ContentControl;
                    //Canvas sss is e.Source;
                    //{
                    
                    //}
                    //var dd = e.Source;




                    Point origin = new Point(b.ActualWidth / 2, b.ActualHeight / 2);
                    var rawPoint = Mouse.GetPosition(b);
                    var transPoint = new Point(rawPoint.X - origin.X, rawPoint.Y - origin.Y);
                    var radians = Math.Atan2(transPoint.Y, transPoint.X);
                    var angle = radians * (180 / Math.PI);
                    transe.Angle = angle;
                    this.RenderTransform = transe;
                }
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


    }
}
