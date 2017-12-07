using Commons;
using OpenTK.Graphics.OpenGL;
using System;

namespace Canvas2D.Models {
    class TestShape : Frame {
        GlobalMaterials material;
        public override void Draw() {
            throw new NotImplementedException();
        }

        public override void Draw2D() {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GlobalMaterials.SetMaterial(MaterialType.ActiveBox);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex2(11, 11);
            GL.Vertex2(21, 11);
            GL.Vertex2(21, 0);
            GL.Vertex2(-11, 1);
            GL.End();
        }

        public TestShape(GlobalMaterials material) {
            this.material = material;
        }

        public TestShape() {
            material = new GlobalMaterials();
        }
    }
}
