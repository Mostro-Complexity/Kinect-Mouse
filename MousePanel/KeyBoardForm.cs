using OpenTK;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MousePanel {
    public partial class KeyBoardForm : Form {
        Point wristLocation, tempLocation;
        Controls.MouseControl mouseControl;

        //结构体布局 本机位置
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRECT {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //将枚举作为位域处理
        [Flags]
        enum MouseEventFlag : uint //设置鼠标动作的键值
        {
            Move = 0x0001,               //发生移动
            LeftDown = 0x0002,           //鼠标按下左键
            LeftUp = 0x0004,             //鼠标松开左键
            RightDown = 0x0008,          //鼠标按下右键
            RightUp = 0x0010,            //鼠标松开右键
            MiddleDown = 0x0020,         //鼠标按下中键
            MiddleUp = 0x0040,           //鼠标松开中键
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              //鼠标轮被移动
            VirtualDesk = 0x4000,        //虚拟桌面
            Absolute = 0x8000
        }

        //设置鼠标位置
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); //UIntPtr指针多句柄类型

        //定义变量
        const int AnimationCount = 80;

        public KeyBoardForm() {
            InitializeComponent();
            mouseControl = new Controls.MouseControl();
            mouseControl.DrawGameEvent += Control_DrawGame;
            mouseControl.KinectClickUpEvent += Click_Up;
            mouseControl.KinectClickDownEvent += Click_Down;
            mouseControl.Start();
            wristLocation = new Point(0, 0);
            tempLocation = new Point(0, 0);
        }

        ~KeyBoardForm() {
            mouseControl.Close();
        }

        private void Canvas2D_DrawGame() {
            wristLocation.X = Convert.ToInt32(mouseControl.WristRight.Position.X * 300);
            wristLocation.Y = Convert.ToInt32(mouseControl.WristRight.Position.Y * 300);
            mouse_event(MouseEventFlag.Move, 10 * (wristLocation.X - tempLocation.X),
              -10 * (wristLocation.Y - tempLocation.Y), 0, UIntPtr.Zero);
            tempLocation.X = wristLocation.X;
            tempLocation.Y = wristLocation.Y;
            mouseControl.Draw();
        }

        private void Control_DrawGame() {
            panel2D1.Draw();
            Text = mouseControl.DataInfo;
        }

        private void Click_Down() {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
        }

        private void Click_Up() {
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }
    }
}
