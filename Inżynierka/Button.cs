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
        private SpriteFont buttonFont;
        private Vector2 buttonPosition;
        private String buttonText;

        private Game game;
        SpriteBatch spriteBatch;

        public Button(Game1 game)
            : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public void Initialize(SpriteFont font, Vector2 position, String text)
        {

            LoadContent();

            buttonFont = font;
            buttonPosition = position;
            buttonText = text;

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
            spriteBatch.DrawString(buttonFont, buttonText, buttonPosition, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
