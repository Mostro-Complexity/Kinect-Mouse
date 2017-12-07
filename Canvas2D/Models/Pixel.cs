using Commons;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Canvas2D.Models {
    public class Pixel : Frame {
        private int imagewidth, imageheight;
        string path;
        List<byte> listOfBytes;

        public int ImageWidth { get => imagewidth; set => imagewidth = value; }
        public int ImageHeight { get => imageheight; set => imageheight = value; }

        public override void Draw() {
            throw new NotImplementedException();
        }

        public override void Draw2D() {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GlobalMaterials.SetMaterial(MaterialType.ActiveBox);
            GL.Color3(Color.Red);
            GL.DrawPixels(ImageWidth, ImageHeight, PixelFormat.Bgr,
                            PixelType.UnsignedByte, listOfBytes.ToArray());
        }

        public Pixel(string path) {
            this.path = path;
            FileStream fileStream = new FileStream(path,
              FileMode.Open);
            Bitmap bit = new Bitmap(fileStream);
            ImageHeight = bit.Height;
            ImageWidth = bit.Width;
            fileStream.Seek(54, SeekOrigin.Begin);
            listOfBytes = new List<byte>();
            int bs;
            while ((bs = fileStream.ReadByte()) != -1) {
                listOfBytes.Add((byte)bs);
            }
            fileStream.Close();
        }

        public Pixel() { }
    }
}
