using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TaskEngineModule;
using Tracer.app.task;

namespace Tracer.gui
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// The engine
        /// </summary>
        private TaskEngine engine;

        /// <summary>
        /// The comport window
        /// </summary>
        private ComportSelectionWindow comportWindow;

        public MainWindow()
        {
            InitializeComponent();

            engine = new TaskEngine();

            engine.register(new TracerDataCatcherTask(engine, TracerDataCatcherTask.INTERVAL_TIMEOUT_MS));
            engine.register(new TracerFileLoadTask(engine, TracerFileLoadTask.INTERVAL_TIMEOUT_MS));

            engine.start(50);

            this.Visible = true;

            comportWindow = new ComportSelectionWindow();
            comportWindow.prepare();
            comportWindow.StartPosition = FormStartPosition.CenterParent;
            comportWindow.ShowDialog(this);
        }

        private void onTraceEvent()
        {

            // remember this method is called by an other thread
            // if required give back controll of form-elemnts
            // back to gui-thread (gui-thread will call the update method by itself)
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new LegicUpdateNotification.UpdateNotificationHandler(onLegicEvent));
                }
                catch (ObjectDisposedException)
                {

                }
                return;
            }
        }
    }
}
