namespace FlashStart
{
    partial class Form_FlashStart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_FlashStart));
            this.panel_Title = new System.Windows.Forms.Panel();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox_Setting = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl_Main = new System.Windows.Forms.TabControl();
            this.tabPage_Default = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lbl_time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel_Title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Setting)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl_Main.SuspendLayout();
            this.tabPage_Default.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Title
            // 
            this.panel_Title.Controls.Add(this.pictureBox_Close);
            this.panel_Title.Controls.Add(this.label1);
            this.panel_Title.Controls.Add(this.pictureBox_Setting);
            this.panel_Title.Location = new System.Drawing.Point(-2, -2);
            this.panel_Title.Name = "panel_Title";
            this.panel_Title.Size = new System.Drawing.Size(386, 56);
            this.panel_Title.TabIndex = 0;
            this.panel_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Title_MouseMove);
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Close.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox_Close.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Close.Image")));
            this.pictureBox_Close.Location = new System.Drawing.Point(340, 7);
            this.pictureBox_Close.Name = "pictureBox_Close";
            this.pictureBox_Close.Size = new System.Drawing.Size(45, 42);
            this.pictureBox_Close.TabIndex = 3;
            this.pictureBox_Close.TabStop = false;
            this.pictureBox_Close.Click += new System.EventHandler(this.pictureBox_Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "闪电启动";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox_Setting
            // 
            this.pictureBox_Setting.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Setting.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox_Setting.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Setting.Image")));
            this.pictureBox_Setting.Location = new System.Drawing.Point(299, 7);
            this.pictureBox_Setting.Name = "pictureBox_Setting";
            this.pictureBox_Setting.Size = new System.Drawing.Size(45, 42);
            this.pictureBox_Setting.TabIndex = 4;
            this.pictureBox_Setting.TabStop = false;
            this.pictureBox_Setting.Click += new System.EventHandler(this.pictureBox_Setting_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "闪电启动";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl_Main);
            this.panel1.Location = new System.Drawing.Point(-2, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 623);
            this.panel1.TabIndex = 1;
            // 
            // tabControl_Main
            // 
            this.tabControl_Main.AllowDrop = true;
            this.tabControl_Main.Controls.Add(this.tabPage_Default);
            this.tabControl_Main.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Main.Multiline = true;
            this.tabControl_Main.Name = "tabControl_Main";
            this.tabControl_Main.SelectedIndex = 0;
            this.tabControl_Main.Size = new System.Drawing.Size(386, 620);
            this.tabControl_Main.TabIndex = 0;
            this.tabControl_Main.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabControl_Main_DragDrop);
            this.tabControl_Main.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabControl_Main_DragEnter);
            this.tabControl_Main.DragOver += new System.Windows.Forms.DragEventHandler(this.tabControl_Main_DragOver);
            this.tabControl_Main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl_Main_MouseDown);
            this.tabControl_Main.MouseLeave += new System.EventHandler(this.tabControl_Main_MouseLeave);
            this.tabControl_Main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl_Main_MouseMove);
            // 
            // tabPage_Default
            // 
            this.tabPage_Default.Controls.Add(this.listView1);
            this.tabPage_Default.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage_Default.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Default.Name = "tabPage_Default";
            this.tabPage_Default.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Default.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage_Default.Size = new System.Drawing.Size(378, 591);
            this.tabPage_Default.TabIndex = 0;
            this.tabPage_Default.Text = "常用";
            this.tabPage_Default.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(372, 585);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(3, 688);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(0, 15);
            this.lbl_time.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form_FlashStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 683);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Title);
            this.Name = "Form_FlashStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.Form_FlashStart_Activated);
            this.Load += new System.EventHandler(this.Form_FlashStart_Load);
            this.SizeChanged += new System.EventHandler(this.Form_FlashStart_SizeChanged);
            this.Leave += new System.EventHandler(this.Form_FlashStart_Leave);
            this.Resize += new System.EventHandler(this.Form_FlashStart_Resize);
            this.panel_Title.ResumeLayout(false);
            this.panel_Title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Setting)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl_Main.ResumeLayout(false);
            this.tabPage_Default.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.PictureBox pictureBox_Setting;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl_Main;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TabPage tabPage_Default;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Timer timer1;
    }
}

