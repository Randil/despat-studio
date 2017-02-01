/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This is a utility class that allows to draw text within given boundaries, adjusting font size according to the space avaiable.
 * Code snippet for DrawButtonText() was published as open-source at: 
 * http://bluelinegamestudios.com/posts/drawstring-to-fit-text-to-a-rectangle-in-xna/
 * 
 * Design patterns: 
 ---------------------------------------------------------------------------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatBreakout
{
    class RectangleFontAdjuster
    {
        public void DrawButtonText(SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle boundaries)
        {
            Vector2 size = font.MeasureString(text);
            boundaries.X += 10;
            boundaries.Y += 10;
            boundaries.Width -= 20;
            boundaries.Height -= 20;
            float xScale = (boundaries.Width / size.X);
            float yScale = (boundaries.Height / size.Y);

            // Taking the smaller scaling value will result in the text always fitting in the boundaires.
            float scale = Math.Min(xScale, yScale);

            // Figure out the location to absolutely-center it in the boundaries rectangle.
            int strWidth = (int)Math.Round(size.X * scale);
            int strHeight = (int)Math.Round(size.Y * scale);
            Vector2 position = new Vector2();
            position.X = (((boundaries.Width - strWidth) / 2) + boundaries.X);
            position.Y = (((boundaries.Height - strHeight) / 2) + boundaries.Y);

            // A bunch of settings where we just want to use reasonable defaults.
            float rotation = 0.0f;
            Vector2 spriteOrigin = new Vector2(0, 0);
            float spriteLayer = 0.0f; // all the way in the front
            SpriteEffects spriteEffects = new SpriteEffects();

            // Draw the string to the sprite batch!
            spriteBatch.DrawString(font, text, position, Color.Black, rotation, spriteOrigin, scale, spriteEffects, spriteLayer);
        } 
    }
}
