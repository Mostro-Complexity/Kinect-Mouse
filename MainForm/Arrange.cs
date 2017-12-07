using Canvas3D.Controls;
using System;
using System.IO;
using System.Windows.Forms;


namespace MainForm {
    public partial class Arrange : Form {
        Canvas2D.Models.Strip frame;
        private Control3D control;
        private double widthRate;

        public Arrange() {
            InitializeComponent();
            control = new Control3D();
            control.DrawGame += Control_DrawGame;
            widthRate = panel3D1.Width / Size.Width;
        }

        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void On_Models_Adding(object sender, EventArgs e) {
            frame = new Canvas2D.Models.Strip();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "dat文件(*.dat)|*.dat|所有文件(*.*)|*.*";
            openFileDialog.ValidateNames = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string strFileName = openFileDialog.FileName;
                frame.GetDatFile(strFileName);
            }
            panel3D1.Draw();
        }

        private void Canvas3D_DrawGame() {
            if (frame != null)
                frame.Draw();
            if (control == null) {
                control = new Control3D();
                return;
            }
            control.Draw();//添加Kinect
        }

        private void Control_DrawGame() {
            panel3D1.Draw();
        }

        /// <summary>
        /// 开始选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void On_Control_Adding(object sender, EventArgs e) {
            control.Start();
        }

        /// <summary>
        /// 写dat数据文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRecord(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            StreamWriter streamwriter;
            sfd.InitialDirectory = "E:\\";
            sfd.Filter = "dat文件(*.dat)|*.dat";
            if (sfd.ShowDialog() == DialogResult.OK) {
                streamwriter = new StreamWriter(sfd.FileName);
                var recorder = control.GetSingleBodyFrame();
                foreach (var i in recorder) {
                    streamwriter.WriteLine(i.Item1.getX().ToString() +
                        "," + i.Item1.getY().ToString() +
                        "," + i.Item1.getZ().ToString() +
                        " " + i.Item2.getX().ToString() +
                        "," + i.Item2.getY().ToString() +
                        "," + i.Item2.getZ().ToString());
                }
                streamwriter.Close();
            }
        }

        private void OnQuit(object sender, FormClosingEventArgs e) {
            control.Close();
        }

        private void Panel3D_Resize(object sender, EventArgs e) { }
    }
}
