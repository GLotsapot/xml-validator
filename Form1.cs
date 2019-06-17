using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xmlValidator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var result = ofdXMLFile.ShowDialog();
            if(result == DialogResult.OK)
            {
                txtFileName.Text = ofdXMLFile.FileName;
                txtFileName.ReadOnly = true;
                btnCheck.Enabled = true;
            }
            
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
