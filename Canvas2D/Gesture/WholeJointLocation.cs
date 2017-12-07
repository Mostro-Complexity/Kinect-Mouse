using Microsoft.Kinect;
using System;
using System.Collections.Generic;

namespace Canvas2D.Gesture {
    class WholeJointLocation : NodeLocation {

        List<List<JointVector>> bodys;
        public WholeJointLocation(Body[] bodys) {
            this.bodys = new List<List<JointVector>>();
            foreach (var b in bodys) {
                List<JointVector> sigleBody = new List<JointVector>();
                this.bodys.Add(sigleBody);
                foreach (var i in b.Joints.Values) {
                    sigleBody.Add(new JointVector(i.Position));
                }
            }
        }
        public override void Draw() {
            foreach (var i in bodys[0]) {
                i.CoordinateTransform();
                i.Draw();
            }
        }

        public override void Draw2D() {
            throw new NotImplementedException();
        }
    }
}
