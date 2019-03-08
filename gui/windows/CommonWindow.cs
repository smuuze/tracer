using Debug;
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
    public partial class CommonWindow : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public delegate void UpdateNotificationHandler();

        /// <summary>
        /// The event_ update notification
        /// </summary>
        public UpdateNotificationHandler Event_UpdateNotification;

        /// <summary>
        /// The valid
        /// </summary>
        protected bool valid;

        /// <summary>
        /// The changed
        /// </summary>
        protected bool changed;

        /// <summary>
        /// The debug mode
        /// </summary>
        protected DEBUG_MODE debugMode = DEBUG_MODE.DISABLED;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonWindow"/> class.
        /// </summary>
        public CommonWindow()
        {

        }

        /// <summary>
        /// Sets the changed.
        /// </summary>
        /// <param name="c">if set to <c>true</c> [c].</param>
        protected void setChanged(bool c)
        {
            changed = c;
        }

        /// <summary>
        /// Determines whether this instance has changed.
        /// </summary>
        /// <returns></returns>
        public bool hasChanged()
        {
            bool t = changed;
            changed = false;
            return t;
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            bool t = valid;
            setValid(false);
            return t;
        }

        /// <summary>
        /// Sets the valid.
        /// </summary>
        /// <param name="v">if set to <c>true</c> [v].</param>
        protected void setValid(bool v)
        {
            valid = v;
        }

        /// <summary>
        /// Invokes the event.
        /// </summary>
        protected void invokeEvent()
        {
            Event_UpdateNotification.Invoke();
        }

        /// <summary>
        /// Debugs the specified d string.
        /// </summary>
        /// <param name="dString">The d string.</param>
        protected void debug(string dString)
        {
            DebugFactory.getInstance().debug(debugMode, dString);
        }

        /// <summary>
        /// Enables the debug.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        public void enableDebug(bool enable)
        {
            debugMode = enable == true ? DEBUG_MODE.CONSOLE : DEBUG_MODE.DISABLED;
        }
    }
}
