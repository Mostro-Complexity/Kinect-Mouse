using System.Xml;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace Commons {
    class clsLights {
        private List<clsLight> Lights;

        public clsLights() {
            Lights = new List<clsLight>();
            XmlDocument XML = new XmlDocument();
            XML.LoadXml(Commons.Properties.Resources.ConfigLight);
            XmlElement node = (XmlElement)XML.SelectSingleNode("Lights");
            node = (XmlElement)node.FirstChild;
            while (node != null) {
                Lights.Add(new clsLight(node));
                node = (XmlElement)node.NextSibling;
            }
        }

        public void SetLight() {
            for (int i = 0; i < Lights.Count; i++) {
                SetLight(i, true);
            }
        }

        public void SetLight(int index, bool open) {
            OpenTK.Graphics.OpenGL.EnableCap Ln = OpenTK.Graphics.
                OpenGL.EnableCap.Light0 + index;
            Lights[index].SetLight(Ln, open);
        }
    }
}
