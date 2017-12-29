using System.Windows.Forms;

namespace MousePanel {
    public partial class MonitoringForm : Form {
        GestureCatcher.GestureControl gestureControl;

        public MonitoringForm() {
            InitializeComponent();
            //mouseControl = new Controls.MouseControl();
            //mouseControl.DrawGameEvent += Control_DrawGame;
            //mouseControl.Start();
            //wristLocation = new Point(0, 0);
            //tempLocation = new Point(0, 0);
            gestureControl = new GestureCatcher.GestureControl();
        }

        ~MonitoringForm() {
            //mouseControl.Close();
        }

        private void Canvas2D_DrawGame() {
            //wristLocation.X = Convert.ToInt32(mouseControl.WristRight.Position.X * 300);
            //wristLocation.Y = Convert.ToInt32(mouseControl.WristRight.Position.Y * 300);
            //mouse_event(MouseEventFlag.Move, 10 * (wristLocation.X - tempLocation.X),
            //  -10 * (wristLocation.Y - tempLocation.Y), 0, UIntPtr.Zero);
            //tempLocation.X = wristLocation.X;
            //tempLocation.Y = wristLocation.Y;
            //mouseControl.Draw();
        }

        private void Control_DrawGame() {
            //panel2D1.Draw();
            //Text = mouseControl.DataInfo;
        }
    }
}
