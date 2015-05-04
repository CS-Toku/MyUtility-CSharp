using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyUtility
{
    public partial class NonFrameMBox : Form
    {
        private const int margin = 8;

        private DialogResult dr1;
        private DialogResult dr2;
        private DialogResult dr3;
        private MessageBoxButtons btn_type;

        private NonFrameMBox()
        {
            InitializeComponent();
        }


        public static DialogResult Show(String msg)
        {
            return Show(null, msg, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult Show(IWin32Window owner, String msg)
        {
            return Show(owner, msg, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(String msg, String title)
        {
            return Show(null, msg, title, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult Show(IWin32Window owner, String msg, String title)
        {
            return Show(owner, msg, title, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(String msg, String title, MessageBoxButtons btn_type)
        {
            return Show(null, msg, title, btn_type, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult Show(IWin32Window owner, String msg, String title, MessageBoxButtons btn_type)
        {
            return Show(owner, msg, title, btn_type, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(String msg, String title, MessageBoxButtons btn_type, MessageBoxIcon icon)
        {
            return Show(null, msg, title, btn_type, icon, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult Show(IWin32Window owner, String msg, String title, MessageBoxButtons btn_type, MessageBoxIcon icon)
        {
            return Show(owner, msg, title, btn_type, icon, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(String msg, String title, MessageBoxButtons btn_type, MessageBoxIcon icon, MessageBoxDefaultButton btn_default)
        {
            return Show(null, msg, title, btn_type, icon, btn_default);
        }
        public static DialogResult Show(IWin32Window owner, String msg, String title, MessageBoxButtons btn_type, MessageBoxIcon icon, MessageBoxDefaultButton btn_default)
        {

            NonFrameMBox mbox = new NonFrameMBox();
            String buf;
            int Height, Width;
            mbox.Owner = (Form)owner;
            mbox.StartPosition = FormStartPosition.CenterScreen;
            mbox.Msg.Text = msg;
            mbox.btn_type = btn_type;
            mbox.Text = title;
            Image img = null;
            switch (icon)
            {
                case MessageBoxIcon.Asterisk:
                    img = SystemIcons.Asterisk.ToBitmap();
                    break;
                case MessageBoxIcon.Error:
                    img = SystemIcons.Error.ToBitmap();
                    break;
                case MessageBoxIcon.Exclamation:
                    img = SystemIcons.Exclamation.ToBitmap();
                    break;
                case MessageBoxIcon.Question:
                    img = SystemIcons.Question.ToBitmap();
                    break;
                case MessageBoxIcon.None:
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            if (img != null)
            {
                mbox.Icon.Image = img;
                mbox.Icon.Height = img.Height;
                mbox.Icon.Width = img.Width;
            }
            else
                mbox.Icon.Visible = false;
                

            if (img != null)
            {
                mbox.Msg.Left = mbox.Icon.Left + mbox.Icon.Height + margin;
            }
            else
            {
                mbox.Msg.Left = margin*2;
            }
            //mbox.Msg.Text = mbox.Msg.Width.ToString();
            if (mbox.Msg.Height > 12 || mbox.Msg.Width > 351)
            {
                mbox.Msg.Top = mbox.Icon.Top;
                mbox.Msg.Text = "";
                foreach (char c in msg)
                {
                    buf = mbox.Msg.Text;
                    mbox.Msg.Text = buf + c;
                    Console.WriteLine(mbox.Msg.Width);
                    if (mbox.Msg.Width > 351)
                        mbox.Msg.Text = buf + '\n' + c;
                }

                Height = System.Windows.Forms.Screen.GetWorkingArea(mbox).Height*3/4 - (mbox.splitContainer1.Panel2.Height + 1 + mbox.Msg.Top + margin * 3);
                if(Height < mbox.Msg.Height)
                {
                    mbox.Msg.AutoSize = false;
                    mbox.Msg.Width = 351;
                    mbox.Msg.Height = Height;
                }


            }
            else
            {
                mbox.Msg.Top = (mbox.splitContainer1.Panel1.Height - mbox.Msg.Height) / 2;
            }

            switch (btn_type)
            {
                case MessageBoxButtons.OK:
                    Width = (mbox.button1.Width + margin)*1 + margin*5;
                    break;
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.RetryCancel:
                case MessageBoxButtons.YesNo:
                    Width = (mbox.button1.Width + margin) * 2 + margin * 5;
                    break;
                case MessageBoxButtons.YesNoCancel:
                case MessageBoxButtons.AbortRetryIgnore:
                    Width = (mbox.button1.Width + margin) * 3 + margin * 5;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            if (Width > mbox.Msg.Left + mbox.Msg.Width + margin * 3)
                mbox.Width = Width;
            else
                mbox.Width = mbox.Msg.Left + mbox.Msg.Width + margin * 3;

            if (mbox.Icon.Top * 2 + mbox.Icon.Height < mbox.Msg.Top + mbox.Msg.Height + margin * 3)
                mbox.Height = mbox.Msg.Top + mbox.Msg.Height + margin * 3 + mbox.splitContainer1.SplitterWidth + mbox.splitContainer1.Panel2.Height;
            else
                mbox.Height = mbox.Icon.Top * 2 + mbox.Icon.Height + mbox.splitContainer1.SplitterWidth + mbox.splitContainer1.Panel2.Height;

            
            switch (btn_type)
            {
                case MessageBoxButtons.OK:
                    mbox.button1.Text = "OK";
                    mbox.button1.Visible = true;
                    mbox.button2.Visible = false;
                    mbox.button3.Visible = false;
                    mbox.button1.Left = mbox.splitContainer1.Panel2.Width - mbox.button1.Width - margin;
                    mbox.button1.Top = (mbox.splitContainer1.Panel2.Height - mbox.button1.Height) / 2;
                    mbox.dr1 = DialogResult.OK;
                    mbox.dr2 = DialogResult.None;
                    mbox.dr3 = DialogResult.None;
                    break;
                case MessageBoxButtons.OKCancel:
                    mbox.button1.Text = "OK";
                    mbox.button2.Text = "キャンセル";
                    mbox.button1.Visible = true;
                    mbox.button2.Visible = true;
                    mbox.button3.Visible = false;
                    mbox.button1.Left = mbox.splitContainer1.Panel2.Width - mbox.button1.Width - mbox.button2.Width - margin*2;
                    mbox.button1.Top = (mbox.splitContainer1.Panel2.Height - mbox.button1.Height) / 2;
                    mbox.button2.Left = mbox.splitContainer1.Panel2.Width - mbox.button2.Width - margin;
                    mbox.button2.Top = (mbox.splitContainer1.Panel2.Height - mbox.button2.Height) / 2;
                    mbox.dr1 = DialogResult.OK;
                    mbox.dr2 = DialogResult.Cancel;
                    mbox.dr3 = DialogResult.None;
                    break;
                case MessageBoxButtons.RetryCancel:
                    mbox.button1.Text = "再試行(R)";
                    mbox.button2.Text = "キャンセル";
                    mbox.button1.Visible = true;
                    mbox.button2.Visible = true;
                    mbox.button3.Visible = false;
                    mbox.button1.Left = mbox.splitContainer1.Panel2.Width - mbox.button1.Width - mbox.button2.Width - margin*2;
                    mbox.button1.Top = (mbox.splitContainer1.Panel2.Height - mbox.button1.Height) / 2;
                    mbox.button2.Left = mbox.splitContainer1.Panel2.Width - mbox.button2.Width - margin;
                    mbox.button2.Top = (mbox.splitContainer1.Panel2.Height - mbox.button2.Height) / 2;
                    mbox.dr1 = DialogResult.Retry;
                    mbox.dr2 = DialogResult.Cancel;
                    mbox.dr3 = DialogResult.None;
                    break;
                case MessageBoxButtons.YesNo:
                    mbox.button1.Text = "はい(Y)";
                    mbox.button2.Text = "いいえ(N)";
                    mbox.button1.Visible = true;
                    mbox.button2.Visible = true;
                    mbox.button3.Visible = false;
                    mbox.button1.Left = mbox.splitContainer1.Panel2.Width - mbox.button1.Width - mbox.button2.Width - margin*2;
                    mbox.button1.Top = (mbox.splitContainer1.Panel2.Height - mbox.button1.Height) / 2;
                    mbox.button2.Left = mbox.splitContainer1.Panel2.Width - mbox.button2.Width - margin;
                    mbox.button2.Top = (mbox.splitContainer1.Panel2.Height - mbox.button2.Height) / 2;
                    mbox.dr1 = DialogResult.Yes;
                    mbox.dr2 = DialogResult.No;
                    mbox.dr3 = DialogResult.None;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    mbox.button1.Text = "はい(Y)";
                    mbox.button2.Text = "いいえ(N)";
                    mbox.button3.Text = "キャンセル";
                    mbox.button1.Visible = true;
                    mbox.button2.Visible = true;
                    mbox.button3.Visible = true;
                    mbox.button1.Left = mbox.splitContainer1.Panel2.Width - mbox.button1.Width - mbox.button2.Width - mbox.button3.Width - margin * 3;
                    mbox.button1.Top = (mbox.splitContainer1.Panel2.Height - mbox.button1.Height) / 2;
                    mbox.button2.Left = mbox.splitContainer1.Panel2.Width - mbox.button2.Width - mbox.button3.Width - margin * 2;
                    mbox.button2.Top = (mbox.splitContainer1.Panel2.Height - mbox.button2.Height) / 2;
                    mbox.button3.Left = mbox.splitContainer1.Panel2.Width - mbox.button3.Width - margin;
                    mbox.button3.Top = (mbox.splitContainer1.Panel2.Height - mbox.button3.Height) / 2;
                    mbox.dr1 = DialogResult.Yes;
                    mbox.dr2 = DialogResult.No;
                    mbox.dr3 = DialogResult.Cancel;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    mbox.button1.Text = "中止(A)";
                    mbox.button2.Text = "再試行(R)";
                    mbox.button3.Text = "無視(I)";
                    mbox.button1.Visible = true;
                    mbox.button2.Visible = true;
                    mbox.button3.Visible = true;
                    mbox.button1.Left = mbox.splitContainer1.Panel2.Width - mbox.button1.Width - mbox.button2.Width - mbox.button3.Width - margin * 3;
                    mbox.button1.Top = (mbox.splitContainer1.Panel2.Height - mbox.button1.Height) / 2;
                    mbox.button2.Left = mbox.splitContainer1.Panel2.Width - mbox.button2.Width - mbox.button3.Width - margin * 2;
                    mbox.button2.Top = (mbox.splitContainer1.Panel2.Height - mbox.button2.Height) / 2;
                    mbox.button3.Left = mbox.splitContainer1.Panel2.Width - mbox.button3.Width - margin;
                    mbox.button3.Top = (mbox.splitContainer1.Panel2.Height - mbox.button3.Height) / 2;
                    mbox.dr1 = DialogResult.Abort;
                    mbox.dr2 = DialogResult.Retry;
                    mbox.dr3 = DialogResult.Ignore;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            switch (btn_default)
            {
                case MessageBoxDefaultButton.Button1:
                    mbox.AcceptButton = mbox.button1;
                    break;
                case MessageBoxDefaultButton.Button2:
                    mbox.AcceptButton = mbox.button2;
                    break;
                case MessageBoxDefaultButton.Button3:
                    mbox.AcceptButton = mbox.button3;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            return mbox.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = this.dr1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = this.dr2;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = this.dr3;
            this.Close();
        }

        private void NonFrameMBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (this.btn_type)
            {
                case MessageBoxButtons.OK:
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.RetryCancel:
                    switch (e.KeyCode)
                    {
                        case Keys.R:
                            this.button1.Focus();
                            this.button1.PerformClick();
                            break;
                    }
                    break;
                case MessageBoxButtons.YesNo:
                case MessageBoxButtons.YesNoCancel:
                    switch (e.KeyCode)
                    {
                        case Keys.Y:
                            this.button1.Focus();
                            this.button1.PerformClick();
                            break;
                        case Keys.N:
                            this.button2.Focus();
                            this.button2.PerformClick();
                            break;
                    }
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    switch (e.KeyCode)
                    {
                        case Keys.A:
                            this.button1.Focus();
                            this.button1.PerformClick();
                            break;
                        case Keys.R:
                            this.button2.Focus();
                            this.button2.PerformClick();
                            break;
                        case Keys.I:
                            this.button3.Focus();
                            this.button3.PerformClick();
                            break;
                    }

                    break;
            }
        }
    }
}
