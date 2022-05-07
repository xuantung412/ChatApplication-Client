
namespace ChatClient
{
    partial class Welcome
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
            this.label1 = new System.Windows.Forms.Label();
            this.ChatApp = new System.Windows.Forms.Button();
            this.p2pMaster = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.p2pSlave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(44, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Select Service To Run";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChatApp
            // 
            this.ChatApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChatApp.FlatAppearance.BorderSize = 5;
            this.ChatApp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChatApp.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ChatApp.Location = new System.Drawing.Point(18, 19);
            this.ChatApp.Name = "ChatApp";
            this.ChatApp.Size = new System.Drawing.Size(110, 23);
            this.ChatApp.TabIndex = 1;
            this.ChatApp.Text = "Client";
            this.ChatApp.UseVisualStyleBackColor = true;
            this.ChatApp.Click += new System.EventHandler(this.ChatApp_Click);
            // 
            // p2pMaster
            // 
            this.p2pMaster.Cursor = System.Windows.Forms.Cursors.Hand;
            this.p2pMaster.FlatAppearance.BorderSize = 5;
            this.p2pMaster.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.p2pMaster.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.p2pMaster.Location = new System.Drawing.Point(21, 19);
            this.p2pMaster.Name = "p2pMaster";
            this.p2pMaster.Size = new System.Drawing.Size(110, 23);
            this.p2pMaster.TabIndex = 2;
            this.p2pMaster.Text = "Master";
            this.p2pMaster.UseVisualStyleBackColor = true;
            this.p2pMaster.Click += new System.EventHandler(this.p2pMaster_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.p2pSlave);
            this.groupBox1.Controls.Add(this.p2pMaster);
            this.groupBox1.Location = new System.Drawing.Point(3, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Peer-To-Peer";
            // 
            // p2pSlave
            // 
            this.p2pSlave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.p2pSlave.FlatAppearance.BorderSize = 5;
            this.p2pSlave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.p2pSlave.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.p2pSlave.Location = new System.Drawing.Point(21, 48);
            this.p2pSlave.Name = "p2pSlave";
            this.p2pSlave.Size = new System.Drawing.Size(110, 23);
            this.p2pSlave.TabIndex = 3;
            this.p2pSlave.Text = "Slave";
            this.p2pSlave.UseVisualStyleBackColor = true;
            this.p2pSlave.Click += new System.EventHandler(this.p2pSlave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChatApp);
            this.groupBox2.Location = new System.Drawing.Point(162, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client-Server Chat App";
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 178);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "Welcome";
            this.Text = "Welcome";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ChatApp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button p2pMaster;
        private System.Windows.Forms.Button p2pSlave;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}