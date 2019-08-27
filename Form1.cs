using System;
using System.Drawing;
using System.Windows.Forms;

namespace Wake_up
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly Handler handler = new Handler();

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            bool isStart = handler.Connect(tbLogin.Text, tbPassword.Text);

            if (isStart)
            {
                SetParamsLabelConnectStatus(true);
                handler.SendMessage();
            }
            else
            {
                SetParamsLabelConnectStatus(false);
            }
        }

        private void SetParamsLabelConnectStatus(bool b)
        {
            if (handler.Connect(tbLogin.Text, tbPassword.Text))
            {
                labelConnectStatus.Text = "Successfully";
                labelConnectStatus.ForeColor = Color.Green;
            }
            else
            {
                labelConnectStatus.Text = "Error";
                labelConnectStatus.ForeColor = Color.Red;
            }
        }
    }
}
