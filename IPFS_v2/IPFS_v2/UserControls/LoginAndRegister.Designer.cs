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
            this.textBoxUsername.Location = new System.Drawing.Point(83, 57);
            this.textBoxUsername.Multiline = true;
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(341, 40);
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
            this.textBoxPassword.Location = new System.Drawing.Point(83, 130);
            this.textBoxPassword.Multiline = true;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(341, 40);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.TabStop = false;
            this.textBoxPassword.Text = "Password";
            this.textBoxPassword.Enter += new System.EventHandler(this.textBoxPassword_Enter);
            this.textBoxPassword.Leave += new System.EventHandler(this.textBoxPassword_Leave);
            // 
            // buttonLoginOrRegister
            // 
            this.buttonLoginOrRegister.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold);
            this.buttonLoginOrRegister.Location = new System.Drawing.Point(83, 207);
            this.buttonLoginOrRegister.Name = "buttonLoginOrRegister";
            this.buttonLoginOrRegister.Size = new System.Drawing.Size(341, 46);
            this.buttonLoginOrRegister.TabIndex = 3;
            this.buttonLoginOrRegister.TabStop = false;
            this.buttonLoginOrRegister.Text = "Login";
            this.buttonLoginOrRegister.UseVisualStyleBackColor = false;
            this.buttonLoginOrRegister.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelNoAccount
            // 
            this.labelNoAccount.AutoSize = true;
            this.labelNoAccount.Location = new System.Drawing.Point(153, 335);
            this.labelNoAccount.Name = "labelNoAccount";
            this.labelNoAccount.Size = new System.Drawing.Size(114, 16);
            this.labelNoAccount.TabIndex = 4;
            this.labelNoAccount.Text = "No account? Click";
            // 
            // labelClickRegister
            // 
            this.labelClickRegister.AutoSize = true;
            this.labelClickRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelClickRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClickRegister.Location = new System.Drawing.Point(280, 335);
            this.labelClickRegister.Name = "labelClickRegister";
            this.labelClickRegister.Size = new System.Drawing.Size(41, 16);
            this.labelClickRegister.TabIndex = 5;
            this.labelClickRegister.Text = "Here";
            this.labelClickRegister.Click += new System.EventHandler(this.labelClickRegister_Click);
            // 
            // LoginOrRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelClickRegister);
            this.Controls.Add(this.labelNoAccount);
            this.Controls.Add(this.buttonLoginOrRegister);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "LoginOrRegister";
            this.Size = new System.Drawing.Size(520, 370);
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
