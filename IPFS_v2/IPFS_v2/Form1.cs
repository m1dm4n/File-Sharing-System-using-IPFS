using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPFS_v2
{
    public partial class FormListFiles : Form
    {
        public FormListFiles()
        {
            InitializeComponent();
        }
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog();
            var res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                var selectedFile = ofd.FileName;
                textBoxSelectedFile.Text = Path.GetFileName(selectedFile);
            }
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            var res = ofd.ShowDialog();
            if (res == DialogResult.OK)
            {
                var selectedFile = ofd.FileName;
                textBoxSelectedFile.Text = Path.GetFileName(selectedFile);
                buttonUpload.Enabled = true;
            }
        }

        private void textBoxDownloadHash_Enter(object sender, EventArgs e)
        {
            if (textBoxDownloadHash.ForeColor == Color.Gray)
            {
                textBoxDownloadHash.Text = string.Empty;
                textBoxDownloadHash.ForeColor = Color.Black;
            }
        }

        private void textBoxDownloadHash_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDownloadHash.Text))
            {
                textBoxDownloadHash.Text = "Enter file's hash to download.";
                textBoxDownloadHash.ForeColor = Color.Gray;
            }
        }
    }


}
