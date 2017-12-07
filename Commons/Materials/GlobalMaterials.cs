using System.Xml;
using OpenTK.Graphics.OpenGL;

namespace Commons {
    public enum MaterialType {
        CommonBox,// 盒子
        ActiveBox,//盒子（选中）
        BoundingBox,//盒子的包围盒
        GameBoxFrame,
        GameBoxBed,
        AxisOrgin,
        AxisX,
        AxisY,
        AxisZ
    }

    public class GlobalMaterials {
        class Material {
            private string ambient;// 设置环境光反射材质
            private string diffuse;//设置漫反射材质
            private string specular;//设置模型镜面光反射率属性
            private string emission;//材质的辐射颜色
            private string shininess;//镜面指数（光照度）
            private float[] A, D, S, E;
            private float SH;

            public Material(string sA, string sD, string sS, string sE,
                string sSH) {
                ambient = sA;
                diffuse = sD;
                specular = sS;
                emission = sE;
                shininess = sSH;
                if (ambient != "") A = SplitXML(ambient);
                if (diffuse != "") D = SplitXML(diffuse);
                if (specular != "") S = SplitXML(specular);
                if (emission != "") E = SplitXML(emission);
                if (shininess != "") SH = float.Parse(shininess);
            }

            public void SetMaterial() {
                if (ambient != "") GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, A);
                if (diffuse != "") GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, D);
                if (specular != "") GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, S);
                if (emission != "") GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, E);
                if (shininess != "") GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, SH);
            }

            private float[] SplitXML(string S) {
                string[] stmp = S.Split(',');
                float[] val = new float[4];
                for (int i = 0; i < 4; i++) {
                    val[i] = float.Parse(stmp[i]) / 255f;
                }
                return val;
            }
        }

        static private XmlElement MaterialConfig;

        public GlobalMaterials() {

            XmlDocument XML = new XmlDocument();
            XML.LoadXml(Commons.Properties.Resources.ConfigMaterial);
            XmlElement node = (XmlElement)XML.SelectSingleNode("Materials");
            MaterialConfig = node;
        }

        static public void SetMaterial(MaterialType mt) {
            XmlDocument XML = new XmlDocument();
            XML.LoadXml(Commons.Properties.Resources.ConfigMaterial);
            MaterialConfig = (XmlElement)XML.SelectSingleNode("Materials");
            string MType = "";
            switch (mt) {
                case MaterialType.CommonBox:
                    MType = "CommonBox"; break;
                case MaterialType.ActiveBox:
                    MType = "ActiveBox"; break;
                case MaterialType.BoundingBox:
                    MType = "BoundingBox"; break;
                case MaterialType.GameBoxFrame:
                    MType = "GameBoxFrame"; break;
                case MaterialType.GameBoxBed:
                    MType = "GameBoxBed"; break;
                case MaterialType.AxisX:
                    MType = "AxisX"; break;
                case MaterialType.AxisY:
                    MType = "AxisY"; break;
                case MaterialType.AxisZ:
                    MType = "AxisZ"; break;
                case MaterialType.AxisOrgin:
                    MType = "AxisOrgin"; break;
            }
            XmlElement node = (XmlElement)MaterialConfig.SelectSingleNode(MType);
            string ambient, diffuse, specular, emission, shininess;
            ambient = node.SelectSingleNode("ambient").InnerXml;
            diffuse = node.SelectSingleNode("diffuse").InnerXml;
            specular = node.SelectSingleNode("specular").InnerXml;
            emission = node.SelectSingleNode("emission").InnerXml;
            shininess = node.SelectSingleNode("shininess").InnerXml;

            Material objMT = new Material(ambient, diffuse, specular,
                emission, shininess);
            objMT.SetMaterial();
        }
    }
}
