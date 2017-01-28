/* --------------------------------------------------------------------------------------------------------
 * Author: Dominik Szczechla
 * Date: 16/01/2016
 * 
 * This class represents a ball, handles its drawing and updating.
 * Ball contains a ICollisionStrategyComponent, which is implemented separately.
 * There is a posibility of swapping a default collision detection strategy with another one.
 * Ball can be duplicated in-game, so it contains a method which returns its exact copy.
 * 
 * Design patterns: Component, Strategy, Prototype
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
    public class Ball : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public float x, y;
        public Rectangle sourceRectangle;
        public Rectangle destinationRectangle;
        public string textureName;
        public IBallCollisionStrategy collisionStrategy;

        public float xSpeed;
        public float ySpeed;
        public float maxSpeed = 500f;

        DespatBreakout game;
        SpriteBatch spriteBatch;
        GameTime previousGameTime;
        int delta; //Time passed between the frames


          public Ball(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

          public enum HitSide
        {
              top, bottom, left, right
        }

          public void FallDown()
        {
            game.activeMission.BallFalled(this);
        }

        /// <summary>
        /// Duplicate() returns an exact copy of the ball on which method was called.
        /// </summary>
        public Ball Duplicate()
        {
            Ball ball = new Ball(game);
            ball.Initialize(textureName, collisionStrategy, (int)x, (int)y, xSpeed, ySpeed);
            ball.collisionStrategy = ball.collisionStrategy.Duplicate(ball);
            ball.xSpeed = xSpeed;
            ball.ySpeed = ySpeed;
            ball.previousGameTime = previousGameTime;
            return ball;
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

           public void Initialize(string textureName, IBallCollisionStrategy collisionStrategy, int x, int y, float xSpeed, float ySpeed)
        {
            LoadContent();
            this.x = x;
            this.y = y;
            this.xSpeed = xSpeed;
            this.ySpeed = ySpeed;
            this.textureName = textureName;
            this.collisionStrategy = collisionStrategy;

            sourceRectangle = DespatBreakout.Instance.gameTextures.GetTextureRectangle(textureName);
            destinationRectangle = new Rectangle(x, y, sourceRectangle.Width, sourceRectangle.Height);
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            previousGameTime = new GameTime();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            delta = gameTime.TotalGameTime.Milliseconds - previousGameTime.TotalGameTime.Milliseconds;
            previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);

            if (delta < 0) delta = 1000 + delta;

            collisionStrategy.CheckCollisions();

            this.x -= (xSpeed * delta / 1000);
            this.y += (ySpeed * delta / 1000);

            destinationRectangle = new Rectangle((int)x, (int)y, sourceRectangle.Width, sourceRectangle.Height);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(DespatBreakout.Instance.gameTextures.textureSheet,
                destinationRectangle,
                sourceRectangle,
                Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ResetGameTime(GameTime gameTime)
        {
            previousGameTime = new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
        }

    }
}
