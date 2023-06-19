namespace IPFS_v2.UserControls
{
    partial class LoginOrRegister
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonLoginOrRegister = new System.Windows.Forms.Button();
            this.labelNoAccount = new System.Windows.Forms.Label();
            this.labelClickRegister = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsername.ForeColor = System.Drawing.Color.Gray;
            this.textBoxUsername.Location = new System.Drawing.Point(62, 46);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxUsername.Multiline = true;
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(257, 33);
            this.textBoxUsername.TabIndex = 1;
            this.textBoxUsername.TabStop = false;
            this.textBoxUsername.Text = "Username";
            this.textBoxUsername.Enter += new System.EventHandler(this.textBoxUsername_Enter);
            this.textBoxUsername.Leave += new System.EventHandler(this.textBoxUsername_Leave);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.ForeColor = System.Drawing.Color.Gray;
            this.textBoxPassword.Location = new System.Drawing.Point(62, 106);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPassword.Multiline = true;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(257, 33);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.TabStop = false;
            this.textBoxPassword.Text = "Password";
            this.textBoxPassword.Enter += new System.EventHandler(this.textBoxPassword_Enter);
            this.textBoxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPassword_KeyPress);
            this.textBoxPassword.Leave += new System.EventHandler(this.textBoxPassword_Leave);
            // 
            // buttonLoginOrRegister
            // 
            this.buttonLoginOrRegister.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold);
            this.buttonLoginOrRegister.Location = new System.Drawing.Point(62, 168);
            this.buttonLoginOrRegister.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLoginOrRegister.Name = "buttonLoginOrRegister";
            this.buttonLoginOrRegister.Size = new System.Drawing.Size(256, 37);
            this.buttonLoginOrRegister.TabIndex = 3;
            this.buttonLoginOrRegister.TabStop = false;
            this.buttonLoginOrRegister.Text = "Login";
            this.buttonLoginOrRegister.UseVisualStyleBackColor = false;
            this.buttonLoginOrRegister.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelNoAccount
            // 
            this.labelNoAccount.AutoSize = true;
            this.labelNoAccount.Location = new System.Drawing.Point(115, 272);
            this.labelNoAccount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNoAccount.Name = "labelNoAccount";
            this.labelNoAccount.Size = new System.Drawing.Size(95, 13);
            this.labelNoAccount.TabIndex = 4;
            this.labelNoAccount.Text = "No account? Click";
            // 
            // labelClickRegister
            // 
            this.labelClickRegister.AutoSize = true;
            this.labelClickRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelClickRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClickRegister.Location = new System.Drawing.Point(210, 272);
            this.labelClickRegister.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelClickRegister.Name = "labelClickRegister";
            this.labelClickRegister.Size = new System.Drawing.Size(34, 13);
            this.labelClickRegister.TabIndex = 5;
            this.labelClickRegister.Text = "Here";
            this.labelClickRegister.Click += new System.EventHandler(this.labelClickRegister_Click);
            // 
            // LoginOrRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Controls.Add(this.labelClickRegister);
            this.Controls.Add(this.labelNoAccount);
            this.Controls.Add(this.buttonLoginOrRegister);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LoginOrRegister";
            this.Size = new System.Drawing.Size(390, 301);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPassword_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLoginOrRegister;
        private System.Windows.Forms.Label labelNoAccount;
        private System.Windows.Forms.Label labelClickRegister;
    }
}
