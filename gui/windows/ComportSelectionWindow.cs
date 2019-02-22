using Connection;
using Connection.SerialIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tracer.app.types;

namespace Tracer.gui
{
    public partial class ComportSelectionWindow : Form
    {
        public ComportSelectionWindow()
        {
            InitializeComponent();

            comboBoxComport.SelectedItem = 0;
            comboBoxBaudrate.SelectedItem = 0;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            TracerContext.getInstance().Comport = (string)comboBoxComport.SelectedItem;
            TracerContext.getInstance().Baudrate = (BAUDRATE)Convert.ToInt32((string)comboBoxBaudrate.SelectedItem);
            TracerContext.getInstance().TraceActive = true;

            this.Visible = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public void prepare()
        {
            comboBoxComport.Items.AddRange(SerialIOFactory.getInstance().getSerialConnection().getAvailablePorts());
            comboBoxBaudrate.Items.AddRange(SerialIOFactory.getInstance().getSerialConnection().getAvailableBaudrates());
            comboBoxBaudrate.SelectedIndex = 0; // selects the first entry in the list
        }
    }
}
