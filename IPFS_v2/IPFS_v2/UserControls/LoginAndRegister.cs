using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sodium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPFS_v2.UserControls
{
    public partial class LoginOrRegister : UserControl
    {
        public LoginOrRegister()
        {
            InitializeComponent();
            textBoxPassword.Text = "password123";
            textBoxUsername.Text = "john123";
            textBoxPassword.ForeColor = Color.Black;
            textBoxUsername.ForeColor = Color.Black;
        }

        private void textBoxUsername_Enter(object sender, EventArgs e)
        {
            if (textBoxUsername.ForeColor == Color.Gray)
            {
                textBoxUsername.Text = string.Empty;
                textBoxUsername.ForeColor = Color.Black;
            }
        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxUsername.Text))
            {
                textBoxUsername.Text = "Username";
                textBoxUsername.ForeColor = Color.Gray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.ForeColor == Color.Gray)
            {
                textBoxPassword.Text = string.Empty;
                textBoxPassword.PasswordChar = '*';
                textBoxPassword.ForeColor = Color.Black;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                textBoxPassword.Text = "Password";
                textBoxPassword.ForeColor = Color.Gray;
                textBoxPassword.PasswordChar = '\0';
            }
        }

        private bool validateLogin(string username, string password)
        {
            string json = string.Format("{{\"action\": \"login\",\"data\": {{\"username\": \"{0}\", \"password\": \"{1}\"}}}}!endf!", username, password);
            SharedConnection.sendData(json);
            string result = SharedConnection.receiveData();
            JObject jsonObject = JObject.Parse(result);
            return (bool)jsonObject["success"];
        }

        private bool checkAccount(string username, string password, string privateKey, string publicKey)
        {
            string json = string.Format("{{\"action\": \"register\",\"data\": {{\"username\": \"{0}\", \"password\": \"{1}\",\"public_key\":\"{2}\",\"encrypted_private_key\":\"{3}\"}}}}!endf!", username,password,publicKey,privateKey);
            SharedConnection.sendData(json);
            string result = SharedConnection.receiveData();
            JObject jsonObject = JObject.Parse(result);
            return (bool)jsonObject["success"];
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {

                if (buttonLoginOrRegister.Text == "Login") //Login
                {
                        if (textBoxUsername.ForeColor != Color.Gray && textBoxPassword.ForeColor != Color.Gray)
                        {
                            bool validate = validateLogin(textBoxUsername.Text, textBoxPassword.Text);
                            if (validate)
                            {
                                FormListFiles formListFiles = new FormListFiles(textBoxUsername.Text, textBoxPassword.Text);
                                Form someForm = (Form)this.Parent.Parent;
                                someForm.Hide();
                                formListFiles.ShowDialog();
                                someForm.Close();
                            }
                            else
                            {
                                //MessageBox.Show("Failed to Login");
                                throw (new Exception("Failed to Login"));
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Failed to Login");
                            throw (new Exception("Fill all boxes before login"));
                        }
                
                }
                else if (buttonLoginOrRegister.Text == "Register") //Register
                {
                    if (textBoxUsername.ForeColor != Color.Gray && textBoxPassword.ForeColor != Color.Gray)
                    {
                        var AESkey = Main.StringToByteArray(Main.SHA256Encryption(textBoxUsername.Text + textBoxPassword.Text));
                        var keyPair = PublicKeyAuth.GenerateKeyPair();
                        byte[] publicKey = keyPair.PublicKey;
                        byte[] privateKey = keyPair.PrivateKey;
                        //MessageBox.Show(Main.byteToHex(publicKey) + "\n" + Main.byteToHex(privateKey)); 
                        //AesManaged aes = new AesManaged();
                        //aes.Mode = CipherMode.ECB;
                        //aes.KeySize = 256;

                        ////byte[] encryptedPrivateKey = aes.CreateEncryptor(AESkey);

                        if (checkAccount(textBoxUsername.Text, textBoxPassword.Text, Main.byteToHex(privateKey), Main.byteToHex(publicKey)))
                        {
                            MessageBox.Show("Register success! Returning to Login.", "Success");
                            labelClickRegister_Click(sender, e);
                        }
                        else
                        {
                            throw (new Exception("Account already exists"));
                        }
                    }
                    else
                    {
                        throw (new Exception("Fill all boxes before login"));
                    }
                }
                else
                {
                    throw (new Exception("Something went wrong"));
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }

        private void labelClickRegister_Click(object sender, EventArgs e)
        {
            if(buttonLoginOrRegister.Text == "Login")
            {
                buttonLoginOrRegister.Text = "Register";
                labelNoAccount.Text = "Yes account? Click";
            }
            else
            {
                buttonLoginOrRegister.Text = "Login";
                labelNoAccount.Text = "No account? Click";
            }

        }
    }
}
