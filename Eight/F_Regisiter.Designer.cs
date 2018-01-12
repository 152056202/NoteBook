namespace NoteBook
{
    partial class F_Regisiter
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
            this.regisiter = new System.Windows.Forms.Button();
            this.regisiter_back = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.regisiter_id = new System.Windows.Forms.TextBox();
            this.regisiter_password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // regisiter
            // 
            this.regisiter.Location = new System.Drawing.Point(46, 212);
            this.regisiter.Name = "regisiter";
            this.regisiter.Size = new System.Drawing.Size(75, 23);
            this.regisiter.TabIndex = 0;
            this.regisiter.Text = "注册";
            this.regisiter.UseVisualStyleBackColor = true;
            this.regisiter.Click += new System.EventHandler(this.regisiter_Click);
            // 
            // regisiter_back
            // 
            this.regisiter_back.Location = new System.Drawing.Point(170, 212);
            this.regisiter_back.Name = "regisiter_back";
            this.regisiter_back.Size = new System.Drawing.Size(75, 23);
            this.regisiter_back.TabIndex = 1;
            this.regisiter_back.Text = "返回";
            this.regisiter_back.UseVisualStyleBackColor = true;
            this.regisiter_back.Click += new System.EventHandler(this.regisiter_back_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "账号";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密码";
            // 
            // regisiter_id
            // 
            this.regisiter_id.Location = new System.Drawing.Point(129, 77);
            this.regisiter_id.Name = "regisiter_id";
            this.regisiter_id.Size = new System.Drawing.Size(100, 21);
            this.regisiter_id.TabIndex = 4;
            // 
            // regisiter_password
            // 
            this.regisiter_password.Location = new System.Drawing.Point(129, 127);
            this.regisiter_password.Name = "regisiter_password";
            this.regisiter_password.Size = new System.Drawing.Size(100, 21);
            this.regisiter_password.TabIndex = 5;
            this.regisiter_password.TextChanged += new System.EventHandler(this.regisiter_password_TextChanged);
            // 
            // F_Regisiter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.regisiter_password);
            this.Controls.Add(this.regisiter_id);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.regisiter_back);
            this.Controls.Add(this.regisiter);
            this.Name = "F_Regisiter";
            this.Text = "记事本——注册界面";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button regisiter;
        private System.Windows.Forms.Button regisiter_back;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox regisiter_id;
        private System.Windows.Forms.TextBox regisiter_password;
    }
}