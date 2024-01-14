using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        private C1RichTextBox _rtb;
        public Window1()
        {
            InitializeComponent();
            _rtb = this.c1RichTextBox1;
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

        private void InsertParameter_Click(object sender, RoutedEventArgs e)
        {
            C1TextRange text = this.c1RichTextBox1.Selection;
            //FontWeight? fw = this.c1RichTextBox1.Selection.FontWeight;
            //this.c1RichTextBox1.Selection.FontWeight = fw.HasValue && fw.Value == FontWeights.Bold
            //      ? FontWeights.Normal
            //      : FontWeights.Bold;

            var stat = text.Start;
            var statOffset = text.Start.Offset;
            var statRun = stat.Element as C1Run;
            if (statRun == null) return;
            var statRunIndex = statRun.Index;

            var parent = statRun.Parent;
            if (0 < statOffset && statOffset < statRun.Text.Length)
            {
                parent.Children.Insert(statRunIndex + 1, new TSRParameter("パラメータ"));
                parent.Children.Insert(statRunIndex + 2, new C1Run()
                {
                    Text = statRun.Text.Substring(statOffset, statRun.Text.Length - statOffset)
                });
                statRun.Text = statRun.Text.Substring(0, statOffset);
            }
            else if (statOffset == 0)
            {
                parent.Children.Insert(statRunIndex, new TSRParameter("パラメータ"));
            }
            else if (statOffset == statRun.Text.Length)
            {
                parent.Children.Insert(statRunIndex + 1, new TSRParameter("パラメータ"));
            }
        }

        private void ShowParameterList_Click(object sender, RoutedEventArgs e)
        {
            dg1.ItemsSource = null;
            
            var list = new List<TSRParameter>();

            foreach (var obj in _rtb.Document.Blocks)
            {
                foreach (var param in obj.Children.OfType<TSRParameter>())
                {
                    list.Add(param);
                }
            }

            dg1.ItemsSource = list;
            tb1.Text=_rtb.Text;
        }
    }
}
