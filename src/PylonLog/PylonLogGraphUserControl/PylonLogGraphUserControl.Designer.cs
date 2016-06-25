namespace PylonLogGraphUserControl
{
    partial class PylonLogGraphUserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.zgcPylonLogGraph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zgcPylonLogGraph
            // 
            this.zgcPylonLogGraph.Location = new System.Drawing.Point(40, 31);
            this.zgcPylonLogGraph.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.zgcPylonLogGraph.Name = "zgcPylonLogGraph";
            this.zgcPylonLogGraph.ScrollGrace = 0D;
            this.zgcPylonLogGraph.ScrollMaxX = 0D;
            this.zgcPylonLogGraph.ScrollMaxY = 0D;
            this.zgcPylonLogGraph.ScrollMaxY2 = 0D;
            this.zgcPylonLogGraph.ScrollMinX = 0D;
            this.zgcPylonLogGraph.ScrollMinY = 0D;
            this.zgcPylonLogGraph.ScrollMinY2 = 0D;
            this.zgcPylonLogGraph.Size = new System.Drawing.Size(1235, 808);
            this.zgcPylonLogGraph.TabIndex = 0;
            this.zgcPylonLogGraph.UseExtendedPrintDialog = true;
            // 
            // PylonLogGraphUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zgcPylonLogGraph);
            this.Name = "PylonLogGraphUserControl";
            this.Size = new System.Drawing.Size(1366, 932);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcPylonLogGraph;
    }
}
