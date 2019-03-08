using Connection.SerialIO;
using Debug;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tracer.app;
using Tracer.app.modules;
using Tracer.app.types;
using Tracer.src.app;

namespace Tracer.gui
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Form
    {

        /// <summary>
        /// The comport window
        /// </summary>
        private ComportSelectionWindow comportWindow;

        /// <summary>
        /// The progress trace window
        /// </summary>
        private TraceInProgressWindow progressTraceWindow;

        /// <summary>
        /// The debug mode
        /// </summary>
        private DEBUG_MODE debugMode = DEBUG_MODE.DISABLED;

        /// <summary>
        /// The trace file content ListBox
        /// </summary>
        private TracerGuiFileContentListBox traceFileContentListBox;

        /// <summary>
        /// The command answe binding list
        /// </summary>
        private BindingList<TraceGuiListItem> traceBindingList;

        /// <summary>
        /// The file content binding list
        /// </summary>
        private BindingList<TraceFileContentElement> fileContentBindingList;

        /// <summary>
        /// The trace element list
        /// </summary>
        private List<TraceElement> traceElementList;

        /// <summary>
        /// The trace history is active
        /// </summary>
        private bool traceHistoryIsActive = false;

        public MainWindow()
        {
            initUserContext();

            DebugFactory.getInstance().DebugLevel = DEBUG_LEVEL.ERROR;
            debugMode = DEBUG_MODE.FILE;

            InitializeComponent();

            this.Visible = true;

            traceElementList = new List<TraceElement>();

            // ----------------------------------------------------------------

            traceListBox = new TraceGuiListBox();
            traceBindingList = new BindingList<TraceGuiListItem>();

            tableLayoutPanel1.Controls.Add(traceListBox, 0, 0);

            traceListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            traceListBox.FormattingEnabled = true;
            traceListBox.Location = new System.Drawing.Point(3, 3);
            traceListBox.Size = new System.Drawing.Size(656, 389);
            traceListBox.TabIndex = 0;
            traceListBox.SelectedIndexChanged += new EventHandler(onTraceListBoxSelectedIndexChanged);

            traceListBox.HorizontalScrollbar = true;
            traceListBox.DataSource = traceBindingList;

            // ------------------------------------------------------

            traceFileContentListBox = new TracerGuiFileContentListBox();
            fileContentBindingList = new BindingList<TraceFileContentElement>();

            this.tableLayoutPanel1.Controls.Add(this.traceFileContentListBox, 1, 0);

            this.traceFileContentListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traceFileContentListBox.Location = new System.Drawing.Point(339, 3);
            this.traceFileContentListBox.Size = new System.Drawing.Size(320, 409);
            this.traceFileContentListBox.TabIndex = 0;
            this.traceFileContentListBox.Text = "";

            traceFileContentListBox.HorizontalScrollbar = true;
            traceFileContentListBox.DataSource = fileContentBindingList;

            // ------------------------------------------------------

            TracerFactory.getInstance().getInterface().init();
            TracerFactory.getInstance().getInterface().setConnectionInterface(SerialIOFactory.getInstance().getSerialConnection());
            TracerFactory.getInstance().getInterface().getUpdateHandler().Event_UpdateNotification += new TracerUpdateNotification.UpdateNotificationHandler(onTraceEvent);
            
            this.traceListBox.KeyDown += new KeyEventHandler(onKeyDown);
            this.KeyDown += new KeyEventHandler(onKeyDown);
            this.traceFileContentListBox.KeyDown += new KeyEventHandler(onKeyDown);

            progressTraceWindow = new TraceInProgressWindow();
            progressTraceWindow.Event_UpdateNotification += new CommonWindow.UpdateNotificationHandler(onWindowEvent);

            comportWindow = new ComportSelectionWindow();
            comportWindow.prepare();
            comportWindow.StartPosition = FormStartPosition.CenterParent;
            comportWindow.Event_UpdateNotification += new CommonWindow.UpdateNotificationHandler(onWindowEvent);
            comportWindow.ShowDialog(this);
        }

        /// <summary>
        /// Ons the key down.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void onKeyDown(object o, KeyEventArgs e)
        {
            debug("MainWindow.onKeyDown()");

            if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.T)
            {
                comportWindow.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Return && traceListBox.Items.Count != 0)
            {
                if (traceHistoryIsActive)
                {
                    traceHistoryIsActive = false;
                    loadTracesIntoList();
                }
                else
                {
                    traceHistoryIsActive = true;
                    showHistoryOfTraceEntry();
                }
            }
        }

        /// <summary>
        /// Ons the trace event.
        /// </summary>
        private void onTraceEvent()
        {
            // remember this method is called by an other thread
            // if required give back controll of form-elemnts
            // back to gui-thread (gui-thread will call the update method by itself)
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new TracerUpdateNotification.UpdateNotificationHandler(onTraceEvent));
                }
                catch (ObjectDisposedException)
                {

                }
                return;
            }

            debug("MainWindow.onTraceEvent()");

            loadTracesFromInterface();
        }

        /// <summary>
        /// Ons the window event.
        /// </summary>
        private void onWindowEvent()
        {
            debug("MainWindow.onWindowEvent()");

            if (comportWindow.hasChanged())
            {
                if (comportWindow.isValid())
                {
                    startNewTrace();
                }
            }

            if (progressTraceWindow.hasChanged())
            {
                TracerFactory.getInstance().getInterface().stopTracing();
                loadTracesIntoList();
            }
        }

        /// <summary>
        /// Ons the selected index changed.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void onTraceListBoxSelectedIndexChanged(object o, EventArgs e)
        {
            debug("MainWindow.onTraceListBoxSelectedIndexChanged()");

            TraceGuiListItem traceItem = traceListBox.SelectedItem as TraceGuiListItem;

            if (traceItem == null)
            {
                return;
            }

            if (traceFileContentListBox.ActualFileName.CompareTo(traceItem.FileName) == 0)
            {
                labelTraceFileLine.Text = Convert.ToString(traceItem.LineNumber + 1);
                traceFileContentListBox.ActiveLineNumber = traceItem.LineNumber + 1;
                traceFileContentListBox.SelectedIndex = traceItem.LineNumber;
                //traceFileContentListBox.Refresh();
                return;
            }

            labelTraceFilePath.Text = traceItem.FileName;
            labelTraceFileLine.Text = Convert.ToString(traceItem.LineNumber + 1);

            traceFileContentListBox.ActiveLineNumber = traceItem.LineNumber + 1;
            traceFileContentListBox.ActualFileName = traceItem.FileName;
            fileContentBindingList.Clear();

            string[] fileContent = File_Factory.FileFactory.getInstance().getFileContentAsLineArray(TracerContext.getInstance().BasicFilePath + traceItem.FileName);

            for (int i = 0; i < fileContent.Length; i++ )
            {
                fileContentBindingList.Add(new TraceFileContentElement(i + 1, fileContent[i]));
            }

            traceFileContentListBox.SelectedIndex = traceItem.LineNumber;            
            return;
        }

        /// <summary>
        /// Loads the traces from interface.
        /// </summary>
        private void loadTracesFromInterface()
        {
            TraceElement traceElement = TracerFactory.getInstance().getInterface().getNextElement();

            int elementCoutner = 0;

            while (traceElement != null)
            {
                elementCoutner += 1;
                // -------------------traceBindingList.Add(new TraceGuiListItem(traceElement));
                traceElementList.Add(traceElement);
                traceElement = TracerFactory.getInstance().getInterface().getNextElement();
            }

            labelTraceCount.Text = Convert.ToString(traceBindingList.Count);
            debug("MainWindow.loadTracesFromInterface() - New Element count: " + elementCoutner);
        }

        /// <summary>
        /// Loads the traces into list.
        /// </summary>
        private void loadTracesIntoList()
        {
            traceBindingList.Clear();

            foreach (TraceElement element in traceElementList)
            {
                traceBindingList.Add(new TraceGuiListItem(element));
            }
        }

        /// <summary>
        /// Shows the history of trace entry.
        /// </summary>
        private void showHistoryOfTraceEntry()
        {
            debug("MainWindow.showHistoryOfTraceEntry() - SelectedTraceElement Index : " + traceListBox.SelectedIndex);

            TraceElement activeTraceElement = traceElementList[traceListBox.SelectedIndex] as TraceElement;

            if (activeTraceElement == null)
            {
                debug("MainWindow.showHistoryOfTraceEntry() - SelectedTraceElement is NULL !!! ---");
                return;
            }

            traceBindingList.Clear();
            //fileContentBindingList.Clear();

            debug("MainWindow.showHistoryOfTraceEntry() - File: " + activeTraceElement.FileName + " / Line: " + activeTraceElement.LineNumber);

            for (int i = 0; i < traceElementList.Count; i++ )
            {
                if (activeTraceElement.FileName.CompareTo(traceElementList[i].FileName) != 0)
                {
                    continue;
                }

                if (activeTraceElement.LineNumber != traceElementList[i].LineNumber)
                {
                    continue;
                }

                traceBindingList.Add(new TraceGuiListItem(traceElementList[i]));
            }
        }

        /// <summary>
        /// Starts the new trace.
        /// </summary>
        private void startNewTrace()
        {
            traceBindingList.Clear();
            fileContentBindingList.Clear();
            traceElementList.Clear();

            traceFileContentListBox.ActiveLineNumber = -1;
            traceFileContentListBox.ActualFileName = "null";

            labelTraceCount.Text = "0";

            getUserContext().ComportSelection = comportWindow.Comport;
            getUserContext().BaudrateSelection = comportWindow.Baudrate;

            TracerContext.getInstance().Comport = comportWindow.Comport;
            TracerContext.getInstance().Baudrate = comportWindow.Baudrate;
            TracerContext.getInstance().TraceActive = true;

            TracerFactory.getInstance().getInterface().startTracing();

            progressTraceWindow.StartPosition = FormStartPosition.CenterParent;
            progressTraceWindow.ShowDialog(this);
        }

        /// <summary>
        /// Debugs the specified d string.
        /// </summary>
        /// <param name="dString">The d string.</param>
        private void debug(string dString)
        {
            DebugFactory.getInstance().debug(debugMode, dString);
        }

        /// <summary>
        /// Debugs the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="dString">The d string.</param>
        private void debug(DEBUG_LEVEL level,string dString)
        {
            DebugFactory.getInstance().debug(level, debugMode, dString);
        }

        /// <summary>
        /// Gets the user context.
        /// </summary>
        /// <returns></returns>
        private UserContext getUserContext()
        {
            return UserContext.getInstance();
        }

        /// <summary>
        /// Initializes the user context.
        /// </summary>
        private void initUserContext()
        {
            getUserContext().TracerUserHome = String.Format("{0}\\{1}\\{2}", Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName, "Local", "Tracer");
            getUserContext().TracerConfigFile = String.Format("{0}\\{1}.{2}", getUserContext().TracerUserHome, "tracer_configuration", "cfg");

            string debugFileName = string.Format("tracer_debug_{0}_{1}_{2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            getUserContext().TracerDebugFile = String.Format("{0}\\{1}", getUserContext().TracerUserHome, debugFileName);

            if (!Directory.Exists(getUserContext().TracerUserHome))
            {
                Directory.CreateDirectory(getUserContext().TracerUserHome);
            }

            if (!File.Exists(getUserContext().TracerConfigFile))
            {
                FileStream fileStream = File.Create(getUserContext().TracerConfigFile);
                fileStream.Close();
            }
        }
    }
}
