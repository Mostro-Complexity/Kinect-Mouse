using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using static System.Math;

namespace Commons {
    public class clsOpenGL {
        private clsLights Lights;

        public clsOpenGL() {
            Lights = new clsLights();
            //Lights.SetLight();
        }

        public void SetLight() {
            Lights.SetLight();
        }

        public void Init() {
            GL.Enable(EnableCap.DepthTest);// 打开深度比较开关
            GL.DepthFunc(DepthFunction.Less);//z值小者可见，解决多个实体之间的遮挡问题

            GL.Enable(EnableCap.Normalize);
            GL.Enable(EnableCap.CullFace);//剔除与视线方向背离的面，解决单个实体的消隐问题

            GL.Enable(EnableCap.Blend);//色彩混合开关
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha,
                BlendingFactorDest.OneMinusSrcAlpha);//解决透明面的色彩混合
            GL.LineWidth(1);
            GL.Enable(EnableCap.LineSmooth);//  '线平滑
            GL.Enable(EnableCap.PolygonSmooth);

            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.Enable(EnableCap.AutoNormal);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);

            GL.Enable(EnableCap.Lighting);
            GL.ShadeModel(ShadingModel.Smooth);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            SetProjection3();
        }

        public void Clear(Color clrBackground) {
            GL.ClearColor(clrBackground);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.AccumBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
        }

        public void SetProjection3() {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float nearDist = 0.1f, farDist = 10000;
            float fovy = 16 / 180.0f * (float)PI, aspect = 1;
            Matrix4 persp = Matrix4.CreatePerspectiveFieldOfView(
                fovy, aspect, nearDist, farDist);
            GL.LoadMatrix(ref persp);
        }

        public void SetSCS(clsVCS3 SCS) {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            Matrix4 lookat = Matrix4.LookAt(SCS.Vp, SCS.Origin,
               SCS.Up);
            GL.LoadMatrix(ref lookat);
        }

        public void SetSCS() {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            Matrix4 lookat = Matrix4.LookAt(new Vector3(0, 0, 500),
                new Vector3(0, 0, 0),
             new Vector3(0, 1, 0));
            GL.LoadMatrix(ref lookat);
        }
    }
}
