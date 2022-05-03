using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();
            for(int i = 2;i<=72;i += 2)
            {
                TextSize.Items.Add(i);
            }
            using(InstalledFontCollection founts = new InstalledFontCollection())
            {
                foreach(FontFamily fontFamily in founts.Families)
                {
                    textFont.Items.Add(fontFamily.Name);
                }
            }
            textFont.SelectedItem = MainConfig.GetValue(MainConfig.ConfigType.TEXTFONT);
            TextSize.SelectedItem = int.Parse(MainConfig.GetValue(MainConfig.ConfigType.TEXTSIZE));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainConfig.SetValue(MainConfig.ConfigType.TEXTFONT, textFont.SelectedItem.ToString());
            MainConfig.SetValue(MainConfig.ConfigType.TEXTSIZE, TextSize.SelectedItem.ToString());

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
