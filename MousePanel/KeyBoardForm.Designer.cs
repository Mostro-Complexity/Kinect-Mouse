namespace MousePanel {
    partial class KeyBoardForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyBoardForm));
            this.panel2D1 = new Canvas2D.Panel2D();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SuspendLayout();
            // 
            // panel2D1
            // 
            this.panel2D1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2D1.Location = new System.Drawing.Point(0, 0);
            this.panel2D1.MouseLocation = ((OpenTK.Vector2)(resources.GetObject("panel2D1.MouseLocation")));
            this.panel2D1.Name = "panel2D1";
            this.panel2D1.Size = new System.Drawing.Size(438, 388);
            this.panel2D1.TabIndex = 0;
            this.panel2D1.DrawGame += new Canvas2D.Panel2D.DrawGameHandler(this.Canvas2D_DrawGame);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // KeyBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 388);
            this.Controls.Add(this.panel2D1);
            this.Name = "KeyBoardForm";
            this.Text = "尚未启动";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private Canvas2D.Panel2D panel2D1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}

