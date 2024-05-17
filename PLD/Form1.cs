using System;
using System.Windows.Forms;
using com.calitha.goldparser;

namespace PLD
{
    public partial class Form1 : Form
    {
        MyParser parser;

        public Form1()
        {
            InitializeComponent();
            parser = new MyParser("x_project.cgt", listBox1);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            parser.Parse(richTextBox1.Text);
        }
    }
}
