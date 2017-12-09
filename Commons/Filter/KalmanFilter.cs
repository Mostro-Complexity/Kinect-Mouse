using Microsoft.Kinect;

namespace Commons.Filter {
    /// <summary>
    /// Kalman Filter初步实现
    /// </summary>
    public class KalmanFilter {
        private float K = 0;
        private float X_last = 0, X = 0;
        private float P_last = 0, P = 0;
        float Q, R;

        /// <summary>
        /// 初始化Kalman Filter
        /// </summary>
        /// <param name="Q">Q参数，越小越敏感</param>
        /// <param name="R">R参数，越大越敏感</param>
        public KalmanFilter(float Q, float R) {
            this.Q = Q;
            this.R = R;
            P_last = 1;
        }

        /// <summary>
        /// 初始化Kalman Filter
        /// </summary>
        /// <param name="Q">Q参数，越小越敏感</param>
        /// <param name="R">R参数，越大越敏感</param>
        /// <param name="P">非零浮点数</param>
        public KalmanFilter(float Q, float R, float P) {
            this.Q = Q;
            this.R = R;
            P_last = P;
        }

        public float Fetch(float source) {
            K = P_last / (P_last + R);
            X = X_last + K * (source - X_last);
            P = P_last - K * P_last + Q;
            return X;
        }
        public Joint Fetch(Joint joint) {
            joint.Position.X = Fetch(joint.Position.X);
            joint.Position.Y = Fetch(joint.Position.Y);
            joint.Position.Z = Fetch(joint.Position.Z);
            return joint;
        }
    }
}
