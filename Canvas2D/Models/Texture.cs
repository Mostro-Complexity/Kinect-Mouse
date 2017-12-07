using Commons;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Canvas2D.Models
{
    public class Texture : Frame
    {
        private int imagewidth, imageheight, texture;
        string path;
        GlobalMaterials globalMaterial;
        List<byte> listOfBytes;

        public int ImageWidth { get => imagewidth; set => imagewidth = value; }
        public int ImageHeight { get => imageheight; set => imageheight = value; }

        public Texture(string path)
        {
            this.path = path;
            globalMaterial = new GlobalMaterials();
            FileStream fileStream = new FileStream(path,
              FileMode.Open);
            Bitmap bit = new Bitmap(fileStream);
            ImageHeight = bit.Height;
            ImageWidth = bit.Width;
            fileStream.Seek(54, SeekOrigin.Begin);
            listOfBytes = new List<byte>();
            int bs;
            while ((bs = fileStream.ReadByte()) != -1)
            {
                listOfBytes.Add((byte)bs);
            }
            fileStream.Close();
        }

        private int GetTexture()
        {
            int texture;
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexEnv(TextureEnvTarget.TextureEnv,
                TextureEnvParameter.TextureEnvMode, 0x2100);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter, 0x2601);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter, 0x2601);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS, 0x2901);
            GL.TexParameter(TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT, 0x2901);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb
                , ImageWidth, imageheight, 0, PixelFormat.Bgr,
                PixelType.UnsignedByte, listOfBytes.ToArray());
            return texture;
        }

        private void SetBackGround()
        {
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(-1, -1);
            GL.TexCoord2(1, 0); GL.Vertex2(+1, -1);
            GL.TexCoord2(1, 1); GL.Vertex2(+1, +1);
            GL.TexCoord2(0, 1); GL.Vertex2(-1, +1);
            GL.End();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Draw2D()
        {
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
            texture = GetTexture();
            GL.Enable(EnableCap.Texture2D);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Scale(20, 20, 1);
            GL.Translate(0, 0, -150);
            SetBackGround();
            GL.PopMatrix();
        }
    }
}
