using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Globalization;
using EHomeDemo.Language;
namespace EHomeDemo
{
    public partial class SelectIP : Form
    {
        
        public SelectIP()
        {
            InitializeComponent();
            GetLocalIP();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Update();
            this.Close();
        }

        private void SelectIP_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strText = comboBoxLanguage.Text;
            if(strText == "Chinese")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                MultiLanguage.SetDefaultLanguage("Chinese");
            }
            else if(strText == "English")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                MultiLanguage.SetDefaultLanguage("English");
            }
        }
    }
}
