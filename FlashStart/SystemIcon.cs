using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashStart
{
    /// <summary>
    /// 系统Icon
    /// 1、Get()  获取指定索引对应的系统icon 
    /// 2、Save() 保存所有系统图像
    /// 3、Show() 显示所有系统Icon图像
    /// </summary>
    public partial class SystemIcon : Form
    {
        public SystemIcon()
        {
            InitializeComponent();

            Show(this);
            //Save();
        }

        /// <summary>
        /// 在form上显示所有系统icon图像
        /// </summary>
        public static void Show(Form form)
        {
            LoadSystemIcon();

            FlowLayoutPanel flowLayout = new FlowLayoutPanel();
            flowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayout.AutoScroll = true;

            for (int i = 0; i < SystemIconList.Count; i++)
            {
                PictureBox pic = new PictureBox();
                pic.Size = new System.Drawing.Size(32, 32);
                flowLayout.Controls.Add(pic);

                Bitmap p = SystemIconList[i].ToBitmap();
                pic.Image = p;
            }
            form.Controls.Add(flowLayout);
        }

        /// <summary>
        /// 保存所有系统图像
        /// </summary>
        public static void Save()
        {
            LoadSystemIcon();

            for (int i = 0; i < SystemIconList.Count; i++)
            {
                Bitmap p = SystemIconList[i].ToBitmap();

                // 保存图像
                string path = AppDomain.CurrentDomain.BaseDirectory + "系统图标\\";
                string filepath = path + (i + ".png");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                if (!File.Exists(filepath)) p.Save(filepath);
            }
        }

        /// <summary>
        /// 获取指定索引对应的系统icon
        /// </summary>
        public static Icon Get(int index)
        {
            LoadSystemIcon();
            return index < SystemIconList.Count ? SystemIconList[index] : null;
        }


        private static List<Icon> SystemIconList = new List<Icon>(); // 记录系统图标

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern bool MessageBeep(uint type);

        [DllImport("Shell32.dll")]
        public extern static int ExtractIconEx(string libName, int iconIndex, IntPtr[] largeIcon, IntPtr[] smallIcon, int nIcons);

        private static IntPtr[] largeIcon;
        private static IntPtr[] smallIcon;

        /// <summary>
        /// 获取所有系统icon图像
        /// </summary>
        private static void LoadSystemIcon()
        {
            if (SystemIconList.Count > 0) return;

            largeIcon = new IntPtr[1000];
            smallIcon = new IntPtr[1000];

            ExtractIconEx("shell32.dll", 0, largeIcon, smallIcon, 1000);

            SystemIconList.Clear();
            for (int i = 0; i < largeIcon.Length; i++)
            {
                try
                {
                    Icon ic = Icon.FromHandle(largeIcon[i]);
                    SystemIconList.Add(ic);
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            //long index;
            //for (int i = 1; i < 200; ++i)
            //{
            //    Icon ic = Helper.SystemIcons.GetFolderIcon(textBox1.Text.Replace(@"\", "\\"), false, out index);
            //    pictureBox1.Image = ic.ToBitmap();
            //}
        }
    }
}
