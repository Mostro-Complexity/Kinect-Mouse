using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Canvas2D.Models {
    public abstract class Frame {
        protected Vector3 point;

        virtual public void Draw() {
            IntPtr pObj = OpenTK.Graphics.Glu.NewQuadric();
            double _SphereRadius = 3;
            GL.Flush();
            GL.PushMatrix();
            GL.Translate(point.X, point.Y, point.Z);
            OpenTK.Graphics.Glu.Sphere(pObj, _SphereRadius, 10, 10);
            GL.PopMatrix();
            pObj = IntPtr.Zero;
        }
        abstract public void Draw2D();

        protected Frame() {
            point = new Vector3();
        }

        protected Frame(Vector3 vector3) {
            point = vector3;
        }

        protected Frame(Vector2 vector2) {
            point = new Vector3(vector2.X, vector2.Y, 0);
        }

        protected Frame(float x, float y) {
            point = new Vector3(x, y, 0);
        }


    }
}
