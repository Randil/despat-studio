using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class TextureSheetElement
    {
        public string name;
        public Rectangle sourceRectangle;

        public TextureSheetElement(string name, int x, int y, int width, int height)
        {
            this.name = name;
            this.sourceRectangle = new Rectangle(x, y, width, height);
        }
    }
}
