using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Canvas2D.Function {
    public class KeyBoardData {
        private class EachKey {
            private Models.Point rightDown;
            private string content;
            private Models.Point leftUp;

            public EachKey(XmlElement node) {
                try {
                    leftUp = new Models.Point(float.Parse(
                    node.FirstChild.NextSibling.FirstChild.InnerText),
                    float.Parse(node.FirstChild.NextSibling.FirstChild.NextSibling.InnerText));
                    rightDown = new Models.Point(float.Parse(
                    node.FirstChild.NextSibling.NextSibling.FirstChild.InnerText),
                    float.Parse(node.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.InnerText));
                    content = node.FirstChild.NextSibling.NextSibling.NextSibling.InnerText;
                } catch {
                    Debug.WriteLine("XML格式出错");
                } finally {
                    Debug.WriteLine(node.FirstChild.NextSibling.FirstChild.InnerText);
                    Debug.WriteLine(node.FirstChild.NextSibling.FirstChild.NextSibling.InnerText);
                    Debug.WriteLine(node.FirstChild.NextSibling.NextSibling.FirstChild.InnerText);
                    Debug.WriteLine(node.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.InnerText);
                    Debug.WriteLine(node.FirstChild.NextSibling.NextSibling.NextSibling.InnerText);
                }
            }

            public Models.Point LeftUp { get => leftUp; }
            public Models.Point RightDown { get => rightDown; }
            public string Character { get => content; }
        }

        private List<EachKey> keys;

        public KeyBoardData(string path) {
            Path.GetFileName(path);
            keys = new List<EachKey>();
            XmlDocument XML = new XmlDocument();
            XML.Load(path);
            XmlElement node = (XmlElement)XML.SelectSingleNode("Blanks");
            node = (XmlElement)node.FirstChild;
            while (node != null) {
                keys.Add(new EachKey(node));
                node = (XmlElement)node.NextSibling;
            }
        }

        public string Search(Models.Point p) {
            foreach (EachKey item in keys) {
                if (p.X >= item.LeftUp.X && p.X <= item.RightDown.X
                    && p.Y >= item.RightDown.Y && p.Y <= item.LeftUp.Y) {
                    return item.Character;
                }
            }
            return string.Empty;
        }
    }
}
