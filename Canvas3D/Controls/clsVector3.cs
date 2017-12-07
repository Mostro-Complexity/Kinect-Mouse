using Microsoft.Kinect;
using OpenTK;
using Commons;
using System;
using static OpenTK.Graphics.Glu;
using OpenTK.Graphics.OpenGL;

namespace Canvas3D.Controls {
    public class clsVector3 {
        private Vector3 vec;
        private double scale;

        public clsVector3(CameraSpacePoint Position) {
            vec = new Vector3(Position.X, Position.Y, Position.Z);
            //scale = 10;
            scale = 100;
        }

        public clsVector3() {
            vec = new Vector3();
            //scale = 15;
            scale = 150;
        }

        public clsVector3(int p1, int p2, int p3) {
            vec = new Vector3(p1, p2, p3);
            //scale = 15;
            scale = 150;
        }

        /// <summary>
        /// 坐标系转换
        /// </summary>
        public void CoordinateTransform() {
            double temp = vec.X;
            vec.X = vec.Z;
            vec.Z = vec.Y;
            vec.Y = (float)temp;
        }

        public void Draw() {
            GlobalMaterials.SetMaterial(MaterialType.AxisOrgin);
            IntPtr pObj = NewQuadric();
            double _SphereRadius = 2.5;
            GL.Flush();
            GL.PushMatrix();
            GL.Translate(vec.X * scale, vec.Y * scale, vec.Z * scale);
            Sphere(pObj, _SphereRadius, 10, 10);
            GL.PopMatrix();
            pObj = IntPtr.Zero;
        }
        public float getX() {
            return vec.X;
        }

        public float getY() {
            return vec.Y;
        }

        public float getZ() {
            return vec.Z;
        }
        public Vector3 getVec() {
            return vec;
        }

        public void setX(double x) {
            vec.X = (float)x;
        }
        public void setY(double y) {
            vec.Y = (float)y;
        }
        public void setZ(double z) {
            vec.Z = (float)z;
        }
    }
}
