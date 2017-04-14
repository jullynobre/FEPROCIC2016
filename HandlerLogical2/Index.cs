using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HandlerLogical2
{
    public partial class LinearFunction : Form
    {
        public LinearFunction()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            x.Text = Program.start(function.Text);
        }

        private void LinearFunction_Load(object sender, EventArgs e)
        {

        }

        private void function_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            function.Text = "f(x) = x";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            function.Text = "f(x) = x²";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            function.Text = "ax² + b - c = 0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            function.Text = "x + 1 = 2x - 1";
        }
    }
}
