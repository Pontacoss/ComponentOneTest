using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ComponentOneTest
{
    /// <summary>
    /// Window3.xaml の相互作用ロジック
    /// </summary>
    public partial class Window3 : Window
    {
        C1Length _height;
        C1Length _width;

        public Window3()
        {
            InitializeComponent();
            this.WindowStartupLocation=WindowStartupLocation.CenterScreen;
            rtb.ViewMode = TextViewMode.Print;
            rtb.PrintPageLayout.Width=839;
            rtb.PrintPageLayout.Height=993.4;
            rtb.Zoom = 1;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var img = new Image();
            img.BeginInit();
            img.Source = new BitmapImage(
                new Uri(@"C:\Users\ey28754\Pictures\Screenshots\Oracle.png",
                UriKind.Relative));
            img.EndInit();

            DataTemplate? contentTemp =  GetDataTemplate();

            // 既に取り込んだ画像がある場合、その画像のサイズを読み取って
            // 同じサイズで画像を追加
            var container = new C1InlineUIContainer
            {
                Content = img,
                Height = _height,
                Width = _width,
            };
            
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(new C1Run());
            paragraph.Children.Add(container);
            paragraph.Children.Add(new C1Run());
            rtb.Document.Blocks.Add(paragraph);

            //Image.Source = img;
        }

        private DataTemplate? GetDataTemplate()
        {
            foreach (var parag in rtb.Document.Children.OfType<C1Paragraph>())
            {
                foreach (var item in parag.Children.OfType<C1InlineUIContainer>())
                {
                    _height = item.Height;
                    _width = item.Width;
                    return item.ContentTemplate;
                }
            }
            return null;
        }

    }
}
