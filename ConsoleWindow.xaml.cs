using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrowserWpf
{
    public partial class ConsoleWindow : Window
    {
        private StringBuilder consoleOutput = new StringBuilder();

        public ConsoleWindow()
        {
            InitializeComponent();

            // Redirect Console.Out to the TextBox
            Console.SetOut(new TextBoxWriter(ConsoleTextBox, consoleOutput));
        }

        // Other methods and event handlers for your console window
    }

    // Custom TextWriter class to redirect Console output to a TextBox
    public class TextBoxWriter : TextWriter
    {
        private TextBox textBox;
        private StringBuilder outputBuffer;

        public TextBoxWriter(TextBox textBox, StringBuilder outputBuffer)
        {
            this.textBox = textBox;
            this.outputBuffer = outputBuffer;
        }

        public override void Write(char value)
        {
            // Update the TextBox and the output buffer
            Application.Current.Dispatcher.Invoke(() =>
            {
                textBox.AppendText(value.ToString());
            });
            outputBuffer.Append(value);
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
