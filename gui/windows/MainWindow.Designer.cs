
namespace Tracer.gui
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.listviewTrace = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listviewTrace
            // 
            this.listviewTrace.Location = new System.Drawing.Point(12, 12);
            this.listviewTrace.Name = "listviewTrace";
            this.listviewTrace.Size = new System.Drawing.Size(638, 370);
            this.listviewTrace.TabIndex = 0;
            this.listviewTrace.UseCompatibleStateImageBehavior = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 415);
            this.Controls.Add(this.listviewTrace);
            this.Name = "MainWindow";
            this.Text = "Tracer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listviewTrace;
    }
}

