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

namespace Tracer.src.gui
{
    public partial class TraceMainListBox : ListBox
    {
        public TraceMainListBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 50;
            InitializeComponent();
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

            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;

            if (e.Index >= 0)
            {
                TraceElement item = (TraceElement)Items[e.Index];
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
                 * */
            }
        }
    }
}
