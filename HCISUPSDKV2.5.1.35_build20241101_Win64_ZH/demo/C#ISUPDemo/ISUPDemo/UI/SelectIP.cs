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
using ISUPDemo.Language;
namespace ISUPDemo
{
    public partial class SelectIP : Form
    {
        public static List<IPAddress> IpAddressList=new List<IPAddress>();
        public SelectIP()
        {
            InitializeComponent();
            GetLocalIP();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Update();

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            MultiLanguage.SetDefaultLanguage("English");
            this.Close();
        }

        private void SelectIP_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }
    }
}
