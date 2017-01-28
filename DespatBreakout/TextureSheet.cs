/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class is a parser of texture sheets from the form of XML into a list of texture coordinates. 
 * It allows multiple textures to be stored within the same file and accessed by their names.
 * 
 * Design patterns: Interpreter
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DespatBreakout
{
    public class TextureSheet
    {
        public string basePath;
        public Texture2D textureSheet;

        List<TextureSheetElement> textures;

        public TextureSheet(XmlDocument textureAtlas)
        {
            textures = new List<TextureSheetElement> { };
            basePath = textureAtlas.DocumentElement.ChildNodes[0].Attributes["imagePath"].InnerText;
            textureSheet = DespatBreakout.Instance.Content.Load<Texture2D>("Graphics\\"+ basePath );
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

        public Rectangle GetTextureRectangle(string name)
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
