using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatShooter
{
    public class TextureSheet
    {
        public string basePath;
        List<TextureSheetElement> textures;
        public Texture2D textureSheet;

        public TextureSheet(XmlDocument textureAtlas)
        {
            textures = new List<TextureSheetElement> { };
            basePath = textureAtlas.DocumentElement.ChildNodes[0].Attributes["imagePath"].InnerText;
            textureSheet = Game1.Instance.Content.Load<Texture2D>("Graphics\\"+ basePath );
            string name; 
            int x, y, width, height;
            foreach (XmlNode node in textureAtlas.DocumentElement.ChildNodes[0].ChildNodes)
            {
                name = node.Attributes["name"].InnerText;
                x = Int32.Parse(node.Attributes["x"].InnerText);
                y = Int32.Parse(node.Attributes["y"].InnerText);
                width = Int32.Parse(node.Attributes["width"].InnerText);
                height = Int32.Parse(node.Attributes["height"].InnerText);
                textures.Add(new TextureSheetElement(name, x, y, width, height));
            }

        }

        public Rectangle getTextureRectangle(string name)
        {
            Rectangle result = new Rectangle();
            foreach(TextureSheetElement t in textures)
            {
                if (t.name.Equals(name))
                    result = t.sourceRectangle;
            }
            try
            {
                return result;
            }
            catch(System.NullReferenceException)
            {
                System.ArgumentException argEx = new System.ArgumentException("Cannot find given texture");
                throw argEx;
            }
            
        }

    }
}
