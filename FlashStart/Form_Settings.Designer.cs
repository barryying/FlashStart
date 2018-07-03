namespace FlashStart
{
    partial class Form_Settings
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
            this.panel_Title = new System.Windows.Forms.Panel();
            this.pictureBox_Close = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.lbl_tabpageindex = new System.Windows.Forms.Label();
            this.lbl_tabpagename = new System.Windows.Forms.Label();
            this.lbl_tabpageid = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.txt_tabpageindex = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_tabpagename = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_tabpageid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbl_isdoubleclick = new System.Windows.Forms.Label();
            this.lbl_shortcutkey = new System.Windows.Forms.Label();
            this.button_apply = new System.Windows.Forms.Button();
            this.button_restore = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_shortcutkey = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button_RestoreData = new System.Windows.Forms.Button();
            this.button_ManualBackup = new System.Windows.Forms.Button();
            this.checkBox_isautobackup = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_Title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Title
            // 
            this.panel_Title.Controls.Add(this.pictureBox_Close);
            this.panel_Title.Controls.Add(this.label1);
            this.panel_Title.Location = new System.Drawing.Point(-2, -2);
            this.panel_Title.Name = "panel_Title";
            this.panel_Title.Size = new System.Drawing.Size(744, 56);
            this.panel_Title.TabIndex = 0;
            this.panel_Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Title_MouseMove);
            // 
            // pictureBox_Close
            // 
            this.pictureBox_Close.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Close.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox_Close.Image = global::FlashStart.Properties.Resources.close;
            this.pictureBox_Close.Location = new System.Drawing.Point(694, 7);
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
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "设置";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "闪电启动";
            this.notifyIcon1.Visible = true;
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
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(40, 100);
            this.tabControl1.Location = new System.Drawing.Point(-2, 60);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(744, 351);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_Add);
            this.tabPage1.Controls.Add(this.btn_Delete);
            this.tabPage1.Controls.Add(this.lbl_tabpageindex);
            this.tabPage1.Controls.Add(this.lbl_tabpagename);
            this.tabPage1.Controls.Add(this.lbl_tabpageid);
            this.tabPage1.Controls.Add(this.btn_Cancel);
            this.tabPage1.Controls.Add(this.btn_Edit);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.txt_tabpageindex);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txt_tabpagename);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txt_tabpageid);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(104, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 343);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "选项卡设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(61, 291);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 13;
            this.btn_Add.Text = "新增";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(152, 291);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Delete.TabIndex = 12;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // lbl_tabpageindex
            // 
            this.lbl_tabpageindex.AutoSize = true;
            this.lbl_tabpageindex.Location = new System.Drawing.Point(173, 204);
            this.lbl_tabpageindex.Name = "lbl_tabpageindex";
            this.lbl_tabpageindex.Size = new System.Drawing.Size(63, 15);
            this.lbl_tabpageindex.TabIndex = 11;
            this.lbl_tabpageindex.Text = "label10";
            this.lbl_tabpageindex.Visible = false;
            // 
            // lbl_tabpagename
            // 
            this.lbl_tabpagename.AutoSize = true;
            this.lbl_tabpagename.Location = new System.Drawing.Point(92, 204);
            this.lbl_tabpagename.Name = "lbl_tabpagename";
            this.lbl_tabpagename.Size = new System.Drawing.Size(55, 15);
            this.lbl_tabpagename.TabIndex = 10;
            this.lbl_tabpagename.Text = "label9";
            this.lbl_tabpagename.Visible = false;
            // 
            // lbl_tabpageid
            // 
            this.lbl_tabpageid.AutoSize = true;
            this.lbl_tabpageid.Location = new System.Drawing.Point(17, 204);
            this.lbl_tabpageid.Name = "lbl_tabpageid";
            this.lbl_tabpageid.Size = new System.Drawing.Size(55, 15);
            this.lbl_tabpageid.TabIndex = 9;
            this.lbl_tabpageid.Text = "label8";
            this.lbl_tabpageid.Visible = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(61, 246);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 8;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.Location = new System.Drawing.Point(152, 246);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(75, 23);
            this.btn_Edit.TabIndex = 7;
            this.btn_Edit.Text = "编辑";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(282, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(344, 328);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // txt_tabpageindex
            // 
            this.txt_tabpageindex.Location = new System.Drawing.Point(111, 161);
            this.txt_tabpageindex.Name = "txt_tabpageindex";
            this.txt_tabpageindex.Size = new System.Drawing.Size(159, 25);
            this.txt_tabpageindex.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "选项卡排序：";
            // 
            // txt_tabpagename
            // 
            this.txt_tabpagename.Location = new System.Drawing.Point(111, 116);
            this.txt_tabpagename.Name = "txt_tabpagename";
            this.txt_tabpagename.Size = new System.Drawing.Size(159, 25);
            this.txt_tabpagename.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "选项卡名称：";
            // 
            // txt_tabpageid
            // 
            this.txt_tabpageid.Location = new System.Drawing.Point(111, 70);
            this.txt_tabpageid.Name = "txt_tabpageid";
            this.txt_tabpageid.Size = new System.Drawing.Size(159, 25);
            this.txt_tabpageid.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "选项卡ID：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbl_isdoubleclick);
            this.tabPage2.Controls.Add(this.lbl_shortcutkey);
            this.tabPage2.Controls.Add(this.button_apply);
            this.tabPage2.Controls.Add(this.button_restore);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txt_shortcutkey);
            this.tabPage2.Location = new System.Drawing.Point(104, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(636, 343);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "快捷键设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbl_isdoubleclick
            // 
            this.lbl_isdoubleclick.AutoSize = true;
            this.lbl_isdoubleclick.Location = new System.Drawing.Point(380, 130);
            this.lbl_isdoubleclick.Name = "lbl_isdoubleclick";
            this.lbl_isdoubleclick.Size = new System.Drawing.Size(0, 15);
            this.lbl_isdoubleclick.TabIndex = 8;
            this.lbl_isdoubleclick.Visible = false;
            // 
            // lbl_shortcutkey
            // 
            this.lbl_shortcutkey.AutoSize = true;
            this.lbl_shortcutkey.Location = new System.Drawing.Point(355, 66);
            this.lbl_shortcutkey.Name = "lbl_shortcutkey";
            this.lbl_shortcutkey.Size = new System.Drawing.Size(0, 15);
            this.lbl_shortcutkey.TabIndex = 7;
            this.lbl_shortcutkey.Visible = false;
            // 
            // button_apply
            // 
            this.button_apply.Location = new System.Drawing.Point(506, 286);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 6;
            this.button_apply.Text = "应用";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // button_restore
            // 
            this.button_restore.Location = new System.Drawing.Point(371, 286);
            this.button_restore.Name = "button_restore";
            this.button_restore.Size = new System.Drawing.Size(115, 23);
            this.button_restore.TabIndex = 5;
            this.button_restore.Text = "还原默认设置";
            this.button_restore.UseVisualStyleBackColor = true;
            this.button_restore.Click += new System.EventHandler(this.button_restore_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(192, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 50);
            this.panel1.TabIndex = 4;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(71, 16);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(58, 19);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "双击";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(58, 19);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "单击";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "单/双击启动应用：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "快捷唤醒软件：";
            // 
            // txt_shortcutkey
            // 
            this.txt_shortcutkey.Location = new System.Drawing.Point(192, 63);
            this.txt_shortcutkey.Name = "txt_shortcutkey";
            this.txt_shortcutkey.Size = new System.Drawing.Size(136, 25);
            this.txt_shortcutkey.TabIndex = 0;
            this.txt_shortcutkey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txt_shortcutkey_MouseClick);
            this.txt_shortcutkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.txt_shortcutkey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button_RestoreData);
            this.tabPage3.Controls.Add(this.button_ManualBackup);
            this.tabPage3.Controls.Add(this.checkBox_isautobackup);
            this.tabPage3.Location = new System.Drawing.Point(104, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(636, 343);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "数据备份与还原";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button_RestoreData
            // 
            this.button_RestoreData.Location = new System.Drawing.Point(310, 178);
            this.button_RestoreData.Name = "button_RestoreData";
            this.button_RestoreData.Size = new System.Drawing.Size(75, 23);
            this.button_RestoreData.TabIndex = 2;
            this.button_RestoreData.Text = "恢复数据";
            this.button_RestoreData.UseVisualStyleBackColor = true;
            this.button_RestoreData.Click += new System.EventHandler(this.button_RestoreData_Click);
            // 
            // button_ManualBackup
            // 
            this.button_ManualBackup.Location = new System.Drawing.Point(143, 178);
            this.button_ManualBackup.Name = "button_ManualBackup";
            this.button_ManualBackup.Size = new System.Drawing.Size(75, 23);
            this.button_ManualBackup.TabIndex = 1;
            this.button_ManualBackup.Text = "手动备份";
            this.button_ManualBackup.UseVisualStyleBackColor = true;
            this.button_ManualBackup.Click += new System.EventHandler(this.button_ManualBackup_Click);
            // 
            // checkBox_isautobackup
            // 
            this.checkBox_isautobackup.AutoSize = true;
            this.checkBox_isautobackup.Location = new System.Drawing.Point(122, 96);
            this.checkBox_isautobackup.Name = "checkBox_isautobackup";
            this.checkBox_isautobackup.Size = new System.Drawing.Size(300, 19);
            this.checkBox_isautobackup.TabIndex = 0;
            this.checkBox_isautobackup.Text = "是否自动备份？（每隔30分钟备份一次）";
            this.checkBox_isautobackup.UseVisualStyleBackColor = true;
            this.checkBox_isautobackup.CheckedChanged += new System.EventHandler(this.checkBox_isautobackup_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Location = new System.Drawing.Point(104, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(636, 343);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "关于";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 15F);
            this.label2.Location = new System.Drawing.Point(41, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(505, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "版权归Barry所有，如需使用......随便造！";
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 410);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel_Title);
            this.Name = "Form_Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel_Title.ResumeLayout(false);
            this.panel_Title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Close)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.PictureBox pictureBox_Close;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_shortcutkey;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.Button button_restore;
        private System.Windows.Forms.Label lbl_shortcutkey;
        private System.Windows.Forms.Label lbl_isdoubleclick;
        private System.Windows.Forms.TextBox txt_tabpageid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_tabpageindex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_tabpagename;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Edit;
        private System.Windows.Forms.Label lbl_tabpageindex;
        private System.Windows.Forms.Label lbl_tabpagename;
        private System.Windows.Forms.Label lbl_tabpageid;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button_RestoreData;
        private System.Windows.Forms.Button button_ManualBackup;
        private System.Windows.Forms.CheckBox checkBox_isautobackup;
    }
}