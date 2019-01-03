using Common;
using QQBatchSend.IR.AuthTool.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QQBatchSend.IR.AuthTool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            tbRegCode.Text = ComputerInfo.GetComputerInfo();
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            RSACryption cryption = new RSACryption();
            tbAuthCode.Text = cryption.RSAEncrypt(RSACryption.publicKey, tbRegCode.Text);
        }
    }
}
