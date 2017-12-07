using OpenTK.Graphics.OpenGL;
using System;
using static OpenTK.Graphics.Glu;

namespace Commons {
    public class clsCS3 {
        private const double SphereRadius = 0.02;//原点半径
        private const double AxisRadius = 0.0025;//轴半径
        private const double AxisHeight = 1;//轴长度
        private const double ArrowHeight = 0.1;//箭头长度
        private const double ArrowRadius = 0.03;//箭头半径 
        private double _SphereRadius;//原点半径
        private double _AxisRadius;//轴半径
        private double _AxisHeight;//轴长度
        private double _ArrowHeight;//箭头长度
        private double _ArrowRadius;//箭头半径 

        public clsCS3(int Length) {
            _SphereRadius = SphereRadius * Length;
            _AxisRadius = AxisRadius * Length;
            _AxisHeight = AxisHeight * Length;
            _ArrowRadius = ArrowRadius * Length;
            _ArrowHeight = ArrowHeight * Length;
        }

        private void DrawAxis1(IntPtr pObj, double AxisRadius,
            double AxisHeight, double ArrowRadius,
            double ArrowHeight) {
            Cylinder(pObj, AxisRadius, AxisRadius, AxisHeight, 10, 1);
            GL.Translate(0, 0, AxisHeight);
            Cylinder(pObj, ArrowRadius, 0, ArrowHeight, 10, 1);
            Disk(pObj, AxisRadius, ArrowRadius, 10, 1);
        }

        public void Draw() {
            IntPtr pObj = NewQuadric();
            //画z轴，带有箭头
            GL.PushMatrix();
            GlobalMaterials.SetMaterial(MaterialType.AxisZ);
            DrawAxis1(pObj, _AxisRadius, _AxisHeight,
                _ArrowRadius, _ArrowHeight);
            GL.PopMatrix();

            //画x轴，带有箭头
            GL.PushMatrix();
            GlobalMaterials.SetMaterial(MaterialType.AxisX);
            GL.Rotate(90, 0, 1, 0);
            DrawAxis1(pObj, _AxisRadius, _AxisHeight,
                _ArrowRadius, _ArrowHeight);
            GL.PopMatrix();

            //画y轴，带有箭头
            GL.PushMatrix();
            GlobalMaterials.SetMaterial(MaterialType.AxisY);
            GL.Rotate(-90, 1, 0, 0);
            DrawAxis1(pObj, _AxisRadius, _AxisHeight,
                _ArrowRadius, _ArrowHeight);
            GL.PopMatrix();

            //白色原点
            GlobalMaterials.SetMaterial(MaterialType.AxisOrgin);
            Sphere(pObj, _SphereRadius, 10, 10);//经向、纬向精度
            pObj = IntPtr.Zero;
        }
    }
}
