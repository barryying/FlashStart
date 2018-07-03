using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashStart
{
    public partial class Form_Settings : Form
    {
        private static string isdoubleclick = "false";
        private static string isautobackup = "false";

        //声明一个委托
        public delegate void MyDelegate(string message);
        //声明一个事件
        public event MyDelegate MyEvent;

        public Form_Settings()
        {
            //aaa
            InitializeComponent();
            TabSet();
            x = this.Width;
            y = this.Height;
            setTag(this);

            LoadShortcutKey();
            LoadTabPages();
            LoadIsAutoBackup();
        }

        #region 关闭窗口事件
        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            if (MyEvent != null)
                MyEvent("关闭窗口");//触发事件
            this.Close();
        }
        #endregion

        #region 鼠标拖动无边框窗体

        //定义无边框窗体Form         
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam); public const int WM_SYSCOMMAND = 0x0112; public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private void panel_Title_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);//*********************调用移动无窗体控件函数 
        }

        #endregion

        #region 控件大小随窗体大小等比例变化
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                //根据窗体缩放的比例确定控件的值，宽度
                con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);
                con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }

            }
        }
        private void Form_FlashStart_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }
        #endregion

        #region 重绘tabControl

        /// <summary>
        /// 设定控件绘制模式
        /// </summary>
        private void TabSet()
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.Alignment = TabAlignment.Left;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.Multiline = true;
            tabControl1.ItemSize = new Size(40, 120);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //SolidBrush back = new SolidBrush(Color.FromArgb(45, 45, 48));
            //SolidBrush white = new SolidBrush(Color.FromArgb(122, 193, 255));
            ////Rectangle rec = tabControl1.GetTabRect(0);
            ////e.Graphics.FillRectangle(back, rec);
            ////Rectangle rec1 = tabControl1.GetTabRect(1);
            ////e.Graphics.FillRectangle(back, rec1);
            //StringFormat sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            //sf.Alignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;
            //for (int i = 0; i < tabControl1.TabPages.Count; i++)
            //{
            //    Rectangle rec2 = tabControl1.GetTabRect(i);
            //    e.Graphics.DrawString(tabControl1.TabPages[i].Text, new Font("微软雅黑", 9), white, rec2, sf);
            //}


            string text = ((TabControl)sender).TabPages[e.Index].Text;
            SolidBrush brushblack = new SolidBrush(Color.Black);
            SolidBrush brushwhite = new SolidBrush(Color.FromArgb(122, 193, 255));
            StringFormat sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(text, SystemInformation.MenuFont, brushblack, e.Bounds, sf);
            if (e.Index == this.tabControl1.SelectedIndex)    //当前Tab页的样式
            {
                e.Graphics.DrawString(text, SystemInformation.MenuFont, brushwhite, e.Bounds, sf);
            }
            else    //其余Tab页的样式
            {
                e.Graphics.DrawString(text, SystemInformation.MenuFont, brushblack, e.Bounds, sf);
            }
        }
        #endregion

        #region 快捷键设置
        private void keyDown(object sender, KeyEventArgs e)
        {
            button_restore.ForeColor = Color.Red;
            StringBuilder keyValue = new StringBuilder();
            keyValue.Length = 0;
            keyValue.Append("");
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    keyValue.Append("Ctrl + ");
                if (e.Alt)
                    keyValue.Append("Alt + ");
                if (e.Shift)
                    keyValue.Append("Shift + ");
            }
            if ((e.KeyValue >= 33 && e.KeyValue <= 40) ||
                (e.KeyValue >= 65 && e.KeyValue <= 90) ||   //a-z/A-Z
                (e.KeyValue >= 112 && e.KeyValue <= 123))   //F1-F12
            {
                keyValue.Append(e.KeyCode);
            }
            else if ((e.KeyValue >= 48 && e.KeyValue <= 57))    //0-9
            {
                keyValue.Append(e.KeyCode.ToString().Substring(1));
            }
            this.ActiveControl.Text = "";
            //设置当前活动控件的文本内容
            this.ActiveControl.Text = keyValue.ToString();
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            string str = this.ActiveControl.Text.TrimEnd();
            int len = str.Length;
            if (len >= 1 && str.Substring(str.Length - 1) == "+")
            {
                this.ActiveControl.Text = "";
            }
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            // Ctrl+鼠标左键
            //if (e.Button == MouseButtons.Left)
            //{
            //    this.ActiveControl.Text += " + 鼠标左键";
            //}
            //else if (e.Button == MouseButtons.Right)
            //{
            //    this.ActiveControl.Text += " + 鼠标右键";
            //}
            //else if (e.Button == MouseButtons.Middle)
            //{
            //    this.ActiveControl.Text += " + 鼠标中键";
            //}
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            keyDown(sender, e);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            keyUp(sender, e);
        }
        private void txt_shortcutkey_MouseClick(object sender, MouseEventArgs e)
        {
            mouseClick(sender, e);
        }
        #endregion

        #region 加载所有TabPages
        private void LoadTabPages()
        {
            listView1.Clear();
            DisableEdit();

            //ColumnHeader ch = new ColumnHeader();
            //ch.Text = "选项卡ID";   //设置列标题
            //ch.Width = 120;    //设置列宽度
            //ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式
            //this.listView1.Columns.Add(ch);    //将列头添加到ListView控件。
            this.listView1.Columns.Add("选项卡ID", 140, HorizontalAlignment.Left); //一步添加
            this.listView1.Columns.Add("选项卡名称", 100, HorizontalAlignment.Left); //一步添加
            this.listView1.Columns.Add("选项卡排序", 100, HorizontalAlignment.Left); //一步添加
            this.listView1.View = View.Details;

            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            //从json文件读取数据
            JObject jsonObj = JsonHelper.readJson();
            int count = 0;
            List<string> apptitlelist = new List<string>();
            List<string> apppathlist = new List<string>();
            foreach (JToken child in jsonObj["Settings"]["tabControl_Main"].Children())
            {
                if (child.Count() != 0)
                {
                    count++;
                    string tabpageid = JsonHelper.GetJsonValue(child.Children(), "tabpageid");
                    string tabpagename = JsonHelper.GetJsonValue(child.Children(), "tabpagename");
                    string tabpageindex = JsonHelper.GetJsonValue(child.Children(), "tabpageindex");
                    ListViewItem lvi = new ListViewItem();
                    lvi.ImageIndex = count;     //通过与imageList绑定，显示imageList中第i项图标
                    lvi.Text = tabpageid;
                    lvi.SubItems.Add(tabpagename);
                    lvi.SubItems.Add(tabpageindex);
                    this.listView1.Items.Add(lvi);
                }
            }
            ////测试数据foreach the JObject add the value
            //for (int i = 0; i < 10; i++)   //添加10行数据
            //{
            //    ListViewItem lvi = new ListViewItem();
            //    lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标
            //    lvi.Text = "subitem" + i;
            //    lvi.SubItems.Add("第2列,第" + i + "行");
            //    lvi.SubItems.Add("第3列,第" + i + "行");
            //    this.listView1.Items.Add(lvi);
            //}
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }
        private void LoadShortcutKey()
        {
            JObject jobject = JsonHelper.readJson();
            string shortcutkey = JsonHelper.GetJsonValue(jobject["Settings"].Children(), "shortcutkey");
            txt_shortcutkey.Text = shortcutkey;
            lbl_shortcutkey.Text = shortcutkey;

            isdoubleclick = JsonHelper.GetJsonValue(jobject["Settings"].Children(), "isdoubleclick");
            lbl_isdoubleclick.Text = isdoubleclick;
            if (isdoubleclick == "false")
                radioButton1.Checked = true;
            else
                radioButton2.Checked = true;
        }

        private void LoadIsAutoBackup()
        {
            JObject jobject = JsonHelper.readJson();
            isautobackup = JsonHelper.GetJsonValue(jobject["Settings"].Children(), "isautobackup");
            if (isautobackup == "false")
                checkBox_isautobackup.Checked = false;
            else
                checkBox_isautobackup.Checked = true;
        }
        #endregion

        #region 还原默认设置 按钮点击事件
        private void button_restore_Click(object sender, EventArgs e)
        {
            if (txt_shortcutkey.Text != lbl_shortcutkey.Text)
            {
                txt_shortcutkey.Text = lbl_shortcutkey.Text;
                //JObject jobject = JsonHelper.readJson();
                //if (JsonHelper.SetJsonValue(jobject, "shortcutkey", txt_shortcutkey.Text))
                    button_restore.ForeColor = SystemColors.ControlText;
            }
            else if (isdoubleclick != lbl_isdoubleclick.Text)
            {
                if (lbl_isdoubleclick.Text == "false")
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;

                //JObject jobject = JsonHelper.readJson();
                //if (JsonHelper.SetJsonValue(jobject, "isdoubleclick", isdoubleclick))
                    button_restore.ForeColor = SystemColors.ControlText;
            }
        }
        #endregion

        #region 应用 按钮点击事件
        private void button_apply_Click(object sender, EventArgs e)
        {
            if (txt_shortcutkey.Text != lbl_shortcutkey.Text)
            {
                JObject jobject = JsonHelper.readJson();
                if (JsonHelper.SetShortcutKeyJsonValue(jobject, "shortcutkey", txt_shortcutkey.Text))
                    MessageBox.Show("修改成功！");
            }
            else if(isdoubleclick != lbl_isdoubleclick.Text)
            {
                JObject jobject = JsonHelper.readJson();
                if (JsonHelper.SetShortcutKeyJsonValue(jobject, "isdoubleclick", isdoubleclick))
                    MessageBox.Show("修改成功！");
            }
            this.Close();
        }
        #endregion

        #region 单/双击启动应用 radioButton切换事件
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                isdoubleclick = "false";
            else
                isdoubleclick = "true";
            Console.WriteLine(isdoubleclick);
            if(lbl_isdoubleclick.Text != isdoubleclick)
                button_restore.ForeColor = Color.Red;
            else
                button_restore.ForeColor = SystemColors.ControlText;
        }
        #endregion

        #region listView 行点击事件
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                //选择项的对象即 listView1.SelectedItems[0]
                //然后可以根据选择项的某个属性值更换相应图片
                txt_tabpageid.Text = listView1.SelectedItems[0].Text;
                this.lbl_tabpageid.Text = txt_tabpageid.Text;
                txt_tabpagename.Text = listView1.SelectedItems[0].SubItems[1].Text;
                this.lbl_tabpagename.Text = txt_tabpagename.Text;
                txt_tabpageindex.Text = listView1.SelectedItems[0].SubItems[2].Text;
                this.lbl_tabpageindex.Text = txt_tabpageindex.Text;
            }
        }
        #endregion

        #region 取消 按钮点击事件
        private void Cancel()
        {
            //清空文本框
            txt_tabpageid.Text = "";
            txt_tabpagename.Text = "";
            txt_tabpageindex.Text = "";
            //还原文本框和按钮状态
            txt_tabpageid.Enabled = false;
            txt_tabpagename.Enabled = false;
            txt_tabpageindex.Enabled = false;
            btn_Edit.Text = "编辑";
            btn_Add.Text = "新增";
            btn_Edit.Enabled = true;
            btn_Delete.Enabled = true;
            btn_Delete.Enabled = false;
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion

        #region 编辑 按钮点击事件
        private void EnableEdit()
        {
            //允许编辑
            //txt_tabpageid.Enabled = true;
            txt_tabpagename.Enabled = true;
            txt_tabpageindex.Enabled = true;
            btn_Delete.Enabled = true;
        }
        private void DisableEdit()
        {
            //不允许编辑
            txt_tabpageid.Enabled = false;
            txt_tabpagename.Enabled = false;
            txt_tabpageindex.Enabled = false;
            btn_Delete.Enabled = false;
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (btn_Edit.Text == "编辑")
            {
                if(txt_tabpageid.Text != "")
                {
                    EnableEdit();
                    btn_Edit.Text = "保存";
                }
                else
                {
                    MessageBox.Show("请先点击一条TabPage记录!");
                }
            }
            else
            {
                if (txt_tabpageid.Text != "" &&
                    txt_tabpagename.Text != "" &&
                    txt_tabpageindex.Text != "")
                {
                    if (txt_tabpageid.Text != lbl_tabpageid.Text ||
                    txt_tabpagename.Text != lbl_tabpagename.Text ||
                    txt_tabpageindex.Text != lbl_tabpageindex.Text)
                    {
                        JObject jobject = JsonHelper.readJson();
                        if (JsonHelper.SetTabPageJsonValue(jobject, txt_tabpageid.Text, "tabpagename", txt_tabpagename.Text, 0) &&
                                JsonHelper.SetTabPageJsonValue(jobject, txt_tabpageid.Text, "tabpageindex", txt_tabpageindex.Text, 0))
                        {
                            JsonHelper.UpdateTabPagename("edit", jobject, "apptag", lbl_tabpagename.Text, txt_tabpagename.Text,0);
                            MessageBox.Show("修改成功！");
                        }
                        LoadTabPages();

                        if (MyEvent != null)
                            MyEvent("修改成功");//触发事件
                    }
                    else
                    {
                        MessageBox.Show("数据未更改！");
                        DisableEdit();
                        btn_Edit.Text = "编辑";
                    }
                    Cancel();
                }
            }
        }
        #endregion
        
        #region 删除 按钮点击事件
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除此选项卡会将此选项卡内的快捷方式全部删除，请先确认是否删除此选项卡？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                JObject jobject = JsonHelper.readJson();
                if (JsonHelper.DeleteTabPageJsonValue(jobject, txt_tabpageid.Text, 0))
                {
                    JsonHelper.UpdateTabPagename("delete", jobject, "apptag", lbl_tabpagename.Text, txt_tabpagename.Text, 0);
                    MessageBox.Show("删除成功！");
                    LoadTabPages();
                    if (MyEvent != null)
                        MyEvent("删除成功");//触发事件
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
                Cancel();
            }
        }
        #endregion

        #region 新增 按钮点击事件
        private void EnableAdd()
        {
            //允许编辑
            //txt_tabpageid.Enabled = true;
            txt_tabpagename.Enabled = true;
            //txt_tabpageindex.Enabled = true;
            btn_Add.Text = "保存";
            btn_Edit.Enabled = false;
            btn_Delete.Enabled = false;
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (btn_Add.Text == "新增")
            {
                Cancel();
                EnableAdd();
            }
            else
            {
                if (txt_tabpagename.Text != "")
                {
                    if (JsonHelper.AddTabPageJsonValue(txt_tabpagename.Text))
                    {
                        MessageBox.Show("新增成功！");
                        LoadTabPages();
                        if (MyEvent != null)
                            MyEvent("新增成功");//触发事件
                    }
                    else
                    {
                        MessageBox.Show("新增失败！");
                    }
                    Cancel();
                }
                else
                {
                    MessageBox.Show("选项卡名称不能为空！");
                }
            }
        }
        #endregion

        #region 备份与还原
        private void button_ManualBackup_Click(object sender, EventArgs e)
        {
            Backup();
        }

        private void button_RestoreData_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void Backup()
        {
            try
            {
                string jsonPath = Environment.CurrentDirectory + "\\resourse.json";
                string directionPath = Environment.CurrentDirectory + "\\Backup\\resourse.json";
                FileHelper.CopyFile(jsonPath, directionPath, true);
                MessageBox.Show("备份成功！");
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
            }
        }
        private void Restore()
        {
            try
            {
                string jsonPath = Environment.CurrentDirectory + "\\resourse.json";
                string sourcePath = Environment.CurrentDirectory + "\\Backup\\resourse.json";
                FileHelper.CopyFile(sourcePath, jsonPath, true);
                MessageBox.Show("还原成功！");
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
            }
        }

        private void checkBox_isautobackup_CheckedChanged(object sender, EventArgs e)
        {
            JObject jobject = JsonHelper.readJson();
            if (isautobackup != checkBox_isautobackup.Checked.ToString().ToLower())
            {
                isautobackup = checkBox_isautobackup.Checked.ToString().ToLower();
                if (JsonHelper.SetShortcutKeyJsonValue(jobject, "isautobackup", isautobackup))
                    MessageBox.Show("修改成功！");
            }
        }
        #endregion

    }
}
