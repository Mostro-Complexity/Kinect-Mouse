namespace MainForm {
    partial class Arrange {
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拍照ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.键盘ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3D1 = new Canvas3D.Panel3D();
            this.clsControl2DBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clsControl2DBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(589, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空ToolStripMenuItem,
            this.添加ToolStripMenuItem,
            this.开始ToolStripMenuItem,
            this.拍照ToolStripMenuItem,
            this.键盘ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.清空ToolStripMenuItem.Text = "清空";
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.On_Models_Adding);
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.开始ToolStripMenuItem.Text = "开始";
            this.开始ToolStripMenuItem.Click += new System.EventHandler(this.On_Control_Adding);
            // 
            // 拍照ToolStripMenuItem
            // 
            this.拍照ToolStripMenuItem.Name = "拍照ToolStripMenuItem";
            this.拍照ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.拍照ToolStripMenuItem.Text = "拍照";
            this.拍照ToolStripMenuItem.Click += new System.EventHandler(this.OnRecord);
            // 
            // 键盘ToolStripMenuItem
            // 
            this.键盘ToolStripMenuItem.Name = "键盘ToolStripMenuItem";
            this.键盘ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.键盘ToolStripMenuItem.Text = "键盘";
            // 
            // panel3D1
            // 
            this.panel3D1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3D1.Location = new System.Drawing.Point(0, 28);
            this.panel3D1.Margin = new System.Windows.Forms.Padding(4);
            this.panel3D1.Name = "panel3D1";
            this.panel3D1.Size = new System.Drawing.Size(589, 583);
            this.panel3D1.TabIndex = 1;
            this.panel3D1.DrawGame += new Canvas3D.Panel3D.DrawGameHandler(this.Canvas3D_DrawGame);
            this.panel3D1.Resize += new System.EventHandler(this.Panel3D_Resize);
            // 
            // clsControl2DBindingSource
            // 
            this.clsControl2DBindingSource.DataSource = typeof(Canvas2D.Controls.Control2D);
            // 
            // Arrange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 611);
            this.Controls.Add(this.panel3D1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Arrange";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnQuit);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clsControl2DBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private Canvas3D.Panel3D panel3D1;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 拍照ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 键盘ToolStripMenuItem;
        private System.Windows.Forms.BindingSource clsControl2DBindingSource;
    }
}
