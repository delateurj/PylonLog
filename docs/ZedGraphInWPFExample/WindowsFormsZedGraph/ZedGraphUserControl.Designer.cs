namespace WindowsFormsZedGraph
{
    partial class ZedGraphUserControl
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
            this.zgcGraph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zgcGraph
            // 
            this.zgcGraph.Location = new System.Drawing.Point(0, 0);
            this.zgcGraph.Name = "zgcGraph";
            this.zgcGraph.ScrollGrace = 0;
            this.zgcGraph.ScrollMaxX = 0;
            this.zgcGraph.ScrollMaxY = 0;
            this.zgcGraph.ScrollMaxY2 = 0;
            this.zgcGraph.ScrollMinX = 0;
            this.zgcGraph.ScrollMinY = 0;
            this.zgcGraph.ScrollMinY2 = 0;
            this.zgcGraph.Size = new System.Drawing.Size(506, 429);
            this.zgcGraph.TabIndex = 0;
            this.zgcGraph.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgcGraph_MouseDownEvent);
            // 
            // ZedGraphUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.zgcGraph);
            this.Name = "ZedGraphUserControl";
            this.Size = new System.Drawing.Size(509, 432);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcGraph;
    }
}
