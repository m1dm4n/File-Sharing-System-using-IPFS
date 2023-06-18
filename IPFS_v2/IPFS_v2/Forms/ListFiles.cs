using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IPFS_v2
{
    public partial class FormListFiles : Form
    {
        public string username, password, selectedFilePath;
        public FormListFiles(string user, string pass)
        {
            InitializeComponent();
            username = user;
            password = pass;
            handleUserList();
        }

        private void handleUserList()
        {
            try
            {
                //if (!File.Exists(string.Format("{0}listFiles.json",username)))
                //{
                //    File.Create(string.Format("{0}listFiles.json",username));
                //    //do
                //    //{

                //    //} while (true);
                //}
                string json = string.Format("{{\"action\": \"list_files\",\"data\": {{\"username\": \"{0}\", \"password\": \"{1}\"}}}}", username, password);
                SharedConnection.sendData(json);
                string result = SharedConnection.receiveData();
                //MessageBox.Show(result);
                JObject jsonObject = JObject.Parse(result);
                bool success = (bool)jsonObject["success"];
                JArray filesArray = (JArray)jsonObject["files"];

                // Add data to the ListView
                foreach (JObject fileObject in filesArray)
                {
                    string fileName = (string)fileObject["file_name"];
                    string fileHash = (string)fileObject["file_hash"];

                    // Create a ListViewItem with the file data
                    ListViewItem item = new ListViewItem(fileName);
                    item.SubItems.Add(fileHash);

                    // Add the ListViewItem to the ListView
                    listView1.Items.Add(item);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ListFiles Error");
            }
        }

        private void download_handler(string username, string password, string hashID)
        {
            try
            {
                string json = string.Format("{{\"action\": \"download\",\"data\": {{\"username\": \"{0}\", \"password\": \"{1}\",\"file_hash\":\"{2}\"}}}}", username, password, hashID);
                SharedConnection.sendData(json);
                string result = SharedConnection.receiveData();
                JObject jsonObject = JObject.Parse(result);
                bool success = (bool)jsonObject["success"];
                if (success)
                {
                    string fileContent = (string)jsonObject["file_content"];
                    var ofd = new SaveFileDialog();
                    ofd.FileName = hashID;
                    var res = ofd.ShowDialog();
                    if (res == DialogResult.OK)
                    {

                        var filePath = ofd.FileName;
                        
                        File.WriteAllBytes(filePath, System.Convert.FromBase64String(fileContent));
                    }
                }

                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }

        private void upload_handler(string username, string password, string hashID, string fileContent, string fileName)
        {
            try
            {
                string json = string.Format("{{\"action\": \"upload\",\"data\": {{\"username\": \"{0}\", \"password\": \"{1}\",\"file_hash\":\"{2}\",\"file_content\":\"{3}\",\"file_name\":\"{4}\"}}}}", username, password, hashID, fileContent, fileName);
                SharedConnection.sendData(json);
                string result = SharedConnection.receiveData();
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
            }
        }


        private void buttonDownload_Click(object sender, EventArgs e)
        {
            download_handler(username, password, textBoxDownloadHash.Text);
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                var res = ofd.ShowDialog();
                if (res == DialogResult.OK)
                {
                    selectedFilePath = ofd.FileName;
                    textBoxSelectedFile.Text = Path.GetFileName(selectedFilePath);
                    buttonUpload.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error");
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

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = listView1.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var focusedItem = listView1.FocusedItem;
            if (focusedItem != null)
            {
                download_handler(username, password, focusedItem.SubItems[1].Text);
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            string fileContent = System.Convert.ToBase64String(File.ReadAllBytes(selectedFilePath));
            string fileName = Path.GetFileName(selectedFilePath);
            string fileHash = Main.SHA256CheckSum(selectedFilePath);
            upload_handler(username, password, fileHash, fileContent, fileName);
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
