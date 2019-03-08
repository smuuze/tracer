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
using Tracer.app.modules;
using Tracer.app.types;

namespace Tracer.gui
{
    public partial class TraceGuiListBox : ListBox
    {
        /// <summary>
        /// The debug mode
        /// </summary>
        private DEBUG_MODE debugMode = DEBUG_MODE.CONSOLE;

        public TraceGuiListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 45;
            InitializeComponent();
            this.BackColor = Color.DarkBlue;
            this.ForeColor = Color.Turquoise;
        }

        /// <summary>
        /// Debugs the specified d string.
        /// </summary>
        /// <param name="dString">The d string.</param>
        private void debug(string dString)
        {
            DebugFactory.getInstance().debug(debugMode, dString);
        }

        //private void onSelectedIndexCHanged

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
                TraceGuiListItem item = (TraceGuiListItem)Items[e.Index];

                string codeLineText = item.CodeLine;
                string dataText = "";

                string arrayText = StringParser.getInstance().byteArray2HexString(item.DataArray, ' ');
                string wordDataText = "Data: ";
                string wordByteWordLongText = "";

                switch (item.Type)
                {
                    default :
                        wordDataText = "";
                        break;

                    case TraceType.BYTE:
                        wordByteWordLongText = "Byte: ";
                        dataText = string.Format("{0}", item.DataByte);
                        break;

                    case TraceType.WORD:
                        wordByteWordLongText = "Word: ";
                        dataText = string.Format("{0}", item.DataWord);
                        break;

                    case TraceType.LONG:
                        wordByteWordLongText = "Long: ";
                        dataText = string.Format("{0}", item.DataLong);
                        break;

                    case TraceType.ARRAY:
                        break;
                }

                var codeLineRect = e.Bounds;
                codeLineRect.Height = 15;
                codeLineRect.Width = this.Size.Width;

                var wordDataRect = e.Bounds;
                wordDataRect.Y += codeLineRect.Height;
                wordDataRect.Height = 15;
                wordDataRect.Width = 30;

                var wordByteWordLongRect = e.Bounds;
                wordByteWordLongRect.Y += codeLineRect.Height + wordDataRect.Height;
                wordByteWordLongRect.X = wordDataRect.X;
                wordByteWordLongRect.Height = wordDataRect.Height;
                wordByteWordLongRect.Width = 30;

                var arrayDataRect = e.Bounds;
                arrayDataRect.Y += codeLineRect.Height;
                arrayDataRect.X += wordDataRect.Width;
                arrayDataRect.Height = 15;
                arrayDataRect.Width = this.Width > wordDataRect.Width ? this.Width - wordDataRect.Width : 100;

                var valueDataRect = e.Bounds;
                valueDataRect.Y += codeLineRect.Height + arrayDataRect.Height;
                valueDataRect.X += wordByteWordLongRect.Width;
                valueDataRect.Height = 15;
                valueDataRect.Width = this.Width > wordByteWordLongRect.Width ? this.Width - wordByteWordLongRect.Width : 100;


                //fileNameRect.X += codeLineRect.Width;
                //fileNameRect.Width = e.Bounds.Width - codeLineRect.Width;
                //fileNameRect.Height = codeLineRect.Height;

                var codeLineFont = new Font("Lucida Console", e.Font.Size + 1, FontStyle.Regular, e.Font.Unit);
                var wordFont = new Font(e.Font.Name, e.Font.Size - 2, FontStyle.Regular, e.Font.Unit);
                var dataFont = new Font(e.Font.Name, e.Font.Size, FontStyle.Regular, e.Font.Unit);

                Color backgroundColor = e.BackColor;

                e = new DrawItemEventArgs(e.Graphics,
                                   e.Font,
                                   e.Bounds,
                                   e.Index,
                                   e.State,
                                   e.ForeColor,
                                   backgroundColor);//Choose the color

                e.DrawBackground();
                TextRenderer.DrawText(e.Graphics, codeLineText, codeLineFont, codeLineRect, e.ForeColor, flags);
                TextRenderer.DrawText(e.Graphics, wordDataText, wordFont, wordDataRect, Color.DeepSkyBlue, flags);
                TextRenderer.DrawText(e.Graphics, wordByteWordLongText, wordFont, wordByteWordLongRect, Color.DeepSkyBlue, flags);
                TextRenderer.DrawText(e.Graphics, arrayText, dataFont, arrayDataRect, Color.DeepSkyBlue, flags);
                TextRenderer.DrawText(e.Graphics, dataText, dataFont, valueDataRect, Color.DeepSkyBlue, flags);
                e.DrawFocusRectangle();




                /*
                string dateText = string.Format("{0}:{1}:{2}:{3}", item.Timestamp.Hour, item.Timestamp.Minute, item.Timestamp.Second, item.Timestamp.Millisecond);
                string itemText = string.Format("{0} : {1}", item.CommandName, item.AnswerStatus);
                string hexStringText = string.Format("Command: \t {0}\nAsnwer: \t {1}", item.CommandHexString, item.AnswerHexString);

                var timestampRect = e.Bounds;
                timestampRect.Height = 15;
                timestampRect.Width = 75;

                var itemRect = e.Bounds;
                itemRect.X += timestampRect.Width;
                itemRect.Width = e.Bounds.Width - timestampRect.Width;
                itemRect.Height = timestampRect.Height;

                var hexStringRect = e.Bounds;
                hexStringRect.X = itemRect.X;
                hexStringRect.Y += itemRect.Height;
                hexStringRect.Width = itemRect.Width;
                hexStringRect.Height = e.Bounds.Height - itemRect.Height;

                var itemFont = new Font(e.Font.Name, e.Font.Size + 1, FontStyle.Bold, e.Font.Unit);
                var hexStringFont = new Font(e.Font.Name, e.Font.Size - 1, FontStyle.Regular, e.Font.Unit);

                Color backgroundColor = e.BackColor;

                switch (item.SuccessState)
                {
                    default:
                    case CommandAnswerListElement.SUCCESS_STATE.SUCCESS: backgroundColor = Color.LightGreen; break;
                    case CommandAnswerListElement.SUCCESS_STATE.WARNING: backgroundColor = Color.Yellow; break;
                    case CommandAnswerListElement.SUCCESS_STATE.FAILURE: backgroundColor = Color.LightCoral; break;
                }

                e = new DrawItemEventArgs(e.Graphics,
                                  e.Font,
                                  e.Bounds,
                                  e.Index,
                                  e.State,
                                  e.ForeColor,
                                  backgroundColor);//Choose the color

                e.DrawBackground();
                TextRenderer.DrawText(e.Graphics, dateText, e.Font, timestampRect, e.ForeColor, flags);
                TextRenderer.DrawText(e.Graphics, itemText, itemFont, itemRect, e.ForeColor, flags);
                TextRenderer.DrawText(e.Graphics, hexStringText, hexStringFont, hexStringRect, e.ForeColor, flags);
                e.DrawFocusRectangle();
                */
            }
        }
    }
}
