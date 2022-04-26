
namespace ChatClient
{
    partial class Registration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.registrationUserNameTextBox = new System.Windows.Forms.TextBox();
            this.registrationPasswordTextBox = new System.Windows.Forms.TextBox();
            this.repeatPasswordLabel = new System.Windows.Forms.Label();
            this.registrationRepeatTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.registrationIDTextBox = new System.Windows.Forms.TextBox();
            this.registrationButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Registration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "UserName";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // registrationUserNameTextBox
            // 
            this.registrationUserNameTextBox.Location = new System.Drawing.Point(148, 51);
            this.registrationUserNameTextBox.Name = "registrationUserNameTextBox";
            this.registrationUserNameTextBox.Size = new System.Drawing.Size(156, 20);
            this.registrationUserNameTextBox.TabIndex = 3;
            this.registrationUserNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.registrationUserNameTextBox_KeyDown);
            // 
            // registrationPasswordTextBox
            // 
            this.registrationPasswordTextBox.Location = new System.Drawing.Point(148, 89);
            this.registrationPasswordTextBox.Name = "registrationPasswordTextBox";
            this.registrationPasswordTextBox.PasswordChar = '*';
            this.registrationPasswordTextBox.Size = new System.Drawing.Size(156, 20);
            this.registrationPasswordTextBox.TabIndex = 4;
            this.registrationPasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.registrationPasswordTextBox_KeyDown);
            // 
            // repeatPasswordLabel
            // 
            this.repeatPasswordLabel.AutoSize = true;
            this.repeatPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repeatPasswordLabel.Location = new System.Drawing.Point(36, 126);
            this.repeatPasswordLabel.Name = "repeatPasswordLabel";
            this.repeatPasswordLabel.Size = new System.Drawing.Size(106, 13);
            this.repeatPasswordLabel.TabIndex = 5;
            this.repeatPasswordLabel.Text = "Repeat Password";
            // 
            // registrationRepeatTextBox
            // 
            this.registrationRepeatTextBox.Location = new System.Drawing.Point(148, 123);
            this.registrationRepeatTextBox.Name = "registrationRepeatTextBox";
            this.registrationRepeatTextBox.PasswordChar = '*';
            this.registrationRepeatTextBox.Size = new System.Drawing.Size(156, 20);
            this.registrationRepeatTextBox.TabIndex = 6;
            this.registrationRepeatTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.registrationRepeatTextBox_KeyDown);
            this.registrationRepeatTextBox.Leave += new System.EventHandler(this.registrationRepeatTextBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Personal ID";
            // 
            // registrationIDTextBox
            // 
            this.registrationIDTextBox.Location = new System.Drawing.Point(148, 161);
            this.registrationIDTextBox.Name = "registrationIDTextBox";
            this.registrationIDTextBox.Size = new System.Drawing.Size(156, 20);
            this.registrationIDTextBox.TabIndex = 8;
            this.registrationIDTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.registrationIDTextBox_KeyDown);
            // 
            // registrationButton
            // 
            this.registrationButton.Location = new System.Drawing.Point(229, 193);
            this.registrationButton.Name = "registrationButton";
            this.registrationButton.Size = new System.Drawing.Size(75, 23);
            this.registrationButton.TabIndex = 9;
            this.registrationButton.Text = "Registration";
            this.registrationButton.UseVisualStyleBackColor = true;
            this.registrationButton.Click += new System.EventHandler(this.registrationButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(39, 193);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Registration
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(337, 228);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.registrationButton);
            this.Controls.Add(this.registrationIDTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.registrationRepeatTextBox);
            this.Controls.Add(this.repeatPasswordLabel);
            this.Controls.Add(this.registrationPasswordTextBox);
            this.Controls.Add(this.registrationUserNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Registration";
            this.Text = "Registration";
            this.Load += new System.EventHandler(this.Registration_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Registration_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox registrationUserNameTextBox;
        private System.Windows.Forms.TextBox registrationPasswordTextBox;
        private System.Windows.Forms.Label repeatPasswordLabel;
        private System.Windows.Forms.TextBox registrationRepeatTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox registrationIDTextBox;
        private System.Windows.Forms.Button registrationButton;
        private System.Windows.Forms.Button cancelButton;
    }
}