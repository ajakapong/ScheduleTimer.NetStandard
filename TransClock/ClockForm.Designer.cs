namespace TransClock
{
    partial class ClockForm
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
            this.CxtMenu = new System.Windows.Forms.ContextMenu();
            this.itmClose = new System.Windows.Forms.MenuItem();
            this.ItmAlarm = new System.Windows.Forms.MenuItem();
            // 
            // CxtMenu
            // 
            this.CxtMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.itmClose,
                                                                                    this.ItmAlarm});
            // 
            // itmClose
            // 
            this.itmClose.Index = 0;
            this.itmClose.Text = "Close";
            this.itmClose.Click += new System.EventHandler(this.itmClose_Click);
            // 
            // ItmAlarm
            // 
            this.ItmAlarm.Index = 1;
            this.ItmAlarm.Text = "Alarm";
            this.ItmAlarm.Click += new System.EventHandler(this.ItmAlarm_Click);
            // 
            // ClockForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(13, 29);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(178, 40);
            this.ContextMenu = this.CxtMenu;
            this.Font = new System.Drawing.Font("Digital Readout Upright", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClockForm";
            this.Opacity = 0.5;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Transparent Clock";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.WhiteSmoke;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClockForm_MouseDown);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ClockForm_Closing);
            this.Load += new System.EventHandler(this.ClockForm_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ClockForm_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ClockForm_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ClockForm_MouseMove);

        }

        #endregion

        private System.Windows.Forms.ContextMenu CxtMenu;
        private System.Windows.Forms.MenuItem itmClose;
        private System.Windows.Forms.MenuItem ItmAlarm;
    }
}