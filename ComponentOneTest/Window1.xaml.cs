using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ComponentOneTest
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        private C1RichTextBox _rtb;
        public Window1()
        {
            InitializeComponent();

            _rtb = this.c1RichTextBox1;
            _rtb.ElementMouseLeftButtonDown += rtb_ElementMouseLeftButtonDown;
            _rtb.Text = "＜試験仕様＞ \r\nHCN-P827： 6.1項を参照すること。" +
                " \r\n\r\n＜所内向け追加指示および注意事項＞ \r\n" +
                "銘板、表記は外形図：H7R2149と一致していることを確認する。" +
                " \r\n\r\nその他詳細は、伊ミキ-63811に従うこと。なお伊ミキ-63811は以下の項目を記載している。" +
                " \r\n3.1\t一般外観検査\t\t\t3.9\tカバー状態検査" +
                " \r\n3.2\t絶縁物検査\t\t\t3.10\t表面処理検査 \r\n" +
                "3.3\t取付け状態検査\t\t3.11\t表示検査 \r\n3.4\t締付け状態検査\t\t" +
                "3.12\t銘板検査 \r\n3.5\t配線及び結線検査\t\t3.13\t付属品検査 \r\n" +
                "3.6\t動作調整検査\t\t\t3.14\t配管検査 \r\n3.7\t電導接触部検査\t\t" +
                "3.15\t光ファイバーケーブル配線検査 \r\n3.8\t寸法検査\r\n\r\n" +
                "IECの項目分けに従い、配線チェックをVisual Inspectionの項目に統合している。配線チェックを忘れず行うこと。" +
                " \r\n組立図：　H14E673～H14E677\t \r\nWIRING DIAGRAM (主回路): H14E516 \r\nWIRING DIAGRAM (制御回路): H14E695 ";

        }

        void rtb_ElementMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.Modifiers != 0)
            {
                // コントロール座標で位置を取得します
                var pt = e.GetPosition(_rtb);
                // その位置のテキストポインタを取得します
                var pointer = _rtb.GetPositionFromPoint(pt);
                // ポインタが C1Run をポイントしていることを確認します
                var run = pointer.Element as C1Run;
                if (run != null)
                {
                    // C1Run 内の単語を取得します
                    var text = run.Text;
                    var start = pointer.Offset;
                    var end = pointer.Offset;
                    while (start > 0 && char.IsLetterOrDigit(text, start - 1))
                        start--;
                    while (end < text.Length - 1 && char.IsLetterOrDigit(text, end + 1))
                        end++;
                    // クリックされたランの太字プロパティを切り替えます
                    var word = new C1TextRange(pointer.Element, start, end - start + 1);
                    word.FontWeight =
                      word.FontWeight.HasValue && word.FontWeight.Value == FontWeights.Bold
                        ? FontWeights.Normal
                      : FontWeights.Bold;
                    word.Foreground =
                        word.Foreground == Brushes.Crimson
                        ? Brushes.Black
                        : Brushes.Crimson;
                }
            }
        }

        private void InsertParameter_Click(object sender, RoutedEventArgs e)
        {
            C1TextRange selectText = this.c1RichTextBox1.Selection;
            //FontWeight? fw = this.c1RichTextBox1.Selection.FontWeight;
            //this.c1RichTextBox1.Selection.FontWeight = fw.HasValue && fw.Value == FontWeights.Bold
            //      ? FontWeights.Normal
            //      : FontWeights.Bold;

            var stat = selectText.Start;
            var statRun = stat.Element as C1Run;

            if (statRun == null) return;

            var parent = statRun.Parent;
            if (0 < stat.Offset && stat.Offset < statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, new TSRParameter("パラメータ"));
                parent.Children.Insert(statRun.Index + 2, new C1Run()
                {
                    Text = statRun.Text.Substring(stat.Offset, statRun.Text.Length - stat.Offset)
                });
                statRun.Text = statRun.Text.Substring(0, stat.Offset);
            }
            else if (stat.Offset == 0)
            {
                parent.Children.Insert(statRun.Index, new TSRParameter("パラメータ"));
            }
            else if (stat.Offset == statRun.Text.Length)
            {
                parent.Children.Insert(statRun.Index + 1, new TSRParameter("パラメータ"));
            }
        }

        private void ShowParameterList_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<TSRParameter>();

            foreach (var paragraph in _rtb.Document.Children)
            {
                
                foreach (var param in paragraph.Children.OfType<TSRParameter>())
                {
                    var index = paragraph.Index;
                    var index2=param.Index;
                    list.Add(param);
                }
            }
            dg1.ItemsSource = list;
            tb1.Text = _rtb.Html
                .Replace(">", ">\n")
                .Replace(".c","\n.c");
            tb2.Text = _rtb.Text;
        }
    }
}
