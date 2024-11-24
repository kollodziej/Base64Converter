using System.Runtime.InteropServices;
using System.Text;

namespace Base64Converter
{
    public partial class Form1 : Form
    {
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var content = textPlain.Text;
            var bytes = Encoding.UTF8.GetBytes(content);
            var base64 = Convert.ToBase64String(bytes);
            textBase64.Text = base64;
        }

        private void textPlain_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                switch (e.KeyData)
                {
                    case Keys.Control | Keys.V:
                        PasteText(textBox);
                        break;
                    case Keys.Control | Keys.C:
                        CopyText(textBox);
                        break;
                }
            }
        }

        private void PasteText(TextBox textBox)
        {
            textBox.Paste();
        }

        private void CopyText(TextBox textBox)
        {
            textBox.Copy();
        }

    }
}
