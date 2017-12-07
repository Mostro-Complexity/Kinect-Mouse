using System.Drawing;
using System.Windows.Forms;
using Commons;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Canvas3D
{
    public partial class Panel3D : UserControl
    {
        public clsOpenGL Opengl;
        public clsVCS3 ViewCS;
        private clsCS3 CS3;
        private Point sp;
        private bool mousePressed;
        private bool shiftPressed;

        public delegate void DrawGameHandler();
        public event DrawGameHandler DrawGame;
        public delegate void PickHandler(int x, int y);
        public event PickHandler Pick;

        public Panel3D()
        {
            InitializeComponent();
            int GameWidth = 100;
            ViewCS = new clsVCS3(new clsBox(-GameWidth / 2, -GameWidth / 2,
                -GameWidth / 2, GameWidth, GameWidth, GameWidth));
            Opengl = new clsOpenGL();
            CS3 = new clsCS3(GameWidth);//坐标轴
            mousePressed = false; shiftPressed = false;
        }

        public void Draw()
        {
            Opengl.Init();
            Opengl.SetLight();
            Opengl.Clear(Color.Gray);
            Opengl.SetSCS(ViewCS);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            CS3.Draw();
            GL.Flush();
            DrawGame?.Invoke();
            GL.Flush();
            try
            {
                glControl1.SwapBuffers();
            }
            catch
            {

            }
        }

        private void glCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            //右键：选择
            if (e.Button == MouseButtons.Right)
            {
                Pick?.Invoke(e.X, e.Y);//事件触发
                return;
            }
            //左键：平移 或 旋转
            if (shiftPressed)
            {
                mousePressed = true; sp = e.Location;
            }
            else
            {
                mousePressed = true; sp = e.Location;
            }
        }

        private void glCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
            mousePressed = false;
        }

        private void glCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //右键：返回
            if (e.Button == MouseButtons.Right) return;
            if (!mousePressed) return;
            //按住左键：平移 或 旋转
            if (shiftPressed)
            {
                Vector3 near1 = new Vector3(), far1 = new Vector3()
                    , near2 = new Vector3(), far2 = new Vector3();
                SelectbyPoint(e.Location, ref near2, far2);
                SelectbyPoint(sp, ref near1, far1);
                ViewCS.MoveOrigin(far2 - far1);
            }
            else
            {
                ViewCS.Rotate((ViewCS.Azimuth - (e.X - sp.X) + 360)
                    % 360, (ViewCS.Elevation + (e.Y - sp.Y) + 360) % 360);
            }
            Draw();
            sp.X = e.X;
            sp.Y = e.Y;
        }

        private void SelectbyPoint(Point point, ref Vector3 near, Vector3 far)
        {
            int[] viewport = new int[4];
            double[] MvMatrix = new double[16],
                ProjMatrix = new double[16];
            GL.GetInteger(GetPName.Viewport, viewport);
            GL.GetDouble(GetPName.ModelviewMatrix, MvMatrix);
            GL.GetDouble(GetPName.ProjectionMatrix, ProjMatrix);
            int realy = viewport[3] - point.Y - 1;// 左下角为坐标原点
            OpenTK.Graphics.Glu.UnProject(new Vector3(point.X, realy,
                0.0f), MvMatrix, ProjMatrix, viewport, out near);
            OpenTK.Graphics.Glu.UnProject(new Vector3(point.X, realy,
                1.0f), MvMatrix, ProjMatrix, viewport, out far);
        }

        private void glCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            ViewCS.Zoom(e.Delta / 20);
            Draw();
        }

        private void glCanvas_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void glCanvas_SizeChanged(object sender, System.EventArgs e)
        {
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);//定义视区
            Draw();
        }

        private void glCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                shiftPressed = true;
            }
        }

        private void glCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                shiftPressed = false;
            }
        }
    }
}
