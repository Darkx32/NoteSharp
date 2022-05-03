using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Form1 : Form
    {
        string filename, filepath;
        string defaultFile = "Unknown";
        bool isSaved = true;
        MainConfig mainConfig;
        public Form1()
        {
            InitializeComponent();
            filename = defaultFile;
            Text = "NoteSharp" + " - " + defaultFile;
            filepath = string.Empty;

            mainConfig = new MainConfig();

            Input.Font = new System.Drawing.Font(MainConfig.GetValue(MainConfig.ConfigType.TEXTFONT), 
                float.Parse(MainConfig.GetValue(MainConfig.ConfigType.TEXTSIZE)));
        }

        private void Input_TextChanged(object sender, EventArgs e)
        {
            if (isSaved)
            {
                Text += "*";
            }
            isSaved = false;
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SAVE();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();
            var n = new OpenFileDialog() { Multiselect = false, Filter = "All Files (*.*)|*.*"};
            if (n.ShowDialog() == DialogResult.OK)
            {
                filepath = n.FileName;

                if (!isSaved) { Text = Text.Remove(Text.Length - 1); }
                Text = Text.Replace(filename, "");
                filename = filepath.Split('\\')[filepath.Split('\\').Length - 1];
                Text += filename;

                isSaved = true;

                using (StreamReader reader = new StreamReader(n.OpenFile()))
                {
                    Input.Text = reader.ReadToEnd();
                }
            }
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();
            var n = new SaveFileDialog();
            if (n.ShowDialog() == DialogResult.OK)
            {
                filepath = n.FileName;

                if (!isSaved) { Text = Text.Remove(Text.Length - 1); }
                Text = Text.Replace(filename, "");
                filename = filepath.Split('\\')[filepath.Split('\\').Length - 1];
                Text += filename;


                File.WriteAllText(filepath, Input.Text);
                isSaved = true;
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SAVE();

            if (!isSaved) { Text = Text.Remove(Text.Length - 1); }
            Input.Text = string.Empty;
            Text = Text.Replace(filename, "");
            filename = defaultFile;
            Text += filename;

            isSaved = true;
        }

        private void SAVE()
        {
            if (!isSaved && filename == defaultFile && filepath == string.Empty)
            {
                var c = MessageBox.Show("Deseja salvar o arquivo?", "Salvar", MessageBoxButtons.YesNo);
                if (c == DialogResult.Yes)
                {
                    Text = Text.Remove(Text.Length - 1);
                    if (filepath == string.Empty)
                    {
                        var n = new SaveFileDialog();
                        if (n.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(n.FileName, Input.Text);

                            filepath = n.FileName;
                            Text = Text.Replace(filename, "");
                            filename = filepath.Split('\\')[filepath.Split('\\').Length - 1];
                            Text += filename;

                            isSaved = true;
                        }
                    }
                    else
                    {
                        File.WriteAllText(filepath, Input.Text);
                        isSaved = true;
                    }
                }
            } else if (!isSaved)
            {
                Text = Text.Remove(Text.Length - 1);
                File.WriteAllText(filepath, Input.Text);
                isSaved = true;
            }
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Cut();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Copy();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Paste();
        }

        private void deletarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.SelectedText = "";
        }

        private void desfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Undo();
        }

        private void sobreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Sobre().ShowDialog();
        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Configuration().ShowDialog();
            Input.Font = new System.Drawing.Font(MainConfig.GetValue(MainConfig.ConfigType.TEXTFONT),
                float.Parse(MainConfig.GetValue(MainConfig.ConfigType.TEXTSIZE)));
        }

        private void refazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Redo();
        }
    }
}
