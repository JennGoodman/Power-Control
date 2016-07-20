namespace Power_Control
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.highPerformanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.balancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerSaverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Control power profiles better than Windows.";
            this.notifyIcon.BalloonTipTitle = "Power Control";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Power Control";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highPerformanceToolStripMenuItem,
            this.balancedToolStripMenuItem,
            this.powerSaverToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(172, 92);
            // 
            // highPerformanceToolStripMenuItem
            // 
            this.highPerformanceToolStripMenuItem.Name = "highPerformanceToolStripMenuItem";
            this.highPerformanceToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.highPerformanceToolStripMenuItem.Text = "High Performance";
            this.highPerformanceToolStripMenuItem.Click += new System.EventHandler(this.highPerformanceToolStripMenuItem_Click);
            // 
            // balancedToolStripMenuItem
            // 
            this.balancedToolStripMenuItem.Name = "balancedToolStripMenuItem";
            this.balancedToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.balancedToolStripMenuItem.Text = "Balanced";
            this.balancedToolStripMenuItem.Click += new System.EventHandler(this.balancedToolStripMenuItem_Click);
            // 
            // powerSaverToolStripMenuItem
            // 
            this.powerSaverToolStripMenuItem.Name = "powerSaverToolStripMenuItem";
            this.powerSaverToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.powerSaverToolStripMenuItem.Text = "Power Saver";
            this.powerSaverToolStripMenuItem.Click += new System.EventHandler(this.powerSaverToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem highPerformanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem balancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerSaverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

