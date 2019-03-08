using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tracer.gui
{
    public partial class TraceInProgressWindow : CommonWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceInProgressWindow"/> class.
        /// </summary>
        public TraceInProgressWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(onKeyEvent);
        }

        /// <summary>
        /// Ons the key event.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void onKeyEvent(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Visible = false;
                this.setValid(false);
                this.setChanged(true);
                this.invokeEvent();
            }
        }
    }
}
