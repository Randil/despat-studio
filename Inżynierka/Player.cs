using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespatShooter
{
    class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        SpriteBatch spriteBatch;
        public String texture;
        public float x, y;
        public float movementSpeed = 8.0f;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        public PlayerSteering steering;

        public Player(Game1 game) : base(game)
        {
            this.game = game;
        }

        public void Initialize(String texture, float x, float y)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.texture = texture;
            this.x = x;
            this.y = y;
            this.sourceRectangle = Game1.Instance.gameTextures.getTextureRectangle(texture);
            this.destinationRectangle = new Rectangle((int) x, (int) y, sourceRectangle.Width, sourceRectangle.Height);
            steering = new PlayerSteering(this);
        }

        public override void Update(GameTime gameTime)
        {
            destinationRectangle.X = (int) x;
            destinationRectangle.Y = (int) y;
            steering.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
              spriteBatch.Begin();

              spriteBatch.Draw(Game1.Instance.gameTextures.textureSheet,
              destinationRectangle,
              sourceRectangle,
              Color.White);

              spriteBatch.End();
        }

    }
}
