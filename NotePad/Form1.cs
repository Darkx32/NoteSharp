using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Form1 : Form
    {
        String File, filepath;
        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var n = new OpenFileDialog() { CheckFileExists = true, Multiselect = false };
            if (n.ShowDialog() == DialogResult.OK)
            {
                filepath = n.FileName;
                Console.WriteLine();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
