using C1.WPF.Core;
using C1.WPF.RichTextBox;
using C1.WPF.RichTextBox.Documents;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;



namespace ComponentOneTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private C1RichTextBox _rtb;
        public MainWindow()
        {
            InitializeComponent();


            _rtb = this.c1RichTextBox1;
            _rtb.Document = DocumentAssembly(typeof(C1RichTextBox).Assembly);
            _rtb.IsReadOnly = true;

            _rtb.KeyDown += rtb2_KeyDown;
            _rtb.ElementMouseLeftButtonDown += rtb_ElementMouseLeftButtonDown;
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

        void rtb2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                foreach (var heading2 in _rtb.Document.Blocks.OfType<Normal>())
                {
                    var text = heading2.ContentRange.Text;
                    heading2.ContentRange.Text = text.ToLower();
                }
            }
        }


        void DocumentMethod(C1Document doc, MethodInfo mi)
        {
            if (mi.IsSpecialName)
                return;
            doc.Blocks.Add(new Heading4(mi.Name));
            var parms = new StringBuilder();
            foreach (var parm in mi.GetParameters())
            {
                if (parms.Length > 0)
                    parms.Append(", ");
                parms.AppendFormat("{0} {1}", parm.ParameterType.Name, parm.Name);
            }
            var text = string.Format("public {0} {1}({2})",
             mi.ReturnType.Name,
             mi.Name,
             parms.ToString());
            doc.Blocks.Add(new Normal(text));
        }
        void DocumentEvent(C1Document doc, EventInfo ei)
        {
            doc.Blocks.Add(new Heading4(ei.Name));
            var text = string.Format("public {0} {1}",
             ei.EventHandlerType.Name,
             ei.Name);
            doc.Blocks.Add(new Normal(text));
        }
        void DocumentProperty(C1Document doc, PropertyInfo pi)
        {
            if (pi.PropertyType.ContainsGenericParameters)
                return;
            doc.Blocks.Add(new Heading4(pi.Name));
            var text = string.Format("public {0} {1} {{ {2}{3} }}",
             pi.PropertyType.Name,
             pi.Name,
             pi.CanRead ? "get; " : string.Empty,
             pi.CanWrite ? "set; " : string.Empty);
            doc.Blocks.Add(new Normal(text));
        }
        void DocumentType(C1Document doc, Type t)
        {
            // 非パブリック/ジェネリックはスキップします
            if (!t.IsPublic || t.ContainsGenericParameters)
                return;
            // タイプ
            doc.Blocks.Add(new Heading2("Class " + t.Name));
            // プロパティ
            doc.Blocks.Add(new Heading3("Properties"));
            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (pi.DeclaringType == t)
                    DocumentProperty(doc, pi);
            }
            // メソッド
            doc.Blocks.Add(new Heading3("Methods"));
            foreach (MethodInfo mi in t.GetMethods())
            {
                if (mi.DeclaringType == t)
                    DocumentMethod(doc, mi);
            }
            // イベント
            doc.Blocks.Add(new Heading3("Events"));
            foreach (EventInfo ei in t.GetEvents())
            {
                if (ei.DeclaringType == t)
                    DocumentEvent(doc, ei);
            }
        }
        C1Document DocumentAssembly(Assembly asm)
        {
            // ドキュメントを作成します
            C1Document doc = new C1Document();
            doc.FontFamily = new FontFamily("Tahoma");
            // アセンブリ
            doc.Blocks.Add(new Heading1("Assembly\r\n" + asm.FullName.Split(',')[0]));
            // タイプ
            foreach (Type t in asm.GetTypes())
                DocumentType(doc, t);
            // Done
            return doc;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

            foreach (var obj in _rtb.Document.Blocks)
            {
                foreach (var param in obj.Children.OfType<TSRParameter>())
                {
                    Debug.WriteLine(" TEST : {0} : {1}  ::{2} ", obj.Index, param.Index, param.Text);
                }
            }

        }
    }
}