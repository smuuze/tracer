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
    public partial class TracerGuiFileContentListBox : ListBox
    {
        /// <summary>
        /// The debug mode
        /// </summary>
        private DEBUG_MODE debugMode = DEBUG_MODE.CONSOLE;

        /// <summary>
        /// The active line number
        /// </summary>
        private int activeLineNumber;

        /// <summary>
        /// The actual file name
        /// </summary>
        private string actualFileName = "null";

        public TracerGuiFileContentListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 15;

            InitializeComponent();
            this.BackColor = Color.DarkBlue;
            this.ForeColor = Color.Turquoise;

            this.activeLineNumber = -1;
        }

        /// <summary>
        /// Sets the active line.
        /// </summary>
        /// <value>
        /// The active line number.
        /// </value>
        /// <param name="lineNumber">The line number.</param>
        public int ActiveLineNumber
        {
            get
            {
                return activeLineNumber;
            }
            set
            {
                activeLineNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the actual name of the file.
        /// </summary>
        /// <value>
        /// The actual name of the file.
        /// </value>
        public string ActualFileName
        {
            get
            {
                return actualFileName;
            }

            set
            {
                actualFileName = value;
            }
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
        /// Raises the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="pe">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        /// <summary>
        /// Löst das <see cref="E:System.Windows.Forms.ListBox.DrawItem" />-Ereignis aus.
        /// </summary>
        /// <param name="e">Ein <see cref="T:System.Windows.Forms.DrawItemEventArgs" />, das die Ereignisdaten enthält.</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //base.OnDrawItem(e);

            debug("TraceGuiListBox.OnDrawItem()");

            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;

            if (e.Index >= 0)
            {
                TraceFileContentElement item = (TraceFileContentElement)Items[e.Index];

                string codeLineText = item.Line;
                string lineNumberText = Convert.ToString(item.LineNumber);

                var lineNumberRect = e.Bounds;
                lineNumberRect.Height = 15;
                lineNumberRect.Width = 35;

                var codeLineRect = e.Bounds;
                codeLineRect.X += lineNumberRect.Width;
                codeLineRect.Height = lineNumberRect.Height;
                codeLineRect.Width = this.Size.Width > lineNumberRect.Height ? this.Size.Width - lineNumberRect.Height : 100;


                //fileNameRect.X += codeLineRect.Width;
                //fileNameRect.Width = e.Bounds.Width - codeLineRect.Width;
                //fileNameRect.Height = codeLineRect.Height;

                var codeLineFont = new Font("Lucida Console", e.Font.Size, FontStyle.Regular, e.Font.Unit);
                var lineNUmberFont = new Font(e.Font.Name, e.Font.Size - 2, FontStyle.Regular, e.Font.Unit);

                Color backgroundColor = e.BackColor;
                Color foreColor = e.ForeColor;


                if (item.LineNumber == activeLineNumber)
                {
                    backgroundColor = Color.Blue;
                    foreColor = Color.LightSeaGreen;
                }

                e = new DrawItemEventArgs(e.Graphics,
                                   e.Font,
                                   e.Bounds,
                                   e.Index,
                                   e.State,
                                   foreColor,
                                   backgroundColor);//Choose the color

                e.DrawBackground();
                TextRenderer.DrawText(e.Graphics, lineNumberText, lineNUmberFont, lineNumberRect, e.ForeColor, flags);
                TextRenderer.DrawText(e.Graphics, codeLineText, codeLineFont, codeLineRect, e.ForeColor, flags);
                e.DrawFocusRectangle();
            }
        }
    }
}
