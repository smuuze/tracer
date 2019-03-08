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
using Tracer.app;

namespace Tracer.gui
{
    public partial class ComportSelectionWindow : CommonWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComportSelectionWindow"/> class.
        /// </summary>
        public ComportSelectionWindow()
        {
            InitializeComponent();

            this.debugMode = Debug.DEBUG_MODE.CONSOLE;

            comboBoxComport.SelectedItem = 0;
            comboBoxBaudrate.SelectedItem = 0;

            this.KeyDown += new KeyEventHandler(onKeyDown);
            comboBoxComport.KeyDown += new KeyEventHandler(onKeyDown);
            comboBoxBaudrate.KeyDown += new KeyEventHandler(onKeyDown);
            buttonCancel.KeyDown += new KeyEventHandler(onKeyDown);
            buttonConnect.KeyDown += new KeyEventHandler(onKeyDown);
        }

        /// <summary>
        /// Ons the key down.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void onKeyDown(object o, KeyEventArgs e)
        {
            debug("ComportSelectionWindow.onKeyDown()");

            if (e.KeyCode == Keys.Escape)
            {
                this.Visible = false;
                this.setValid(false);
                this.setChanged(true);
                this.invokeEvent();
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonConnect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            debug("ComportSelectionWindow.buttonConnect_Click()");

            this.Visible = false;
            this.setValid(true);
            this.setChanged(true);
            this.invokeEvent();
        }

        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// Prepares this instance.
        /// </summary>
        public void prepare()
        {
            comboBoxComport.Items.AddRange(SerialIOFactory.getInstance().getSerialConnection().getAvailablePorts());
            comboBoxBaudrate.Items.AddRange(SerialIOFactory.getInstance().getSerialConnection().getAvailableBaudrates());
            comboBoxBaudrate.SelectedIndex = 0; // selects the first entry in the list
        }

        /// <summary>
        /// Gets the comport.
        /// </summary>
        /// <value>
        /// The comport.
        /// </value>
        public string Comport
        {
            get
            {
                return (string)comboBoxComport.SelectedItem;
            }
        }

        /// <summary>
        /// Gets the baudrate.
        /// </summary>
        /// <value>
        /// The baudrate.
        /// </value>
        public BAUDRATE Baudrate
        {
            get
            {
                return (BAUDRATE)Convert.ToInt32((string)comboBoxBaudrate.SelectedItem);
            }
        }
    }
}
