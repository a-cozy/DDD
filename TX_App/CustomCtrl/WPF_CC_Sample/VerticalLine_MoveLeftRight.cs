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
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:VerticalLine_MoveUpDownHorizontal/>
    ///
    /// </summary>
    public class VerticalLine_MoveLeftRight : Thumb
    {
        static VerticalLine_MoveLeftRight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VerticalLine_MoveLeftRight), new FrameworkPropertyMetadata(typeof(VerticalLine_MoveLeftRight)));
        }

        public VerticalLine_MoveLeftRight()
        {
            DragDelta += (s, e) =>
            {
                double expectposi = Canvas.GetLeft(this) + e.HorizontalChange;
                if (expectposi > 0 && expectposi <= CanvasActualWidth)
                {
                    Canvas.SetLeft(this, expectposi);
                }
            };

            MouseMove += (s, e) =>
            {
                double expectposi = Canvas.GetLeft(this);
                Point point = e.GetPosition(this);
                if (Math.Abs(point.X - expectposi) > 5.0)
                {
                    this.Cursor = Cursors.SizeWE;
                }
            };
        }
        /// <summary>
        /// キャンバス　横サイズ
        /// </summary>
        public int CanvasActualWidth
        {
            get { return (int)GetValue(CanvasActualWidthProperty); }
            set { SetValue(CanvasActualWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CanvasActualWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanvasActualWidthProperty =
            DependencyProperty.Register("CanvasActualWidth", typeof(int), typeof(VerticalLine_MoveLeftRight), new PropertyMetadata(0));


    }
}
