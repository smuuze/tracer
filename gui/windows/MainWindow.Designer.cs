
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelWordFile = new System.Windows.Forms.Label();
            this.labelTraceFilePath = new System.Windows.Forms.Label();
            this.labelWordLine = new System.Windows.Forms.Label();
            this.labelTraceFileLine = new System.Windows.Forms.Label();
            this.labelTraceCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.93976F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(662, 415);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelTraceCount, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 388);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(662, 27);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel3.Controls.Add(this.labelWordFile, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelTraceFilePath, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelWordLine, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelTraceFileLine, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(606, 21);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // labelWordFile
            // 
            this.labelWordFile.AutoSize = true;
            this.labelWordFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWordFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWordFile.Location = new System.Drawing.Point(3, 0);
            this.labelWordFile.Name = "labelWordFile";
            this.labelWordFile.Size = new System.Drawing.Size(39, 21);
            this.labelWordFile.TabIndex = 0;
            this.labelWordFile.Text = "File:";
            // 
            // labelTraceFilePath
            // 
            this.labelTraceFilePath.AutoSize = true;
            this.labelTraceFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTraceFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTraceFilePath.Location = new System.Drawing.Point(48, 0);
            this.labelTraceFilePath.Name = "labelTraceFilePath";
            this.labelTraceFilePath.Size = new System.Drawing.Size(252, 21);
            this.labelTraceFilePath.TabIndex = 1;
            // 
            // labelWordLine
            // 
            this.labelWordLine.AutoSize = true;
            this.labelWordLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWordLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWordLine.Location = new System.Drawing.Point(306, 0);
            this.labelWordLine.Name = "labelWordLine";
            this.labelWordLine.Size = new System.Drawing.Size(39, 21);
            this.labelWordLine.TabIndex = 2;
            this.labelWordLine.Text = "Line: ";
            // 
            // labelTraceFileLine
            // 
            this.labelTraceFileLine.AutoSize = true;
            this.labelTraceFileLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTraceFileLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTraceFileLine.Location = new System.Drawing.Point(351, 0);
            this.labelTraceFileLine.Name = "labelTraceFileLine";
            this.labelTraceFileLine.Size = new System.Drawing.Size(252, 21);
            this.labelTraceFileLine.TabIndex = 3;
            // 
            // labelTraceCount
            // 
            this.labelTraceCount.AutoSize = true;
            this.labelTraceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTraceCount.Location = new System.Drawing.Point(615, 0);
            this.labelTraceCount.Name = "labelTraceCount";
            this.labelTraceCount.Size = new System.Drawing.Size(44, 27);
            this.labelTraceCount.TabIndex = 1;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 415);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainWindow";
            this.Text = "Tracer";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        private TraceGuiListBox traceListBox;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelWordFile;
        private System.Windows.Forms.Label labelTraceFilePath;
        private System.Windows.Forms.Label labelWordLine;
        private System.Windows.Forms.Label labelTraceFileLine;
        private System.Windows.Forms.Label labelTraceCount;




    }
}

