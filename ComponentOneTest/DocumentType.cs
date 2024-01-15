using C1.WPF.RichTextBox.Documents;
using System.Windows.Media;
using System.Windows;

namespace ComponentOneTest
{
    class Normal : C1Paragraph
    {
        public Normal() { }
        public Normal(string text)
        {
            this.Inlines.Add(new C1Run() { Text = text });
            this.Padding = new Thickness(30, 0, 0, 0);
            this.Margin = new Thickness(0);
        }
    }

    class Heading : Normal
    {
        public Heading(string text) : base(text)
        {
            this.FontWeight = FontWeights.Bold;
        }
    }
    class Heading1 : Heading
    {
        public Heading1(string text) : base(text)
        {
            this.Background = new SolidColorBrush(Colors.Yellow);
            this.FontSize = 24;
            this.Padding = new Thickness(0, 10, 0, 10);
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(3, 1, 1, 0);
        }
    }
    class Heading2 : Heading
    {
        public Heading2(string text) : base(text)
        {
            this.FontSize = 18;
            this.FontStyle = FontStyles.Italic;
            this.Background = new SolidColorBrush(Colors.Yellow);
            this.Padding = new Thickness(10, 5, 0, 5);
            this.BorderBrush = new SolidColorBrush(Colors.Black);
            this.BorderThickness = new Thickness(3, 1, 1, 1);
        }
    }
    class Heading3 : Heading
    {
        public Heading3(string text) : base(text)
        {
            this.FontSize = 14;
            this.Background = new SolidColorBrush(Colors.LightGray);
            this.Padding = new Thickness(20, 3, 0, 0);
        }
    }
    class Heading4 : Heading
    {
        public Heading4(string text) : base(text)
        {
            this.FontSize = 14;
            this.Padding = new Thickness(30, 0, 0, 0);
        }
    }

    class Revision : C1Run
    {
        public Revision() { }
        public Revision(string rev)
        {
            Text = rev;
            BorderThickness = new Thickness(1);
        }
    }

    class TSRParameter : C1Run
    {
        static int count;
        public TSRParameter() { }
        public TSRParameter(string name)
        {
            count++;
            Text = "[[" + name + ":"+count+"]]";
            IsEditable = false;
            //VerticalAlignment = C1VerticalAlignment.Super;
            Background= new SolidColorBrush(Colors.Pink);
            //BorderThickness = new Thickness(1);
            Padding=new Thickness(0,0,0,0);
        }
    }
}
