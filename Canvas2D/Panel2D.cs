using System.Drawing;
using System.Windows.Forms;
using Commons;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Canvas2D {
    public partial class Panel2D : UserControl {
        public clsOpenGL Opengl;
        public clsVCS3 ViewCS;
        private Vector2 point;
        public delegate void DrawGameHandler();
        public event DrawGameHandler DrawGame;

        public Vector2 MouseLocation { get => point; set => point = value; }

        public Panel2D() {
            InitializeComponent();
            int GameWidth = 100;
            ViewCS = new clsVCS3(new clsBox(-GameWidth / 2, -GameWidth / 2,
                -GameWidth / 2, GameWidth, GameWidth, GameWidth));
            point = new Vector2();
            Opengl = new clsOpenGL();
        }

        public void Draw() {
            Opengl.Init();
            Opengl.SetLight();
            Opengl.Clear(Color.Gray);
            Opengl.SetSCS();
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Flush();
            DrawGame?.Invoke();
            GL.Flush();
            glControl1.SwapBuffers();
        }

        public void SelectbyPoint(Point point, ref Vector3 near,
            ref Vector3 far) {
            int[] viewport = new int[3];
            double[] MvMatrix = new double[15];
            double[] ProjMatrix = new double[15];
            GL.GetInteger(GetPName.Viewport, viewport);
            GL.GetDouble(GetPName.ModelviewMatrix, MvMatrix);
            GL.GetDouble(GetPName.ProjectionMatrix, ProjMatrix);
            int realy = viewport[2] - point.Y - 1;// 左下角为坐标原点
            OpenTK.Graphics.Glu.UnProject(new Vector3(point.X, realy, 0.0f),
                MvMatrix, ProjMatrix, viewport, out near);
            OpenTK.Graphics.Glu.UnProject(new Vector3(point.X, realy, 1.0f),
                MvMatrix, ProjMatrix, viewport, out far);
        }

        private void glCanvas_Load(object sender, System.EventArgs e) {
        }

        private void glCanvas_MouseDown(object sender, MouseEventArgs e) {
            Draw();
        }

        private void glCanvas_MouseUp(object sender, MouseEventArgs e) {
            Draw();
        }

        private void glCanvas_Paint(object sender, PaintEventArgs e) {
            Draw();
        }

        private void glCanvas_MouseMove(object sender, MouseEventArgs e) {
            point.X = e.X;
            point.Y = e.Y;
            Draw();
        }
    }
}
