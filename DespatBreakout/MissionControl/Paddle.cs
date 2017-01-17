/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class defines player controlled paddle, handles its updating and drawing.
 * 
 * Design patterns: Component
 ---------------------------------------------------------------------------------------------------------*/

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespatShooter
{
    public class Paddle : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public DespatBreakout game;
        SpriteBatch spriteBatch;
        public String texture;
        public float x, y;
        public float movementSpeed = 500.0f;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;

        //Steering component for player paddle
        PaddleSteering steering;

        public Paddle(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

        public void Initialize(String texture, float x, float y)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.texture = texture;
            this.x = x;
            this.y = y;
            this.sourceRectangle = game.gameTextures.GetTextureRectangle(texture);
            this.destinationRectangle = new Rectangle((int) x, (int) y, sourceRectangle.Width, sourceRectangle.Height);
            steering = new PaddleSteering(this);
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

              spriteBatch.Draw(game.gameTextures.textureSheet,
              destinationRectangle,
              sourceRectangle,
              Color.White);

              spriteBatch.End();
        }

        public void ResetGameTime(GameTime gameTime)
        {
            this.steering.ResetGameTime(gameTime);
        }


    }
}
