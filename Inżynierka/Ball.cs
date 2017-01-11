using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespatShooter
{
    public class Ball : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum hitSide
        {
            top, bottom, left, right
        }
        public float x, y;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        string textureName;
        Game1 game;
        SpriteBatch spriteBatch;
        GameTime previousGameTime;
        int delta;

        public float xSpeed = 0f;
        public float ySpeed = -500f;
        public float maxSpeed = 500f;

          public Ball(Game1 game) : base(game)
        {
            this.game = game;
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

           public void Initialize(String textureName, int x, int y)
        {

            LoadContent();
            this.x = x;
            this.y = y;
            this.textureName = textureName;

            sourceRectangle = Game1.Instance.gameTextures.getTextureRectangle(textureName);
            destinationRectangle = new Rectangle(x, y, sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            previousGameTime = new GameTime();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            delta = gameTime.TotalGameTime.Milliseconds - previousGameTime.TotalGameTime.Milliseconds;
            if (delta < 0) delta = 1000 + delta;

            this.x -= (xSpeed * delta / 1000);
            this.y += (ySpeed * delta / 1000);

            destinationRectangle = new Rectangle((int)x, (int)y, sourceRectangle.Width, sourceRectangle.Height);

            previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Game1.Instance.gameTextures.textureSheet,
                destinationRectangle,
                sourceRectangle,
                Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }




    }
}
