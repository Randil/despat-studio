using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    class Button : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public SpriteFont buttonFont;
        public int x;
        public int y;
        public String buttonText;
        public String textureName;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        public Game1.GameState clickDestination;
        private Game game;
        public bool isHoovered;
        public SpriteBatch spriteBatch;

        public Button(Game1 game, Game1.GameState clickDestination)
            : base(game)
        {
            this.game = game;
            this.clickDestination = clickDestination;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(SpriteFont font, int x, int y, String text, String textureName)
        {

            LoadContent();

            buttonFont = font;
            this.x = x;
            this.y = y;
            buttonText = text;
            this.textureName = textureName;
            isHoovered = false;

            sourceRectangle = Game1.Instance.buttonTextures.getTextureRectangle(textureName);
            destinationRectangle = new Rectangle(x, y, sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if(!isHoovered)
                spriteBatch.Draw(Game1.Instance.buttonTextures.textureSheet, 
                destinationRectangle, 
                sourceRectangle, 
                Color.White);
            else
                spriteBatch.Draw(Game1.Instance.buttonTextures.textureSheet,
                destinationRectangle,
                sourceRectangle,
                Color.SkyBlue);

            DrawButtonText(spriteBatch, buttonFont, buttonText, destinationRectangle);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public virtual void Click()
        {
            Game1.Instance.currentGameState = clickDestination;
        }

        static public void DrawButtonText(SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle boundaries)
        {
            //Code published as Open Source on http://bluelinegamestudios.com/posts/drawstring-to-fit-text-to-a-rectangle-in-xna/

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
