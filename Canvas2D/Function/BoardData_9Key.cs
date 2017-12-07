using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace Canvas2D.Function {
    public class BoardData_9Key {
        public class EachKey {
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

        public List<List<EachKey>> keys;

        public BoardData_9Key(string path) {
            int i = 0;
            Path.GetFileName(path);
            keys = new List<List<EachKey>>() { null, null, null, null };
            for (int j = 0; j < 4; j++)
                keys[j] = new List<EachKey>() { null, null, null, null, null };
            XmlDocument XML = new XmlDocument();
            XML.Load(path);
            XmlElement node = (XmlElement)XML.SelectSingleNode("Blanks");
            node = (XmlElement)node.FirstChild;
            while (node != null) {
                for (int j = 0; j < 5; j++) {
                    keys[i][j] = new EachKey(node);
                    node = (XmlElement)node.NextSibling;
                }
                i++;
            }
        }
    }
}
