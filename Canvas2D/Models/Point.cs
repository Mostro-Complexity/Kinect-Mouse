using Commons;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Canvas2D.Models {
    public class Point : Frame {

        public float X { get => point.X; set => point.X = value; }
        public float Y { get => point.Y; set => point.Y = value; }
        public float Z { get => point.Z; set => point.Z = value; }

        public Vector3 Location { get => point; set => point = value; }

        public override void Draw2D() {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GlobalMaterials.SetMaterial(MaterialType.AxisY);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex2(X - 3, Y);
            GL.Vertex2(X + 3, Y);
            GL.End();
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex2(X, Y + 10);
            GL.Vertex2(X, Y - 10);
            GL.End();
        }

        public Point(Vector3 p) : base(p) { }

        public Point(Vector2 p) : base(p) { }

        public Point(float x, float y) : base(x, y) { }
    }
}
