using Commons;
using Microsoft.Kinect;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using static OpenTK.Graphics.Glu;

namespace Canvas2D.Gesture {
    public class JointVector {
        private Vector3 vector;

        public float X { get => vector.X; set => vector.X = value; }
        public float Y { get => vector.Y; set => vector.Y = value; }
        public float Z { get => vector.Z; set => vector.Z = value; }

        public JointVector(CameraSpacePoint Position) {
            vector = new Vector3(Position.X, Position.Y, Position.Z);
        }

        public JointVector() {
            vector = new Vector3();
        }

        public JointVector(int p1, int p2, int p3) {
            vector = new Vector3(p1, p2, p3);
        }

        /// <summary>
        /// 坐标系转换
        /// </summary>
        public void CoordinateTransform() {
            double temp = vector.X;
            vector.X = vector.Z;
            vector.Z = vector.Y;
            vector.Y = (float)temp;
        }

        public void Draw() {
            GlobalMaterials.SetMaterial(MaterialType.AxisOrgin);
            IntPtr pObj = NewQuadric();
            double _SphereRadius = 0.5;
            GL.Flush();
            GL.PushMatrix();
            GL.Translate(vector.X, vector.Y, vector.Z);
            Sphere(pObj, _SphereRadius, 10, 10);
            GL.PopMatrix();
            pObj = IntPtr.Zero;
        }
    }
}
