using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SampleDataGrid.UserCtrl
{
    /// <summary>
    /// Interaction logic for TestUserControl
    /// </summary>
    public partial class TestUserControl : UserControl
    {
        public TestUserControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 表示テキスト
        /// </summary>
        public string MesText
        {
            get { return (string)GetValue(MesTextProperty); }
            set { SetValue(MesTextProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MesText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MesTextProperty =
            DependencyProperty.Register("MesText", typeof(string), typeof(TestUserControl), new PropertyMetadata("あああ"));
        /// <summary>
        /// 画像
        /// </summary>
        public ImageSource SendedImage
        {
            get { return (ImageSource)GetValue(SendedImageProperty); }
            set { SetValue(SendedImageProperty, value); }
        }



        public int ZoomRate
        {
            get { return (int)GetValue(ZoomRateProperty); }
            set { SetValue(ZoomRateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZoomRate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomRateProperty =
            DependencyProperty.Register("ZoomRate", typeof(int), typeof(TestUserControl), new PropertyMetadata(0));



        // Using a DependencyProperty as the backing store for SendedImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SendedImageProperty =
            DependencyProperty.Register("SendedImage", typeof(ImageSource), typeof(TestUserControl), new PropertyMetadata(null));


        private void OnTargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            if (SendedImage != null&& System.IO.Path.IsPathRooted(MesText))
            {
                canvasBig.Zoom = ZoomRate;
                canvasBig.Source = SendedImage;
            }
        }
    }
}
