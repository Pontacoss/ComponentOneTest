using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComponentOneTest
{
    /// <summary>
    /// Window3.xaml の相互作用ロジック
    /// </summary>
    public partial class Window3 : Window
    {
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
            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri("C:\\Users\\ey28754\\Pictures\\Screenshots\\Oracle.png");
            img.EndInit();

            // Ribbonにある画像取り込み機能で取り込んだImageがある場合
            // そのImageを格納するC1InlineUIContainerからDataTemplateを取得
            DataTemplate? contentTemp =  GetDataTemplate();

            // C1InlineUIContainerを作成しContentにImageを設定しても
            // ContentTemplateが適切に設定されていないと画像が表示されない。
            // ContentTemplateの設定の仕方が分からない。
            // Ribbonの機能を使って取り込んだ画像のDataTemplateをコピーして使えば上手くいく。
            var container = new C1InlineUIContainer
            {
                Content = img,
                ContentTemplate = contentTemp != null ? contentTemp : new DataTemplate(typeof(Image))
            };
            
            var paragraph = new C1Paragraph();
            paragraph.Children.Add(new C1Run());
            paragraph.Children.Add(container);
            paragraph.Children.Add(new C1Run());
            rtb.Document.Blocks.Add(paragraph);

            Image.Source = img;
        }

        private DataTemplate? GetDataTemplate()
        {
            foreach (var parag in rtb.Document.Children.OfType<C1Paragraph>())
            {
                foreach (var item in parag.Children.OfType<C1InlineUIContainer>())
                {
                    return item.ContentTemplate;
                }
            }
            return null;
        }

    }
}
