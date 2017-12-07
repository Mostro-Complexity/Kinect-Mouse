using OpenTK;

namespace Commons
{
    public class clsBox
    {
        public float Xmin, Ymin, Zmin;
        public float XMax, YMax, ZMax;
        public float Wx, Wy, Wz;

        public clsBox(Vector3 vec, int wx, int wy, int wz)
        {//规定盒子的范围
            Xmin = vec.X;
            Ymin = vec.Y;
            Zmin = vec.Z;
            Wx = wx;
            Wy = wy;
            Wz = wz;
            XMax = Xmin + Wx;
            YMax = Ymin + Wy;
            ZMax = Zmin = Wz;
        }

        public clsBox(int xmin, int ymin, int zmin, int wx, int wy, int wz)
        {
            Xmin = xmin;
            Ymin = ymin;
            Zmin = zmin;
            Wx = wx;
            Wy = wy;
            Wz = wz;
            XMax = Xmin + Wx;
            YMax = Ymin + Wy;
            ZMax = Zmin + Wz;
        }

        private clsBox(ref clsBox box)
        {
            Xmin = box.Xmin; ;
            Ymin = box.Ymin;
            Zmin = box.Zmin;
            Wx = box.Wx;
            Wy = box.Wy;
            Wz = box.Wz;
            XMax = Xmin + Wx;
            YMax = Ymin + Wy;
            ZMax = Zmin + Wz;
        }

        public Vector3 Base()
        {
            return new Vector3(Xmin, Ymin, Zmin);
        }

        public void Move(Vector3 offset)
        {
            Xmin += offset.X;
            Ymin += offset.Y;
            Zmin += offset.Z;
        }

        public float CX()
        {
            return Xmin + Wx / 2;
        }

        public float CY()
        {
            return Ymin + Wy / 2;
        }

        public float CZ()
        {
            return Zmin + Wz / 2;
        }

        public Vector3 CP()
        {
            return new Vector3(CX(), CY(), CZ());
        }

        public float Volume()
        {
            return Wx * Wy * Wz;
        }

        public string BaseString()
        {
            return Xmin.ToString("0.0") + "," + Ymin.ToString("0.0") +
                "," + Zmin.ToString("0.0");
        }

        public string SizeString()
        {
            return Wx.ToString("0.0") + "," + Wy.ToString("0.0") +
                "," + Wz.ToString("0.0");
        }

        public override string ToString()
        {
            return BaseString() + "\t" + SizeString();
        }
    }
}
