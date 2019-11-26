using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();

            XmlTextReader schemaReader = new XmlTextReader("schema.xsd");
            XmlSchema schema = XmlSchema.Read(schemaReader, ValidationCallBack);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(schema);
            // settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            // settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);


            using (XmlReader reader = XmlReader.Create(txtFileName.Text, settings))
            {
                var slotCount = 0;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "BillCassette")
                    {
                        slotCount++;
                    }
                };

                txtOutput.Text += String.Format("Completed reading file. {0} slots found", slotCount);
            }
        }

        private void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
                txtOutput.Text += "[Warning]: " + e.Message + Environment.NewLine;
            else
                txtOutput.Text += e.Message + Environment.NewLine;
        }
    }
}
