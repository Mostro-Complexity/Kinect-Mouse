using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OpenTK.Graphics.OpenGL;

namespace Commons
{
    class clsLight
    {
        private float[] position = new float[3];//位置(x,y,z,1)
        private float[] ambient = new float[3];//环境光
        private float[] diffuse = new float[3];//散射光
        private float[] specular = new float[3];//高亮光

        public clsLight(XmlElement node)
        {
            string[] v = getArray(node.GetElementsByTagName(
                "position")[0].InnerXml);
            for (int i = 0; i < 3; i++)
            {
                position[i] = float.Parse(v[i]);
            }
            v = getArray(node.GetElementsByTagName(
                "ambient")[0].InnerXml);
            for (int i = 0; i < 3; i++)
            {
                ambient[i] = float.Parse(v[i]) / 255f;
            }
            v = getArray(node.GetElementsByTagName(
                "diffuse")[0].InnerXml);
            for (int i = 0; i < 3; i++)
            {
                diffuse[i] = float.Parse(v[i]) / 255f;
            }
            v = getArray(node.GetElementsByTagName(
                "specular")[0].InnerXml);
            for (int i = 0; i < 3; i++)
            {
                specular[i] = float.Parse(v[i]) / 255f;
            }
        }

        private string[] getArray(string s)
        {
            return s.Split(',');
        }

        public void SetLight(OpenTK.Graphics.OpenGL.EnableCap Ln, bool open)
        {
            if (open)
            {
                GL.Enable(Ln);
                GL.Light((LightName)Ln, LightParameter.Position, position);
                GL.Light((LightName)Ln, LightParameter.Ambient, ambient);
                GL.Light((LightName)Ln, LightParameter.Diffuse, diffuse);
                GL.Light((LightName)Ln, LightParameter.Specular, specular);
            }
            else
            {
                GL.Disable(Ln);
            }
        }
    }
}
