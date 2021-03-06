﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cube3Editor
{
    public partial class FrmRawView : Form
    {
        private bool isModified;
        private bool applyChanges;

        public FrmRawView(List<string> bfbStringList)
        {
            InitializeComponent();

            string seperator = "\r\n";
            rtbRawView.Text = string.Join(seperator, bfbStringList);

            Font defaultFont = new Font(FontFamily.GenericMonospace, 14.0F, FontStyle.Regular, GraphicsUnit.Pixel);
            rtbRawView.Font = defaultFont;

            applyChanges = false;
            isModified = false;

        }

        public bool IsModified { get => isModified; set => isModified = value; }
        public bool ApplyChanges { get => applyChanges; set => applyChanges = value; }

        private void Button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "BFB Files|*.bfb|All Files (*.*)|*.*";
            //saveFileDialog1.ShowDialog(); //Opens the Show File Dialog  
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Check if it's all ok  
            {
                string name = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + ".bfb"; //Just to make sure the extension is .txt  

                char[] seperator = { '\r', '\n' }; 
                string[] tbLines = rtbRawView.Text.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    using (var outFile = File.OpenWrite(name))
                    using (var bfbWriter = new StreamWriter(outFile))
                    {
                        foreach (string line in tbLines)
                        {
                            bfbWriter.WriteLine(line);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show($"Unable to write to file '{name}'");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //fontDialog1.ShowDialog(); //Shows the font dialog               //fontDialog1.ShowDialog(); //Shows the font dialog   
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rtbRawView.Font = fontDialog1.Font; //Sets the font to the one selected in the dialog  
            }
        }

        private void RtbRawView_TextChanged(object sender, EventArgs e)
        {
            IsModified = true;
            btnApply.Enabled = true;
            ApplyChanges = false;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            ApplyChanges = true;
            btnApply.Enabled = false;
        }
    }
}
