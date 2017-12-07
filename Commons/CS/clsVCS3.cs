using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Commons {
    public class clsVCS3 {
        private float _Azimuth;//方位角azimuth
        private float _Elevation;//仰角elevation
        private float _Radius;//球半径
        private Vector3 _Origin;//坐标原点

        public Vector3 Vp, Up;

        public clsVCS3(clsBox box) {
            _Azimuth = 55;
            _Elevation = 20;
            Resize(box);
            _Radius -= 100;//稍放大
            UpdateVpUp();
        }

        public void Resize(clsBox box) {
            _Radius = box.Wx;
            if (box.Wy > _Radius) _Radius = box.Wy;
            if (box.Wz > _Radius) _Radius = box.Wz;
            _Radius *= 10;
            //设置观察的目标点为包围盒的中心
            _Origin = new Vector3(box.CX(), box.CY(), box.CZ());
            UpdateVpUp();
        }

        public void Rotate(float Azimuth, float Elevation) {
            _Azimuth = Azimuth;
            _Elevation = Elevation;
            UpdateVpUp();
        }

        public void Zoom(float deltaRadius) {
            _Radius += deltaRadius;
            UpdateVpUp();
        }

        public void MoveOrigin(Vector3 d) {
            _Origin.X -= d.X * 0.2f;
            _Origin.Y -= d.Y * 0.2f;
            _Origin.Z -= d.Z * 0.2f;
            UpdateVpUp();
        }

        private void UpdateVpUp() {
            Vp.X = _Origin.X + _Radius * (float)Cos(_Elevation / 180.0f *
                (float)PI) * (float)Cos(_Azimuth / 180.0f * (float)PI);
            Vp.Y = _Origin.Y + _Radius * (float)Cos(_Elevation / 180.0 *
                (float)PI) * (float)Sin(_Azimuth / 180.0 * (float)PI);
            Vp.Z = _Origin.Z + _Radius * (float)Sin(_Elevation
                / 180.0 * (float)PI);
            if (_Elevation > 90 && _Elevation <= 270)
                Up = new Vector3(0, 0, -1);//从下向上看
            else
                Up = new Vector3(0, 0, 1);//从上向下看
        }

        public Vector3 Origin {
            get { return _Origin; }
        }

        public float Radius {
            get { return _Radius; }
        }

        public float Azimuth {
            get { return _Azimuth; }
        }

        public float Elevation {
            get { return _Elevation; }
        }
    }
}
