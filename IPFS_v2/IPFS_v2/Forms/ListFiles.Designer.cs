using System;

namespace IPFS_v2
{
    partial class FormListFiles
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListFiles));
            this.buttonChooseFile = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.textBoxDownloadHash = new System.Windows.Forms.TextBox();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbStatus = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxSelectedFile = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelListFiles = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHashID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChooseFile
            // 
            resources.ApplyResources(this.buttonChooseFile, "buttonChooseFile");
            this.buttonChooseFile.Name = "buttonChooseFile";
            this.buttonChooseFile.UseVisualStyleBackColor = true;
            this.buttonChooseFile.Click += new System.EventHandler(this.buttonChooseFile_Click);
            // 
            // buttonUpload
            // 
            resources.ApplyResources(this.buttonUpload, "buttonUpload");
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // textBoxDownloadHash
            // 
            resources.ApplyResources(this.textBoxDownloadHash, "textBoxDownloadHash");
            this.textBoxDownloadHash.ForeColor = System.Drawing.Color.Gray;
            this.textBoxDownloadHash.Name = "textBoxDownloadHash";
            this.textBoxDownloadHash.TabStop = false;
            this.textBoxDownloadHash.Enter += new System.EventHandler(this.textBoxDownloadHash_Enter);
            this.textBoxDownloadHash.Leave += new System.EventHandler(this.textBoxDownloadHash_Leave);
            // 
            // buttonDownload
            // 
            resources.ApplyResources(this.buttonDownload, "buttonDownload");
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tableLayoutPanel1.Controls.Add(this.textBoxDownloadHash, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonDownload, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbStatus, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbStatus.Name = "lbStatus";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tableLayoutPanel2.Controls.Add(this.textBoxSelectedFile, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonChooseFile, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonUpload, 2, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // textBoxSelectedFile
            // 
            resources.ApplyResources(this.textBoxSelectedFile, "textBoxSelectedFile");
            this.textBoxSelectedFile.Name = "textBoxSelectedFile";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tableLayoutPanel3.Controls.Add(this.listView1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelListFiles, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // labelListFiles
            // 
            resources.ApplyResources(this.labelListFiles, "labelListFiles");
            this.labelListFiles.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelListFiles.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelListFiles.Name = "labelListFiles";
            this.labelListFiles.Click += new System.EventHandler(this.labelListFiles_Click);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // listView1
            // 
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.BackgroundImage = global::IPFS_v2.Properties.Resources.cheems_lo_;
            this.listView1.BackgroundImageTiled = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderHashID});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderHashID
            // 
            resources.ApplyResources(this.columnHeaderHashID, "columnHeaderHashID");
            // 
            // FormListFiles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormListFiles";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonChooseFile;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.TextBox textBoxDownloadHash;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelListFiles;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderHashID;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox textBoxSelectedFile;
    }
}

