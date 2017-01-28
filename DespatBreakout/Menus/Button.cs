/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a clickable button that changes the game state.
 * 
 * Design patterns: 
 ---------------------------------------------------------------------------------------------------------*/
namespace DespatBreakout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class Button : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public SpriteFont buttonFont;
        public int x;
        public int y;
        public string buttonText;
        public string textureName;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        public DespatBreakout.GameState clickDestination;
        public DespatBreakout game;
        public bool isHoovered;
        public SpriteBatch spriteBatch;

        RectangleFontAdjuster helper; //Adjusts font size to the boundaries

        public Button(DespatBreakout game, DespatBreakout.GameState clickDestination)
            : base(game)
        {
            this.game = game;
            this.clickDestination = clickDestination;
            this.helper = new RectangleFontAdjuster();
        }

        

        public void Initialize(SpriteFont font, int x, int y, string text, string textureName)
        {

            LoadContent();

            buttonFont = font;
            this.x = x;
            this.y = y;
            buttonText = text;
            this.textureName = textureName;
            isHoovered = false;
            sourceRectangle = game.buttonTextures.GetTextureRectangle(textureName);
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

            if (!isHoovered)
                spriteBatch.Draw(game.buttonTextures.textureSheet, 
                destinationRectangle, 
                sourceRectangle, 
                Color.White);
            else
                spriteBatch.Draw(game.buttonTextures.textureSheet,
                destinationRectangle,
                sourceRectangle,
                Color.SkyBlue);

            helper.DrawButtonText(spriteBatch, buttonFont, buttonText, destinationRectangle);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public virtual void Click()
        {
            game.currentGameState = clickDestination;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
