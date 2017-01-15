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
        public string textureName;
        DespatBreakout game;
        SpriteBatch spriteBatch;
        GameTime previousGameTime;
        int delta;
        int delay;
        public IBallCollisionStrategy collisionStrategy;

        public float xSpeed = 0f;
        public float ySpeed = -500f;
        public float maxSpeed = 500f;

          public Ball(DespatBreakout game) : base(game)
        {
            this.game = game;
        }

          public void FallDown()
        {
            game.activeMission.BallFalled(this);
        }

        public Ball Duplicate()
        {
            Ball ball = new Ball(game);
            ball.Initialize(textureName, collisionStrategy, (int)x, (int)y);
            ball.collisionStrategy = ball.collisionStrategy.Duplicate(ball);
            ball.delay = -100;
            ball.xSpeed = xSpeed;
            ball.ySpeed = ySpeed;
            ball.previousGameTime = previousGameTime;
            return ball;
        }

           protected override void LoadContent()
        {
            base.LoadContent();
        }

           public void Initialize(String textureName, IBallCollisionStrategy collisionStrategy, int x, int y)
        {

            LoadContent();
            this.x = x;
            this.y = y;
            this.textureName = textureName;
            this.collisionStrategy = collisionStrategy;
            delay = 1500;

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

            delay -= delta;
            if (delay > 0) return;  //We want some time before the ball starts flying

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




    }
}
