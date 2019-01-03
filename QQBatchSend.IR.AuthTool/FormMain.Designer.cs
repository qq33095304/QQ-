namespace QQBatchSend.IR.AuthTool
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAuth = new System.Windows.Forms.Button();
            this.tbAuthCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbRegCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAuth);
            this.panel2.Controls.Add(this.tbAuthCode);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.tbRegCode);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(751, 221);
            this.panel2.TabIndex = 1;
            // 
            // btnAuth
            // 
            this.btnAuth.Location = new System.Drawing.Point(649, 180);
            this.btnAuth.Name = "btnAuth";
            this.btnAuth.Size = new System.Drawing.Size(75, 25);
            this.btnAuth.TabIndex = 4;
            this.btnAuth.Text = "授权";
            this.btnAuth.UseVisualStyleBackColor = true;
            this.btnAuth.Click += new System.EventHandler(this.btnAuth_Click);
            // 
            // tbAuthCode
            // 
            this.tbAuthCode.Location = new System.Drawing.Point(111, 65);
            this.tbAuthCode.Multiline = true;
            this.tbAuthCode.Name = "tbAuthCode";
            this.tbAuthCode.Size = new System.Drawing.Size(613, 109);
            this.tbAuthCode.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "授权码：";
            // 
            // tbRegCode
            // 
            this.tbRegCode.Location = new System.Drawing.Point(111, 18);
            this.tbRegCode.Name = "tbRegCode";
            this.tbRegCode.Size = new System.Drawing.Size(613, 25);
            this.tbRegCode.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "申请码：";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 220);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "群发授权工具";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAuth;
        private System.Windows.Forms.TextBox tbAuthCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbRegCode;
        private System.Windows.Forms.Label label7;
    }
}

