using System;
using System.Collections.Generic;
using Microsoft.Kinect;
using System.Threading.Tasks;

namespace Canvas3D.Controls {
    /// <summary>
    /// 多个身体
    /// </summary>
    public class clsBodies {
        private List<clsBody> bodies = new List<clsBody>();

        public List<clsBody> BodiesDataQueue { get => bodies; }

        public clsBody GetBody(int i) {
            if (bodies != null && i < bodies.Count && i > -1) {
                return bodies[i];
            }
            return null;
        }

        public void Append(ref clsBody body) {
            bodies.Add(body);
        }

        public void Append(ref IReadOnlyDictionary<JointType, Joint> js) {
            clsBody tempBody = new clsBody();
            foreach (var i in js.Keys) {
                tempBody.Append(js[i]);
            }
            Append(ref tempBody);
        }

        public void Draw() {
            bodies[bodies.Count - 1].Draw();
        }

        public void Draw(int i) {
            if (bodies == null)
                return;
            if (bodies.Count <= i)
                return;
            bodies[i].Draw();
        }

        /// <summary>
        /// 单个身体全身数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<Tuple<clsVector3, clsVector3>> GetSingleBodyFrame(int index) {
            if (bodies != null && index < bodies.Count && index > -1) {
                return bodies[index].Shot();
            }
            return null;//
        }

        public override string ToString() {
            if (bodies.Count != 0) {
                var str = bodies[0].ToString();
                foreach (var i in bodies) {
                    str += i.ToString();
                }
                return str;
            }
            return "";
        }
    }
}
