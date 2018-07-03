using FlashStart.Helper;
using IWshRuntimeLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FlashStart
{
    public partial class Form_FlashStart : Form
    {
        private string currentPanel = "";
        KeyboardHook kh;
        MouseHook mh;
        private string shortcutkey = "";
        private ContextMenuStrip strip = new ContextMenuStrip();
        System.Timers.Timer t = new System.Timers.Timer(1800000); //设置时间间隔为30分钟

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(IntPtr hWnd);
        GetIcoPointName getPn = new GetIcoPointName();
        static class WindowsApi
        {
            [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
            public static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

            [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

            [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
            public static extern void SetForegroundWindow(IntPtr hwnd);

            [DllImport("user32.dll")]
            public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

            public delegate bool CallBack(IntPtr hwnd, int lParam);
        }
        private IntPtr FindWindowEx(IntPtr hwnd, string lpszWindow, bool bChild)
        {
            IntPtr iResult = IntPtr.Zero;
            // 首先在父窗体上查找控件
            iResult = WindowsApi.FindWindowEx(hwnd, 0, null, lpszWindow);
            // 如果找到直接返回控件句柄
            if (iResult != IntPtr.Zero) return iResult;

            // 如果设定了不在子窗体中查找
            if (!bChild) return iResult;

            // 枚举子窗体，查找控件句柄
            int i = WindowsApi.EnumChildWindows(
            hwnd,
            (h, l) =>
            {
                IntPtr f1 = WindowsApi.FindWindowEx(h, 0, null, lpszWindow);
                if (f1 == IntPtr.Zero)
                    return true;
                else
                {
                    iResult = f1;
                    return false;
                }
            },
            0);
            // 返回查找结果
            return iResult;
        }
        public Form_FlashStart()
        {
            InitializeComponent();

            x = this.Width;
            y = this.Height;
            setTag(this);

            Initializenotifyicon();
            //LoadImageList("");
            this.AllowDrop = true;

            //json
            string defaultcontent = "{\"AppListModel\": [{}]}";
            JsonHelper.CreateJson(defaultcontent);

            //string jsonData = "{\"name\":\"lily\",\"age\":23,\"addr\":{\"city\":guangzhou,\"province\":guangdong}}";
            //string jsonData = "{\"AppListModel\": [{\"AppInfo\": {\"apptag\": \"常用\",\"apptitle\": \"BaiduNetdisk\",\"appiconpath\": \"C:/Users/YT/AppData/Roaming/baidu/BaiduNetdisk/BaiduNetdisk.exe\"}}, {\"AppInfo\": {\"apptag\": \"常用\",\"apptitle\": \"BaiduNetdisk\",\"appiconpath\": \"C:/Users/YT/AppData/Roaming/baidu/BaiduNetdisk/BaiduNetdisk.exe\"}}]}";
            LoadImageList();
            //AppListModel appinfomodel = (AppListModel)JsonConvert.DeserializeObject(jsonData, typeof(AppListModel));

            LoadTabControl_Main();

            LoadTime();

            //创建钩子
            kh = new KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += kh_OnKeyDownEvent;
            mh = new MouseHook();
            mh.Start();
            mh.OnMouseActivity += mh_OnMouseClickEvent; 

        }

        #region 自动备份json文件
        private void Form_FlashStart_Load(object sender, EventArgs e)
        {
            t.Elapsed += new System.Timers.ElapsedEventHandler(AutoBackup);
            //t.AutoReset = false; //每到指定时间Elapsed事件是触发一次（false），还是一直触发（true） 
            t.Enabled = true; //是否触发Elapsed事件
            t.Start();
        }
        private void AutoBackup(object sender, System.Timers.ElapsedEventArgs elap)
        {
            try
            {
                JObject jobject = JsonHelper.readJson();
                string isautobackup = JsonHelper.GetJsonValue(jobject["Settings"].Children(), "isautobackup");
                if (isautobackup == "true")
                {
                    string jsonPath = Environment.CurrentDirectory + "\\resourse.json";
                    string directionPath = Environment.CurrentDirectory + "\\Backup\\resourse.json";
                    FileHelper.CopyFile(jsonPath, directionPath, true);
                    Console.WriteLine("备份成功！");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("e.Message： " + e.Message + "；e.StackTrace： " + e.StackTrace);
            }
        }
        #endregion

        #region 设置快捷键唤出窗口
        private void SetShortcutKey()
        {
            JObject jobject = JsonHelper.readJson();
            shortcutkey = JsonHelper.GetJsonValue(jobject["Settings"].Children(), "shortcutkey");
        }
        private void IsFormMini()
        {
            if (this.Visible)  //判断是否最小化
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
                this.Hide();
            }
            else
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
            }
        }
        void kh_OnKeyDownEvent(object sender, KeyEventArgs e)
        {
            //获取快捷键
            SetShortcutKey();
            //e = HotKey.GetKeyByString("");
            //if (e.KeyData == (Keys.Q | Keys.Alt))
            if (e.KeyData == HotKey.GetKeyByString(shortcutkey).KeyData)                
            {
                IsFormMini();
            }//Ctrl+S显示窗口
            //if (e.KeyData == (Keys.H | Keys.Control)) { this.Hide(); }//Ctrl+H隐藏窗口
            if (e.KeyData == (Keys.C | Keys.Alt)) { this.Close(); }//Ctrl+C 关闭窗口 
            //if (e.KeyData == (Keys.A | Keys.Control | Keys.Alt)) { this.Text = "你发现了什么？"; }//Ctrl+Alt+A
        }
        void mh_OnMouseClickEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                IsFormMini();
            }
        }
        #endregion

        #region 动态添加tabPages
        private void LoadTabControl_Main()
        {
            tabControl_Main.TabPages.Clear();
            //foreach the JObject add the value
            JObject jobject = JsonHelper.readJson();
            foreach (JToken child in jobject["Settings"]["tabControl_Main"].Children())
            {
                if (child.Count() != 0)
                {
                    string tabpageid = JsonHelper.GetJsonValue(child.Children(), "tabpageid");
                    string tabpagename = JsonHelper.GetJsonValue(child.Children(), "tabpagename");
                    string tabpageindex = JsonHelper.GetJsonValue(child.Children(), "tabpageindex");
                    //if(tabpageid != "tabPage_default")
                    //{
                        //tabControl_Main.TabPages.Insert(int.Parse(tabpageindex), tabpagename);
                        TabPage Page = new TabPage();
                        Page.Name = tabpageid;
                        Page.Text = tabpagename;
                        Page.TabIndex = int.Parse(tabpageindex);
                        Page.Tag = "386;620;0;0;9";
                        this.tabControl_Main.Controls.Add(Page);
                    //}
                }
            }
            MatchTabPage(tabControl_Main.SelectedTab.Text);
            listView1.MouseClick += new MouseEventHandler(listView1MouseClick);
            strip.ItemClicked += new ToolStripItemClickedEventHandler(stripItemClicked);
        }
        /// <summary>
        /// TabPages右键点击弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //绑定前先清空菜单
                strip.Items.Clear();
                //绑定tabPage右键菜单
                strip.Items.Add("移动到");
                strip.Items.Add("删除此快捷方式");
                //绑定"移动到"的子级菜单，即从json获取所有tabPage名称
                JObject jobject = JsonHelper.readJson();
                foreach (JToken child in jobject["Settings"]["tabControl_Main"].Children())
                {
                    if (child.Count() != 0)
                    {
                        string tabpagename = JsonHelper.GetJsonValue(child.Children(), "tabpagename");
                        if (tabpagename != tabControl_Main.SelectedTab.Text)
                        {
                            ToolStripItem stripitem = new ToolStripMenuItem();
                            stripitem.Name = tabpagename;
                            stripitem.Text = tabpagename;
                            stripitem.Click += new EventHandler(itemClicked);
                            /*只能通过ContextMenuStrip的Item的索引为其添加子菜单，通过Item的Text属性会报错*/
                            ((ToolStripDropDownItem)(strip.Items[0])).DropDownItems.Add(stripitem);
                        }
                    }
                }
                strip.Show(listView1, e.Location);//鼠标右键按下弹出菜单
            }
        }
        /// <summary>
        /// "删除此快捷方式"点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除此快捷方式")
            {
                Console.WriteLine(e.ClickedItem.Text + " : " + listView1.SelectedItems[0].Text);

                JObject jobject = JsonHelper.readJson();
                JsonHelper.DeleteListviewJson(jobject, listView1.SelectedItems[0].Text, 0);
                LoadImageList();
            }
        }
        /// <summary>
        /// "移动到"子级菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemClicked(object sender, EventArgs e)
        {
            JObject jobject = JsonHelper.readJson();
            ToolStripItem item = (ToolStripItem)sender;
            Console.WriteLine(item.Text + " : " + listView1.SelectedItems[0].Text);
            JsonHelper.SetListviewJson(jobject, listView1.SelectedItems[0].Text, "apptag", item.Text, 0);
            LoadImageList();
        }
        #endregion

        #region 最小化到任务栏

        private ContextMenu notifyiconMnu;
        /// <summary>
        /// 最小化到任务栏
        /// </summary>
        private void Initializenotifyicon()
        {
            //定义一个MenuItem数组，并把此数组同时赋值给ContextMenu对象 
            MenuItem[] mnuItms = new MenuItem[3];
            mnuItms[0] = new MenuItem();
            mnuItms[0].Text = "显示窗口";
            mnuItms[0].Click += new System.EventHandler(this.notifyIcon1_showfrom);

            mnuItms[1] = new MenuItem("-");

            mnuItms[2] = new MenuItem();
            mnuItms[2].Text = "退出系统";
            mnuItms[2].Click += new System.EventHandler(this.ExitSelect);
            mnuItms[2].DefaultItem = true;

            notifyiconMnu = new ContextMenu(mnuItms);
            notifyIcon1.ContextMenu = notifyiconMnu;
            //为托盘程序加入设定好的ContextMenu对象 
        }

        private void Form_FlashStart_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
                this.Hide();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.ShowInTaskbar = true;  //显示在系统任务栏
                this.WindowState = FormWindowState.Normal;  //还原窗体
                //notifyIcon1.Visible = false;  //托盘图标隐藏
            }
        }

        public void notifyIcon1_showfrom(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                //notifyIcon1.Visible = false;
            }
        }

        public void ExitSelect(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //隐藏托盘程序中的图标 
                notifyIcon1.Visible = false;
                //卸载钩子
                kh.UnHook();
                //关闭系统 
                this.Close();
                this.Dispose(true);
            }
        }
        #endregion

        #region Form_Settings关闭窗口事件 和 有更新后通过委托刷新主窗体
        private void pictureBox_Setting_Click(object sender, EventArgs e)
        {
            Form_Settings frm_settings = new Form_Settings();
            frm_settings.MyEvent += new Form_Settings.MyDelegate(b_MyEvent);//监听b窗体事件
            frm_settings.ShowDialog();
        }
        void b_MyEvent(string message)
        {
            LoadTabControl_Main();
            LoadImageList();
        }
        #endregion

        #region 关闭窗口事件
        private void pictureBox_Close_Click(object sender, EventArgs e)
        {
            #region 没用的
            //foreach (var i in getPn.GetIcoName())
            //{
            //    Console.WriteLine(i);
            //}
            ////ShwoDesk();
            //Console.WriteLine(getPn.GetIcoPoint("此电脑").ToString());
            //Console.WriteLine(getPn.GetIcoPoint("回收站").ToString());

            // 查找主界面句柄
            //IntPtr mainHandle = WindowsApi.FindWindow(null, "此电脑");
            //if (mainHandle != IntPtr.Zero)
            //{
            //    // 查找按钮句柄
            //    IntPtr iBt = FindWindowEx(mainHandle, "此电脑", true);
            //    if (iBt != IntPtr.Zero)
            //    {
            //        // 发送单击消息
            //        WindowsApi.SendMessage(iBt, 0xF5, 0, 0);
            //        WindowsApi.SendMessage(iBt, 0x1000 + 16, 0, 0);
            //        WindowsApi.SetForegroundWindow(iBt);

            //    }
            //}
            #endregion

            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region 显示桌面
        /// <summary>  
        /// 显示桌面  
        /// </summary>  
        private void ShwoDesk()
        {
            Type shellType = Type.GetTypeFromProgID("Shell.Application");
            object shellObject = System.Activator.CreateInstance(shellType);
            shellType.InvokeMember("ToggleDesktop", System.Reflection.BindingFlags.InvokeMethod,
    null, shellObject, null);
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

        #region 显示快捷方式列表

        //private void LoadImageList(string filePath)
        //{
        //    ////for test
        //    //var path = Environment.CurrentDirectory + "\\Images\\";
        //    //var list = new List<string>();
        //    //list.Add("close.ico");
        //    //list.Add("logo2.ico");
        //    //list.Add("settings2.ico");

        //    //ImageList imageList1 = new ImageList();
        //    //imageList1.ImageSize = new Size(48, 48);
        //    //imageList1.ColorDepth = ColorDepth.Depth32Bit;
        //    //foreach (var fileName in list)
        //    //{
        //    //    imageList1.Images.Add(Image.FromFile(path + fileName));
        //    //}

        //    //filePath = GetFilePath(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Notepad++\Notepad++.lnk");

        //    filePath = GetFilePath(filePath);
        //    if (filePath != "")
        //    {
        //        ImageList imageList1 = new ImageList();
        //        imageList1.ImageSize = new Size(48, 48);
        //        imageList1.ColorDepth = ColorDepth.Depth32Bit;
        //        imageList1.Images.Add(Icon.ExtractAssociatedIcon(filePath));
        //        listView1.LargeImageList = imageList1;

        //        for (int i = 0; i < imageList1.Images.Count; i++)
        //        {
        //            var lvi = new ListViewItem();
        //            lvi.ImageIndex = i;
        //            lvi.Text = Path.GetFileNameWithoutExtension(filePath);//"P" + i;
        //            lvi.ToolTipText = Path.GetFileNameWithoutExtension(filePath);//"P" + i;
        //            listView1.Items.Add(lvi);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("该目录不存在！");
        //    }
        //}
        private void LoadImageList()
        {
            imageList1.Images.Clear();
            listView1.Clear();
            MatchTabPage(tabControl_Main.SelectedTab.Text);
            //foreach the JObject add the value
            JObject jobject = JsonHelper.readJson();
            int count = 0;
            List<string> apptitlelist = new List<string>();
            List<string> apppathlist = new List<string>();
            foreach (JToken child in jobject["AppListModel"].Children())
            {
                if(child.Count() != 0)
                {
                    if(tabControl_Main.SelectedTab.Text.Equals(JsonHelper.GetJsonValue(child.Children(), "apptag")))
                    {
                        count++;
                        string apptitle = JsonHelper.GetJsonValue(child.Children(), "apptitle");
                        apptitlelist.Add(JsonHelper.GetJsonValue(child.Children(), "apptitle"));
                        string apppath = JsonHelper.GetJsonValue(child.Children(), "appiconpath").Replace("/", "\\");
                        apppathlist.Add(apppath);
                        string appiconpath = apppath;
                        if (JsonHelper.GetJsonValue(child.Children(), "apptype").Equals(".lnk"))
                        {
                            appiconpath = GetFilePath(apppath);
                        }
                        if (appiconpath != "")
                        {
                            //ImageList imageList1 = new ImageList();
                            imageList1.ImageSize = new Size(48, 48);
                            imageList1.ColorDepth = ColorDepth.Depth32Bit;
                            //imageList1.Images.Add(Icon.ExtractAssociatedIcon(appiconpath));
                            imageList1.Images.Add(GetIcon(appiconpath));

                        }
                        else
                        {
                            MessageBox.Show("该目录不存在！");
                        }
                    }
                }
            }
            listView1.LargeImageList = imageList1;

            for (int i = 0; i < imageList1.Images.Count; i++)
            {
                var lvi = new ListViewItem();
                lvi.ImageIndex = i;
                lvi.Text = apptitlelist[i];//Path.GetFileNameWithoutExtension(filePath);//"P" + i;
                lvi.ToolTipText = apppathlist[i];//Path.GetFileNameWithoutExtension(filePath);//"P" + i;
                listView1.Items.Add(lvi);
            }
        }
        #endregion

        #region 获取快捷方式启动路径
        /// <summary>  
        /// 获取快捷方式启动路径  
        /// </summary>  
        /// <param name="lnkPath">快捷方式路径</param>  
        /// <returns>启动路径</returns>  
        private string GetFilePath(string lnkPath)
        {
            //lnkPath = @"d:\Test.lnk";
            if (lnkPath != "" && System.IO.File.Exists(lnkPath))
            {
                WshShell shell = new WshShell();
                IWshShortcut iWshShortcut = (IWshShortcut)shell.CreateShortcut(lnkPath);
                //快捷方式文件指向的路径.Text = 当前快捷方式文件IWshShortcut类.TargetPath;  
                //快捷方式文件指向的目标目录.Text = 当前快捷方式文件IWshShortcut类.WorkingDirectory;
                if (iWshShortcut.TargetPath == "")
                {
                    return "nofile";
                }
                else if(System.IO.File.Exists(iWshShortcut.TargetPath))
                {
                    return iWshShortcut.TargetPath;
                }
                else
                {
                    return iWshShortcut.TargetPath.Replace(" (x86)", "");
                }
            }
            else
            {
                return "nofile";
            }
        }

        #endregion

        #region 判断文件格式
        /// <summary>
        /// 判断文件格式
        /// http://www.cnblogs.com/babycool 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsAllowedExtension(string filePath)
        {
            /*文件扩展名说明
             * 255216 jpg
             * 208207 doc xls ppt wps
             * 8075 docx pptx xlsx zip
             * 5150 txt
             * 8297 rar
             * 7790 exe
             * 3780 pdf      
             * 
             * 4946/104116 txt
             * 7173        gif 
             * 255216      jpg
             * 13780       png
             * 6677        bmp
             * 239187      txt,aspx,asp,sql
             * 208207      xls.doc.ppt
             * 6063        xml
             * 6033        htm,html
             * 4742        js
             * 8075        xlsx,zip,pptx,mmap,zip
             * 8297        rar   
             * 01          accdb,mdb
             * 7790        exe,dll
             * 5666        psd 
             * 255254      rdp 
             * 10056       bt种子 
             * 64101       bat 
             * 4059        sgf    
             */
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            string fileclass = "";
            // byte buffer;
            try
            {

                //buffer = reader.ReadByte();
                //fileclass = buffer.ToString();
                //buffer = reader.ReadByte();
                //fileclass += buffer.ToString();

                for (int i = 0; i < 2; i++)
                {
                    fileclass += reader.ReadByte().ToString();
                }

            }
            catch (Exception)
            {

                throw;
            }

            if (fileclass == "255216")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 拖动快捷方式到窗体内
        // 1：DragEnter是你拖动后首次在进入某个控件内发生。
        // 2：DragOver发生在DragEnter之后，当你移动拖动对象（鼠标）时发生，类似于MouseMove。
        // 3：DragDrop当你松开鼠标时发生。
        private void tabControl_Main_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("DragEnter是你拖动后首次在进入某个控件内发生");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Count() != 0)
                {
                    foreach (string file in files)
                    {
                        //路径字符串长度不为空
                        if (file.Length > 1)
                        {
                            if (!IsExist(file))
                            {
                                //判断是文件夹吗
                                FileInfo fil = new FileInfo(file);
                                if (fil.Attributes == FileAttributes.Directory)//文件夹
                                {
                                    JsonHelper.addListviewJson(tabControl_Main.SelectedTab.Text, "dir", file);
                                    //鼠标图标链接
                                    //e.Effect = DragDropEffects.Link;
                                }
                                else
                                {
                                    if (file.LastIndexOf(".lnk") > 0)  //快捷方式
                                    {
                                        string appiconpath = GetFilePath(file);
                                        //JsonHelper.addListviewJson(tabControl_Main.SelectedTab.Text, file.Split('.')[1], file);
                                        if(appiconpath != "nofile")
                                            JsonHelper.addListviewJson(tabControl_Main.SelectedTab.Text, Path.GetExtension(file), file);
                                    }
                                    else     //文件
                                    {
                                        JsonHelper.addListviewJson(tabControl_Main.SelectedTab.Text, "file", file);
                                    }
                                    //鼠标图标禁止
                                    //e.Effect = DragDropEffects.None;
                                }
                            }
                            else
                            {
                                Console.WriteLine("此图标已经存在！");
                                //MessageBox.Show("此图标已经存在！");
                            }
                        }
                        //if (!IsExist(file))
                        //{
                        //    //JsonHelper.addJson(file, tabControl_Main.SelectedTab.Text);
                        //}
                        //else
                        //{
                        //    MessageBox.Show("此图标已经存在！");
                        //}
                    }
                    LoadImageList();
                }
            }
        }
        private void tabControl_Main_DragOver(object sender, DragEventArgs e)
        {
            Console.WriteLine("DragOver发生在DragEnter之后，当你移动拖动对象（鼠标）时发生，类似于MouseMove。");
        }

        private void tabControl_Main_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("DragDrop当你松开鼠标时发生");
        }

        #endregion

        #region 判断拖进来的快捷方式是否存在
        /// <summary>
        /// 判断拖进来快捷方式是否存在
        /// </summary>
        /// <param name="file">拖进来的快捷方式路径</param>
        /// <returns></returns>
        private bool IsExist(string file)
        {
            bool flag = false;
            JObject jobject = JsonHelper.readJson();
            var apppaths = from applistmodel in jobject["AppListModel"].Children().Children().Children()
                           select applistmodel["appiconpath"];
            foreach (var apppath in apppaths)
            {
                Console.WriteLine(apppath);
                if (file.Equals(apppath.ToString().Replace("/","\\")))
                    return true;
                else
                    flag = false;
            }

            //foreach (JToken child in jsonObj["AppListModel"].Children())
            //{
            //    if (child.Count() != 0)
            //    {
            //        if (file.Equals(JsonHelper.GetJsonValue(child.Children(), "appiconpath")))
            //            return true;
            //        else
            //            flag = false;
            //    }
            //}
            //foreach (ListViewItem item in listView1.Items)   //需要对所有tabpage里的图标及进行循环
            //{
            //    if (item.ToolTipText.Equals(file))
            //        return true;
            //    else
            //        flag = false;
            //}
            return flag;
        }
        #endregion

        #region 点击图标 启动应用程序
        private void listView1_Click(object sender, EventArgs e)
        {
            JObject jobject = JsonHelper.readJson();
            if (JsonHelper.GetJsonValue(jobject["Settings"].Children(), "isdoubleclick") == "false")
            {
                if (this.listView1.SelectedItems.Count == 0)
                    return;

                //前提，listview禁止多选
                ListViewItem currentRow = listView1.SelectedItems[0];
                if (currentRow.ToolTipText.LastIndexOf(".lnk") > 0)
                    System.Diagnostics.Process.Start(GetFilePath(currentRow.ToolTipText));
                else
                    System.Diagnostics.Process.Start(currentRow.ToolTipText);
            }   
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            JObject jobject = JsonHelper.readJson();
            if (JsonHelper.GetJsonValue(jobject["Settings"].Children(), "isdoubleclick") == "true")
            {
                if (this.listView1.SelectedItems.Count == 0)
                    return;

                //前提，listview禁止多选
                ListViewItem currentRow = listView1.SelectedItems[0];
                if (currentRow.ToolTipText.LastIndexOf(".lnk") > 0)
                    System.Diagnostics.Process.Start(GetFilePath(currentRow.ToolTipText));
                else
                    System.Diagnostics.Process.Start(currentRow.ToolTipText);
            }
        }
        #endregion

        #region 鼠标停放在tabPage选项卡上 切换选项卡
        /// <summary>
        /// 鼠标停放在tabPage上 切换选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_Main_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
            for (int i = 0; i < tabControl_Main.TabCount; i++)
            {
                if (tabControl_Main.GetTabRect(i).IntersectsWith(mouseRect))
                {
                    tabControl_Main.SelectedIndex = i;
                    currentPanel = tabControl_Main.SelectedTab.Name;

                    //tabControl_Main.SelectedTab.Controls.Add(listView1);
                    LoadImageList();
                    //MessageBox.Show(currentPanel);
                    break;
                }
            }
        }

        private void MatchTabPage(string tabpageName)
        {
            foreach (TabPage tabpage in tabControl_Main.TabPages)
            {
                if (tabpageName.Equals(tabpage.Text))
                {
                    tabControl_Main.SelectedTab = tabpage;
                    tabControl_Main.SelectedTab.Controls.Add(listView1);
                }
                //foreach (Control control in page.Controls)
                //{
                //    if (control is TextBox)
                //    {
                //        ((TextBox)control) = "";
                //    }
                //    if (control is ComboBox)
                //    {
                //        ((ComboBox)control).SelectedIndex = -1;
                //    }
                //}
            }
        }
        #endregion

        #region 右键选项卡 弹出菜单
        /// <summary>
        /// 右键选项卡 弹出菜单
        /// </summary>
        /// <param name="Control">加载到哪个控件上</param>
        /// <param name="itemstr">选项卡名称</param>
        private void LoadMenustrip(Control Control, string itemstr)
        {
            ContextMenuStrip UserMenu = new ContextMenuStrip();
            UserMenu.Items.Add(itemstr);
            UserMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenuStrip1_ItemClicked);
            Control.ContextMenuStrip = UserMenu;
        }

        private void tabControl_Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabControl_Main.TabPages.Count; i++)
                {
                    TabPage tp = tabControl_Main.TabPages[i];
                    if (tabControl_Main.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        tabControl_Main.SelectedTab = tp;
                        break;
                    }
                }
                LoadMenustrip(tabControl_Main, "右侧添加一个选项卡(Ctrl+C)");
                //this.tabControl_Main.ContextMenuStrip = this.UserMenu;  //弹出菜单
            }
        }
        private void tabControl_Main_MouseLeave(object sender, EventArgs e)
        {
            this.tabControl_Main.ContextMenuStrip = null;  //离开选项卡后 取消菜单
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Console.WriteLine(e.ClickedItem.Text);
            tabControl_Main.TabPages.Insert(tabControl_Main.SelectedIndex + 1, "NewTabPage");
        }
        #endregion

        #region 通过路径获取图标
        ///   <summary>  
        ///  通过路径获取图标 
        ///   </summary>  
        ///   <param name="path"> 文件或文件夹路径 </param>  
        ///   <returns> 获取的图标 </returns>   
        public static Icon GetIcon(string path)
        {
            FileHelper.FileInfomation _info = new FileHelper.FileInfomation();

            FileHelper.GetFileInfo(path, 0, ref _info, Marshal.SizeOf(_info),
            (int)(FileHelper.GetFileInfoFlags.SHGFI_ICON | FileHelper.GetFileInfoFlags.SHGFI_LARGEICON)); //SHGFI_LARGEICON 大图标
            try
            {
                return Icon.FromHandle(_info.hIcon);
            }
            catch
            {
                return null;
            }
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
                if(con.Tag != null)
                {
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
        }
        private void Form_FlashStart_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
        }

        #endregion
        

        #region ////////未使用  当前窗口才有效的监听快捷键
        private void Form_FlashStart_Activated(object sender, EventArgs e)
        {
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。  
            //HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Ctrl, Keys.Q);
        }

        private void Form_FlashStart_Leave(object sender, EventArgs e)
        {
            //注销Id号为100的热键设定  
            //HotKey.UnregisterHotKey(Handle, 100);
        }

        ///   
        /// 监视Windows消息  
        /// 重载WndProc方法，用于实现热键响应  
        ///   
        ///   
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键   
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:    //Ctrl+Q  
                            Console.WriteLine("Ctrl+Q");         //此处填写快捷键响应代码           
                            break;
                        case 101:    //按下的是Ctrl+B  
                                     //此处填写快捷键响应代码  
                            break;
                        case 102:    //按下的是Alt+D  
                                     //此处填写快捷键响应代码  
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region 显示已开机时长和当前时间
        private void LoadTime()
        {
            timer1.Start();
            //Console.WriteLine(Environment.TickCount + "---" + formatLongToTimeStr(Environment.TickCount));
            //lbl_time.Text = "开机时长: " + formatLongToTimeStr(Environment.TickCount);
        }
        public static string formatLongToTimeStr(int tickcount)
        {
            string str = "";
            int hour = 0;
            int minute = 0;
            int second = 0;
            second = tickcount / 1000;

            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            str = hour.ToString() + "小时" + minute.ToString() + "分钟"
                + second.ToString() + "秒";
            return str;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_time.Text = "已开机" + formatLongToTimeStr(Environment.TickCount) + ",现在是" + DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss");
        }
        #endregion

        
    }

}
