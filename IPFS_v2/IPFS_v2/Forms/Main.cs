using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sodium;

namespace IPFS_v2
{
    public partial class Main : Form
    {
        public string addr = "enkai.id.vn";
        public int port = 5000;
        public Main()
        {
            InitializeComponent();
            SharedConnection.establishConnection(addr, port);
        }

        public static UserControls.LoginOrRegister lgn;

        public Panel _panelContainer
        {
            get { return panelContainer; }
            set { panelContainer = value; }
        }
        public static string SHA256Encryption(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

        public static string SHA256CheckSum(string filePath)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = File.OpenRead(filePath))
                    return byteToHex(SHA256.ComputeHash(fileStream)).ToLower();
            }
        }

        public static string byteToHex(byte[] rawData)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < rawData.Length; i++)
            {
                builder.Append(rawData[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lgn = new UserControls.LoginOrRegister();
            _panelContainer.Controls.Add(lgn);
        }

    }

}
