using OpenTK;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyBoard {
    public partial class KeyBoardForm : Form {
        private Canvas2D.Controls.Control2D control;
        private Canvas2D.Models.Point point;
        private Canvas2D.Models.Texture texture;
        Point rightHandLocation, tempLocation;

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
            control = new Canvas2D.Controls.Control2D();
            control.drawGameEventHandler += Control_DrawGame;
            point = new Canvas2D.Models.Point(new Vector2(0, 0));
            texture = new Canvas2D.Models.Texture(@"../../../大软键盘.bmp");
            control.Start();
            Height = texture.ImageHeight;
            Width = texture.ImageWidth;
            rightHandLocation = new Point(0, 0);
            tempLocation = new Point(0, 0);
        }

        ~KeyBoardForm() {
            control.Close();
        }

        private void Canvas2D_DrawGame() {
            point.X = panel2D1.MouseLocation.X - Width / 2 + 8;
            point.Y = -(panel2D1.MouseLocation.Y - Height / 2) - 24;
            point.X /= 4.6f;
            point.Y /= 1.2f;
            Text = string.Concat("(", point.X, ",", point.Y
                , ")窗口宽度", Width, "，窗口长度", Height);
            rightHandLocation.X = Convert.ToInt32(control.RightHand.X);
            rightHandLocation.Y = Convert.ToInt32(control.RightHand.Y);
            Debug.WriteLine(String.Format("{0},{1}", rightHandLocation.X, rightHandLocation.Y));
            mouse_event(MouseEventFlag.Move, 10 * (rightHandLocation.X - tempLocation.X),
              -10 * (rightHandLocation.Y - tempLocation.Y), 0, UIntPtr.Zero);
            tempLocation.X = rightHandLocation.X;
            tempLocation.Y = rightHandLocation.Y;
            control.Track


            texture.Draw2D();
            point.Draw2D();
            control.Draw();
        }

        private void Control_DrawGame() {
            panel2D1.Draw();
            toolStripStatusLabel1.Text = control.DataInfo;
        }
    }
}
